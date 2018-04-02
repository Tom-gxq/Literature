using RPCCommonTool;
using System;
using System.Collections.Generic;
using System.Text;
using static SP.Service.OrderService;

namespace OrderGRPCInterface
{
    public class OrderClientHelper
    {
        private static ChannelHelper channelHelper = new ChannelHelper("orderservice_host");
        public static OrderServiceClient GetClient()
        {
            var channle = channelHelper.GetFirstChannel();
            if (channle != null)
            {
                var client = new OrderServiceClient(channle);
                return client;
            }
            else
            {
                return null;
            }
        }
    }
}
