using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Producer
{
    public class Util
    {
        private static object singletonLock = new object();

        private static Sender sender;
        /// <summary>
        /// 队列发送器实例
        /// </summary>
        public static Sender Sender
        {
            get
            {
                lock (singletonLock)
                {
                    if (sender == null)
                        sender = new Sender();

                    return sender;
                }
            }
        }
    }
}
