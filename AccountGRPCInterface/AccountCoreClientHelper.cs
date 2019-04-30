using RPCCommonTool;
using System;
using System.Collections.Generic;
using System.Text;
using static SP.Service.AccountService;

namespace AccountGRPCInterface
{
    public class AccountCoreClientHelper
    {
        private static CoreChannelHelper channelHelper = new CoreChannelHelper("accountservice_host");
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
