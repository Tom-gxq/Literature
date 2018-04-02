using RPCCommonTool;
using System;
using System.Collections.Generic;
using System.Text;
using static SP.Service.ProductService;

namespace ProductGRPCInterface
{
    class ProductClientHelper
    {
        private static ChannelHelper channelHelper = new ChannelHelper("productservice_host");
        public static ProductServiceClient GetClient()
        {
            var channle = channelHelper.GetFirstChannel();
            if (channle != null)
            {
                var client = new ProductServiceClient(channle);
                return client;
            }
            else
            {
                return null;
            }
        }
    }
}
