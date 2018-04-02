using System;
using System.Collections.Generic;
using System.Text;
using Grpc.Core;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace RPCCommonTool
{
    public class ChannelHelper
    {
        private List<Channel> channelList = new List<Channel>();
        private int index = 0;
        private Dictionary<string, Channel> ipAddress = new Dictionary<string, Channel>();
        private readonly string serviceKey = null;

        public ChannelHelper(string serviceKey)
        {
            this.serviceKey = serviceKey;
            CreateChanel(serviceKey);
        }
        private void CreateChanel(string key, ChannelCredentials credentials = null, IEnumerable<ChannelOption> options = null)
        {
            var targets = GrpcChannelTargetsSection.Current.GetTarget(key);
            if (targets == null)
            {
                throw new KeyNotFoundException("Can not find target with key '" + key + "'");
            }
            if (credentials == null)
            {
                credentials = ChannelCredentials.Insecure;
            }
            foreach (var target in targets.OrderBy(x => Guid.NewGuid()))
            {
                if (!this.ipAddress.ContainsKey(target))
                {
                    int tInd = 0;
                    try
                    {
                        this.channelList.Add(new Channel(target, credentials, options));
                        tInd = this.channelList.Count - 1;
                        this.channelList[tInd].ConnectAsync(DateTime.UtcNow.AddMilliseconds(GrpcChannelTargetsSection.Current.ConnectTimeout)).Wait();
                        if (this.channelList[tInd].State != ChannelState.Shutdown
                            && this.channelList[tInd].State != ChannelState.TransientFailure)
                        {
                            this.ipAddress[target] = this.channelList[tInd];
                        }
                    }
                    catch (Exception ex)
                    {
                        if (this.channelList[tInd] != null)
                        {
                            var tmpChannel = this.channelList[tInd];
                            this.channelList.Remove(tmpChannel);
                            Task.Factory.StartNew((c) =>
                            {
                                var tmp = c as Channel;
                                if (tmp != null)
                                {
                                    tmp.ShutdownAsync().Wait();
                                }
                            }, tmpChannel);
                        }
                        if (this.ipAddress.ContainsKey(target))
                        {
                            this.ipAddress.Remove(target);
                        }
                    }
                }
            }

        }
        public Channel GetFirstChannel()
        {
            return GetChannel();
        }

        /// <summary>  
        /// 获取Grpc Channel  
        /// </summary>  
        /// <param name="key">配置中的Key值</param>  
        /// <param name="credentials"></param>  
        /// <param name="options"></param>  
        /// <returns></returns>  
        private Channel GetChannel()
        {
            if (this.index >= this.channelList.Count)
            {
                this.index = 0;
            }
            if (this.channelList != null && this.index < this.channelList.Count())
            {
                var channel = this.channelList[this.index];
                this.index++;
                if (channel != null && channel.State != ChannelState.Shutdown && channel.State != ChannelState.TransientFailure)
                {
                    return channel;
                }
                else
                {
                    if (channel != null)
                    {
                        string ipKey = string.Empty;
                        foreach (var item in this.ipAddress)
                        {
                            if (item.Value == channel)
                            {
                                ipKey = item.Key;
                                break;
                            }
                        }
                        this.ipAddress.Remove(ipKey);
                        this.channelList.Remove(channel);

                        var tmpChannel = channel;
                        Task.Factory.StartNew((c) =>
                        {
                            var tmp = c as Channel;
                            if (tmp != null)
                            {
                                tmp.ShutdownAsync().Wait();
                            }
                        }, tmpChannel).ContinueWith(x => { this.CreateChanel(this.serviceKey); });
                    }
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }

    public class GrpcChannelTargetsSection : ConfigurationSection
    {
        public static GrpcChannelTargetsSection Current
        {
            get { return (GrpcChannelTargetsSection)ConfigurationManager.GetSection("grpcChannelTargets"); }
        }
        /// <summary>  
        /// 连接超时时间（单位毫秒）  
        /// </summary>  
        [ConfigurationProperty("connectTimeout", DefaultValue = 500)]
        public int ConnectTimeout
        {
            get { return Convert.ToInt32(this["connectTimeout"]); }
            set { this["connectTimeout"] = value; }
        }

        [ConfigurationProperty("", IsDefaultCollection = true)]
        public KeyValueConfigurationCollection Targets
        {
            get
            {
                return (KeyValueConfigurationCollection)base[""];
            }
        }
        readonly IDictionary<string, IEnumerable<string>> _dic = new Dictionary<string, IEnumerable<string>>();
        protected override void PostDeserialize()
        {
            if (this.Targets == null || this.Targets.Count == 0)
            {
                throw new ArgumentException("No target can be found!");
            }
            foreach (var key in this.Targets.AllKeys)
            {
                var target = this.Targets[key].Value.Split(',').Where(str => !string.IsNullOrWhiteSpace(str));
                if (!target.Any())
                {
                    throw new ArgumentException("Argument error,the target of " + key + " is empty");
                }
                this._dic.Add(key, target);
            }
            base.PostDeserialize();
        }
        public IEnumerable<string> GetTarget(string key)
        {
            if (this._dic.ContainsKey(key))
            {
                return this._dic[key];
            }
            return null;
        }
    }


}
