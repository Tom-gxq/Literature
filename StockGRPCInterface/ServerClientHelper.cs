using RPCCommonTool;
using System;
using System.Collections.Generic;
using System.Text;
using static SP.Service.StockService;

namespace StockGRPCInterface
{
    class ServerClientHelper
    {
        private static ChannelHelper channelHelper = new ChannelHelper("stockservice_host");
        public static StockServiceClient GetClient()
        {
            var channle = channelHelper.GetFirstChannel();
            if (channle != null)
            {
                var client = new StockServiceClient(channle);
                return client;
            }
            else
            {
                return null;
            }
        }
    }
}
