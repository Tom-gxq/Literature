using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPCCommonTool
{
    public class CoreChannelHelper
    {
        private List<Channel> channelList = new List<Channel>();
        private int index = 0;
        private Dictionary<string, Channel> ipAddress = new Dictionary<string, Channel>();
        private readonly string serviceKey = null;

        public CoreChannelHelper(string serviceKey)
        {
            this.serviceKey = serviceKey;
            CreateChanel(serviceKey);
        }

        public Channel GetFirstChannel()
        {
            return GetChannel();
        }
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
        private void CreateChanel(string key, ChannelCredentials credentials = null, IEnumerable<ChannelOption> options = null)
        {
            var list = new List<string>();
            list.Add(key);
            var targets = new CoreChannelTargetsSection(list).GetTarget(key);
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
    }

    public class CoreChannelTargetsSection 
    {
        private KeyValueConfigurationCollection targets = new KeyValueConfigurationCollection();
        public KeyValueConfigurationCollection Targets
        {
            get
            {
                return targets;
            }
        }

        readonly IDictionary<string, IEnumerable<string>> _dic = new Dictionary<string, IEnumerable<string>>();

        public CoreChannelTargetsSection(List<string> list)
        {
            foreach (var item in list)
            {
                var value = ConfigurationManager.AppSettings[item].ToString();
                this.targets.Add(new KeyValueConfigurationElement(item, value));
            }
            PostDeserialize();
        }
        public IEnumerable<string> GetTarget(string key)
        {
            if (this._dic.ContainsKey(key))
            {
                return this._dic[key];
            }
            return null;
        }

        private void PostDeserialize()
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
        }
    }
}
