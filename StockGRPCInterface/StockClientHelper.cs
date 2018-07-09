using Grpc.Core;
using RPCCommonTool;
using System;
using System.Collections.Generic;
using System.Text;
using static SP.Service.StockService;

namespace StockGRPCInterface
{
    public class StockClientHelper
    {
        private string Host;
        private Channel channle;
        public StockClientHelper(string host)
        {
            Host = host;
        }
        public StockServiceClient GetClient()
        {
            channle = new Channel(Host, ChannelCredentials.Insecure);
            if (channle != null && channle.State != ChannelState.Shutdown 
                && channle.State != ChannelState.TransientFailure)
            {                
                var client = new StockServiceClient(channle);
                return client;
            }
            else
            {
                return null;
            }
        }

        public void Dispose()
        {
            channle.ShutdownAsync();
        }
    }
}
