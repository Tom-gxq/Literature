using Grpc.Service.Core.Domain.Sender;
using SP.Producer;
using System;
using System.Collections.Generic;
using System.Timers;

namespace SP.Producer
{
    public class Sender: ISender
    {
        private ThreaderState senderState;

        public ThreaderState SenderState
        {
            get { return senderState; }
            set { senderState = value; }
        }

        private List<AbstractEntity> waitingQueries = new List<AbstractEntity>();
        

        private List<AbstractEntity> runningQueries = new List<AbstractEntity>();

        private object singleLock = new object();
        private Threader threader;
        private Timer timer;

        public Sender()
        {
            Init("SP_Sender");
        }
        public Sender(string threadName)
        {
            Init(threadName);
        }
        private void Init(string threadName)
        {
            this.senderState = ThreaderState.Free;
            this.threader = new Threader(threadName);
            this.threader.Completed += new SendCompletedEventHandler(this.SendCompleted);

            int interval = 1000 * 60 * 5;//如果没有被手动触发，则5分钟后自动触发

            timer = new Timer(interval);

            timer.Elapsed += new ElapsedEventHandler(delegate (object sender, ElapsedEventArgs e)
            {
                Start();
            });
        }


        public void Add(AbstractEntity entity)
        {
            entity.ID = Guid.NewGuid().ToString();
            this.waitingQueries.Add(entity);
            if (this.senderState == ThreaderState.Free)
            {
                Start();
            }
        }
        public void Start()
        {
            lock (singleLock)
            {
                if (this.SenderState == ThreaderState.Free && (this.waitingQueries.Count > 0 || this.runningQueries.Count > 0))
                {
                    this.senderState = ThreaderState.Running;
                    this.runningQueries.Clear();
                    this.runningQueries.AddRange(this.waitingQueries);
                    this.waitingQueries.Clear();
                    this.threader.Queries = this.runningQueries;
                    if (this.threader.Queries.Count > 0)
                    {
                        System.Threading.Thread thread = new System.Threading.Thread(this.threader.Start);
                        thread.Start();
                    }
                    else
                    {
                        this.senderState = ThreaderState.Free;
                    }
                }
            }
        }

        private void SendCompleted(object sender, SendCompletedEventArgs e)
        {
            this.senderState = ThreaderState.Free;
            Start();
        }
    }
}
