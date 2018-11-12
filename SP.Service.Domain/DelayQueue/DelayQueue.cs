using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.DelayQueue
{
    public class DelayQueue
    {        
        private string task_id;
        private string data_key;
        private RedisConf self;
        private DelayQueue queue ;
        private static object lockObj1 = new object();
        private static object lockObj2 = new object();
        public DelayQueue()
        {
            this.self = new RedisConf();
        }
        public void Push(JObject data)
        {
            lock (lockObj1)
            {
                // 唯一ID
                task_id = Guid.NewGuid().ToString();
                data_key = string.Format("{0}_{1}", self.DATA_PREFIX, task_id);
                lock (lockObj2)
                {
                    // save string  
                    self.Client.Set(data_key, data.ToString());
                    // add zset(queue_key=>data_key,ts)  
                    self.Client.SortedSetAdd(self.QUEUE_KEY, data_key, DateTime.Now.Ticks);
                }
            }
        }

        public List<JObject> Pop(int previous= 20)
        {
            var until_ts = DateTime.Now.AddSeconds(-previous).Ticks;
            var task_ids = self.Client.SortedSetRangeByScore(self.QUEUE_KEY, 0, until_ts);
            GetPipelining(self, task_ids);
            List<object> list = new List<object>();
            foreach(var item in task_ids)
            {
                list.Add(item);
            }
            var datas = self.Client.StringGet(list.ToArray());
            self.Client.KeyDelete(list.ToArray());
            List<JObject> retlist = new List<JObject>();
            foreach(var item in datas)
            {
                var data = JsonConvert.DeserializeObject<JObject>(item.ToString());
                retlist.Add(data);
            }
            return retlist;
        }
        private void GetPipelining(RedisConf self,List<object> list)
        {
            var batch = self.Client.CreateBatch();
            foreach(var key in list)
            {
                batch.SortedSetRemoveAsync(self.QUEUE_KEY,key.ToString());
            }
            batch.Execute();
        }
    }
}
