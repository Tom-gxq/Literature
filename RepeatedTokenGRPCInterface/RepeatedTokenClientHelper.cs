using RPCCommonTool;
using System;
using System.Collections.Generic;
using System.Text;
using static SP.Service.RepeatedTokenService;

namespace RepeatedTokenGRPCInterface
{
    internal class RepeatedTokenClientHelper
    {
        private static ChannelHelper channelHelper = new ChannelHelper("tokenservice_host");
        public static RepeatedTokenServiceClient GetClient()
        {
            var channle = channelHelper.GetFirstChannel();
            if (channle != null)
            {
                var client = new RepeatedTokenServiceClient(channle);
                return client;
            }
            else
            {
                return null;
            }
        }
    }
}
