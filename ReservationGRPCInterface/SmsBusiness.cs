using MD.SmsService;
using ReservationGRPCInterface;
using SP.Api.Model.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmsGRPCInterface
{
    public class SmsBusiness
    {
        public static SmsModel SendMessage(string mobile,string message,string accountId)
        {
            var client = SmsClientHelper.GetClient();
            var request1 = new SendMessageRequest()
            {
                 Mobile = mobile,
                 RequestId = Guid.NewGuid().ToString(),
                 Message = message,
                 MessageType = 1,
                 FromAccountId = accountId
            };
            var reuslt = client.SendMessage(request1);
            var model = new SmsModel();
            if (reuslt != null)
            {
                model.Code = reuslt.Code;
                model.Message = reuslt.Message;
            }
            return model;
        }

        public static bool CheckIsAllowSendRegisterMobileMessage(string mobile, string ip)
        {
            var client = SmsClientHelper.GetClient();
            var request1 = new RegisterRequest()
            {
               MobilePhone = mobile,
               Ip = ip
            };
            var reuslt = client.CheckIsAllowSendRegisterMobileMessage(request1);
            var model = new SmsModel();
            if (reuslt != null && reuslt.Code ==0 )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void SetRegisterMobileMessageLimit(string mobile, string ip)
        {
            var client = SmsClientHelper.GetClient();
            var request1 = new RegisterRequest()
            {
                MobilePhone = mobile,
                Ip = ip
            };
            var reuslt = client.SetRegisterMobileMessageLimit(request1);
            
        }
        public static string SendHttp(string url, string data)
        {
            var client = SmsClientHelper.GetClient();
            var request1 = new HttpRequest()
            {
                Url = url,
                Data = data
            };
            var reuslt = client.SendHttp(request1);
            if(reuslt.Code == 10001)
            {
                return reuslt.Message;
            }
            else
            {
                return null;
            }
        }
    }
}
