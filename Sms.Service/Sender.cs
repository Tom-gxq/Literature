using Grpc.Service.Core.Caching;
using Grpc.Service.Core.Dependency;
using Sms.Service.Cache;
using Sms.Service.Dtos;
using Sms.Service.Entities;
using Sms.Service.Utility;
using Sms.Service.Utility.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sms.Service
{
    public class Sender
    {
        private readonly SendInput sendInput;        

        public Sender(SendInput sendInput)
        {
            this.sendInput = sendInput;
        }

        public SendOutput Send()
        {
            // 必填写检验
            if (string.IsNullOrEmpty(sendInput.RequestId)
                || string.IsNullOrEmpty(sendInput.Mobile)
                || string.IsNullOrEmpty(sendInput.Message))
            {
                WriteLog(Codes.Failed, ProviderType.None, "缺少参数");

                return new SendOutput()
                {
                    Code = Codes.Failed,
                    Message = "缺少参数"
                };
            }

            // 敏感词检验
            if (HaveSensitive())
            {
                WriteLog(Codes.Forbid, ProviderType.None, "含有敏感词");

                return new SendOutput()
                {
                    Code = Codes.Failed,
                    Message = "含有敏感词"
                };
            }

            List<Provider> providers;

            if (sendInput.MessageType == MessageType.Mobile)
            {
                providers = GetMobileProviders();
            }
            else
            {
                providers = GetVoiceProviders();
            }

            var excludeProviderName = string.Empty;
            var cache = IocManager.Instance.Resolve<ICacheManager>().GetCache<string,string>("CacheItems");
            // 判断一定时间内是否向这个手机号发送过，短时间内表示收不到短信或语音，那就不使用之前的供应商发送
            var cacheProvider = cache.GetOrDefault(sendInput.Mobile);            
            if (cacheProvider != null)
            {
                excludeProviderName = cacheProvider;
            }
            var provider = GetRandomProvider(providers, excludeProviderName);
            if (provider == null)
            {
                WriteLog(Codes.Failed, ProviderType.None, "没有找到匹配的供应商");

                return new SendOutput()
                {
                    Code = Codes.Failed,
                    Message = "没有找到匹配的供应商"
                };
            }

            // 短信
            if (sendInput.MessageType == MessageType.Mobile)
            {
                return SendMobileMessageByProvider(provider);
            }

            // 语音
            return SendVoiceMessageByProvider(provider);
        }

        #region 私有

        /// <summary>
        /// 获取所有符合条件的短信供应商
        /// </summary>
        /// <returns></returns>
        private List<Provider> GetMobileProviders()
        {
            List<Provider> providers;

            var isChina = UtilHelper.ValidateChineseMobileNumber(sendInput.Mobile);

            if (isChina)  //支持发送中国号码的短信服务商
            {
                providers = ProviderCache.Providers.Where(item =>
                  item.Mobile?.China != null && (
                      item.Mobile.China.IsSupportText //支持自定义文本发送
                      ||
                      (TemplateType.Default != sendInput.TemplateType
                      && item.Mobile.China.IsSupportTemplate //支持模板发送
                      && item.Mobile.China.Templates.Find(t => t.Type == sendInput.TemplateType) != null)
                  )
                 ).ToList();
            }
            else //支持发送全球号码的短信服务商
            {
                providers = ProviderCache.Providers.Where(item =>
                 item.Mobile?.International != null && (
                     item.Mobile.International.IsSupportText //支持自定义文本发送
                     ||
                     (TemplateType.Default != sendInput.TemplateType
                     && item.Mobile.International.IsSupportTemplate //支持模板发送
                     && item.Mobile.International.Templates.Find(t => t.Type == sendInput.TemplateType) != null)
                 )
                ).ToList();
            }

            return providers;
        }

        /// <summary>
        /// 获取所有符合条件的语音供应商
        /// </summary>
        /// <returns></returns>
        private List<Provider> GetVoiceProviders()
        {
            List<Provider> providers;

            var isChina = UtilHelper.ValidateChineseMobileNumber(sendInput.Mobile);

            if (isChina) //支持发送中国号码的语音服务商
            {
                providers = ProviderCache.Providers.Where(item =>
                  item.Voice?.China != null && (
                  (item.Voice.China.IsSupportText && sendInput.TemplateType == TemplateType.VMRegister)
                  ||
                  (item.Voice.China.IsSupportTemplate
                  && item.Voice.China.Templates.Find(t => t.Type == sendInput.TemplateType) != null)
                  )
                 ).ToList();
            }
            else  //支持发送全球号码的语音服务商
            {
                providers = ProviderCache.Providers.Where(item =>
                 item.Voice?.International != null && (
                 (item.Voice.International.IsSupportText && sendInput.TemplateType == TemplateType.VMRegister)
                 ||
                  (item.Voice.International.IsSupportTemplate
                  && item.Voice.International.Templates.Find(t => t.Type == sendInput.TemplateType) != null)
                 )
                ).ToList();
            }

            return providers;
        }

        /// <summary>
        /// 使用随机出的供应商发送短信
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        private SendOutput SendMobileMessageByProvider(Provider provider)
        {
            var tuple = GetTemplateInfo(provider.Mobile);

            var providerType = (ProviderType)Enum.Parse(typeof(ProviderType), provider.Name);
            var sender = Flyweight.ProviderFactory.GetSender(providerType);

            var sendResult = !string.IsNullOrEmpty(tuple?.Item1) ?
                sender.SendMobileMessage(sendInput.Mobile, tuple.Item2, tuple.Item1) :
                sender.SendMobileMessage(sendInput.Mobile, sendInput.Message);

            var cache = IocManager.Instance.Resolve<ICacheManager>().GetCache<string, string>("CacheItems");
            cache.Set(sendInput.Mobile, provider.Name, TimeSpan.FromSeconds(60));

            WriteSendResultLog(sendResult, providerType);

            return new SendOutput()
            {
                Code = sendResult.Code
            };
        }

        /// <summary>
        /// 使用随机出的供应商发送语音
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        private SendOutput SendVoiceMessageByProvider(Provider provider)
        {
            var tuple = GetTemplateInfo(provider.Voice);

            var providerType = (ProviderType)Enum.Parse(typeof(ProviderType), provider.Name);
            var sender = Flyweight.ProviderFactory.GetSender(providerType);

            var sendResult = !string.IsNullOrEmpty(tuple.Item1) ?
               sender.SendVoiceMessage(sendInput.Mobile, tuple.Item2, tuple.Item1) :
               sender.SendVoiceMessage(sendInput.Mobile, sendInput.Message);

            var cache = IocManager.Instance.Resolve<ICacheManager>().GetCache<string, string>("CacheItems");
            cache.Set(sendInput.Mobile, provider.Name, TimeSpan.FromSeconds(60));

            WriteSendResultLog(sendResult, providerType);

            return new SendOutput()
            {
                Code = sendResult.Code
            };
        }

        /// <summary>
        /// 获取模板参数
        /// </summary>
        /// <param name="supportRange"></param>
        /// <returns></returns>
        private Tuple<string, Dictionary<string, string>> GetTemplateInfo(SupportRange supportRange)
        {
            var templateId = string.Empty;
            var templateDataDic = new Dictionary<string, string>();
            if (sendInput.TemplateType != TemplateType.Default) //如果指定使用模板发送短信
            {
                var isChina = UtilHelper.ValidateChineseMobileNumber(sendInput.Mobile);
                SupportInfo supportInfo = null;
                if (isChina)
                {
                    if (supportRange.China.IsSupportTemplate)
                    {
                        supportInfo = supportRange.China;
                    }
                }
                else
                {
                    if (supportRange.International.IsSupportTemplate)
                    {
                        supportInfo = supportRange.International;
                    }
                }

                var template = supportInfo?.Templates.Find(t => t.Type == sendInput.TemplateType);
                if (template != null)
                {
                    templateId = template.Id;
                    for (var i = 0; i < template.Vars.Count; i++)
                    {
                        var varData = sendInput.TemplateDatas.ElementAtOrDefault(i);
                        templateDataDic.Add(template.Vars[i], varData ?? string.Empty);
                    }
                }
            }

            return new Tuple<string, Dictionary<string, string>>(templateId, templateDataDic);
        }

        /// <summary>
        /// 根据权重随机一个供应商
        /// </summary>
        /// <param name="providers"></param>
        /// <param name="excludeProviderName"></param>
        /// <returns></returns>
        private Provider GetRandomProvider(List<Provider> providers, string excludeProviderName = null)
        {
            var count = providers.Count();
            if (count > 0)
            {
                //排除某一个服务商
                if (!string.IsNullOrEmpty(excludeProviderName) && count > 1)
                {
                    providers = providers.Where(item => item.Name != excludeProviderName).ToList();
                }

                //根据权重随机
                var indexs = new List<int>();
                for (int i = 0; i < providers.Count(); i++)
                {
                    var provider = providers.ElementAt(i);
                    for (int j = 0; j < provider.Weight; j++)
                    {
                        indexs.Add(i);
                    }
                }
                if (indexs.Count > 0)
                {
                    return providers.ElementAt(indexs[new Random().Next(indexs.Count)]);
                }
            }
            return null;
        }

        #endregion

        #region 日志

        /// <summary>
        /// 验证是否含有敏感词
        /// </summary>
        /// <returns></returns>
        private bool HaveSensitive()
        {
            return SensitiveCache.ContainSensitive(sendInput.FromAccountId, Utility.SensitiveType.AccountId)
               || SensitiveCache.ContainSensitive(sendInput.IP, Utility.SensitiveType.IP)
               || SensitiveCache.ContainSensitive(sendInput.Mobile, Utility.SensitiveType.Moblie)
               || SensitiveCache.ContainSensitive(sendInput.Message, Utility.SensitiveType.Text);
        }

        /// <summary>
        /// 发送结果
        /// </summary>
        /// <param name="sendResult"></param>
        /// <param name="providerType"></param>
        private void WriteSendResultLog(SendResult sendResult, ProviderType providerType)
        {
            //RedisHelper.AddSmsSendCount();

            WriteLog(sendResult.Code, providerType, sendResult.Response, sendResult.Ex);
        }

        private void WriteLog(Codes code, ProviderType providerType, string response = null, Exception ex = null)
        {
            //LogHelper.WriteLog(sendInput.RequestId, code, providerType + "-" + sendInput.MessageType, sendInput.Mobile, sendInput.Message, sendInput.FromAccountId, sendInput.IP, response, ex);
        }
        #endregion
    }

}
