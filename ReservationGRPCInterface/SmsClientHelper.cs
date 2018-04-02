using RPCCommonTool;
using System;
using static MD.SmsService.Sms;

namespace ReservationGRPCInterface
{
    public class SmsClientHelper
    {
        private static ChannelHelper channelHelper = new ChannelHelper("smsservice_host");
        public static SmsClient GetClient()
        {
            var channle = channelHelper.GetFirstChannel();
            if (channle != null)
            {
                var client = new SmsClient(channle);
                return client;
            }
            else
            {
                return null;
            }
        }
    }
}
