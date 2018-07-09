using Grpc.Service.Core.Dependency;
using Microsoft.Extensions.Configuration;
using SP.Service.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Util
{
    public class OrderCommon
    {
        private static object singLockOrderCode = new object();
        private static string OrderCodeBefore = string.Empty;
        private static int OrderCodeAfter = 1;
        public static string GetOrderCode(string clientID)
        {
            clientID = clientID.ToLower();
            lock (singLockOrderCode)
            {
                string tempCode = string.Empty;
                string now = DateTime.Now.ToString("yyyyMMdd");
                if (!OrderCodeBefore.Equals(now))
                {
                    OrderCodeBefore = now;
                    OrderCodeAfter = 1;
                }
                if (OrderCodeAfter == 1)
                {
                    var rep = IocManager.Instance.Resolve(typeof(OrderRepository)) as OrderRepository;

                    string orderCode = rep.GetOrderCode(OrderCodeBefore);
                    if (!string.IsNullOrEmpty(orderCode))
                    {
                        int orderAfter = int.Parse(orderCode.Replace(OrderCodeBefore, string.Empty));
                        OrderCodeAfter = orderAfter + 1;
                    }
                }

                tempCode = OrderCodeBefore + OrderCodeAfter.ToString("00000");
                OrderCodeAfter++;
                return tempCode;
            }
        }

        public static string GetHost()
        {
            var config = IocManager.Instance.Resolve<IConfigurationRoot>();
            string host = string.Empty;
            if (config != null)
            {
                host = config.GetSection("stockservice_host").Value?.ToString() ?? string.Empty;
            }
            return host;
        }
    }
}
