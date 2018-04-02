using Sms.Service.Interfaces;
using Sms.Service.Senders;
using Sms.Service.Utility;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Sms.Service.Flyweight
{
    internal class ProviderFactory
    {
        private static readonly ConcurrentDictionary<ProviderType, ISendMessage> Flyweights = new ConcurrentDictionary<ProviderType, ISendMessage>();

        public static ISendMessage GetSender(ProviderType type)
        {
            if (!Flyweights.ContainsKey(type))
            {
                ISendMessage provider = null;

                switch (type)
                {
                    //case ProviderType.Rspread:
                    //    provider = new Rspread(type);
                    //    break;
                    //case ProviderType.Yuntongxun:
                    //    provider = new Yuntongxun(type);
                    //    break;
                    //case ProviderType.WebPowerFunction:
                    //    provider = new WebPower(type);
                    //    break;
                    //case ProviderType.WebPowerMarketing:
                    //    provider = new WebPower(type);
                    //    break;
                    //case ProviderType.MWGate:
                    //    provider = new MwGate(type);
                    //    break;
                    //case ProviderType.Yunpian:
                    //    provider = new Yunpian(type);
                    //    break;
                    //case ProviderType.Santi:
                    //    provider = new Santi(type);
                    //    break;
                    case ProviderType.Aliyun:
                        provider = new Senders.Aliyun(type);
                        break;
                    //case ProviderType.Submail:
                    //    provider = new Submail(type);
                    //    break;
                    case ProviderType.Chuanglan:
                        provider = new Chuanglan(type);
                        break;
                    case ProviderType.Xulong:
                        provider = new Xulong(type);
                        break;
                }

                Flyweights.TryAdd(type, provider);
            }
            return Flyweights[type];
        }
    }
}
