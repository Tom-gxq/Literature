using RPCCommonTool;
using System;
using static SP.Service.AccountService;

namespace AccountGRPCInterface
{
    public class AccountClientHelper
    {
        private static ChannelHelper channelHelper = new ChannelHelper("accountservice_host");
        public static AccountServiceClient GetClient()
        {
            var channle = channelHelper.GetFirstChannel();
            if (channle != null)
            {
                var client = new AccountServiceClient(channle);
                return client;
            }
            else
            {
                return null;
            }
        }
    }
}
