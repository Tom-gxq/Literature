using Grpc.Service.Core.Domain.Sender;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Producer
{
    internal delegate void SendCompletedEventHandler(object sender, SendCompletedEventArgs e);
    internal class SendCompletedEventArgs : EventArgs
    {
        public SendCompletedEventArgs() { }
    }
    internal class Threader
    {
        public string ThreaderName { get; set; }

        public ThreaderState State { get; set; }

        public List<AbstractEntity> Queries { get; set; }

        public event SendCompletedEventHandler Completed;

        public Threader(string threaderName)
        {
            this.ThreaderName = threaderName;
            this.State = ThreaderState.Free;
        }

        public Threader(string threaderName, List<AbstractEntity> entities)
        {
            this.ThreaderName = threaderName;
            this.State = ThreaderState.Free;
            this.Queries = entities;
        }

        public void Start()
        {
            if (this.Queries != null && this.Queries.Count > 0)
            {
                this.State = ThreaderState.Running;
                //去重
                List<AbstractEntity> newQueries = new List<AbstractEntity>();
                foreach (AbstractEntity item in this.Queries)
                {
                    if (!newQueries.Contains(item))
                        newQueries.Add(item);
                }

                this.Queries = newQueries;
                foreach (AbstractEntity entity in this.Queries)
                {
                    try
                    {
                        entity.Run();
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine("Threader ex=" + ex.Message+ " StackTrace=" + ex.StackTrace);
                    }
                }
                SendCompletedEventArgs e = new SendCompletedEventArgs();
                Completed(this, e);
                this.State = ThreaderState.Free;
            }
        }
    }

    public enum ThreaderState
    {
        Free = 0,
        Running = 1
    }
}
