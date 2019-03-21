using Grpc.Service.Core.Domain.Repositories;
using ServiceStack.OrmLite;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.EntityFramework.Repositories
{
    public class BalancePayRepository : EfRepository<AccountFinanceEntity>
    {
        public BalancePayRepository(DataContext context) : base(context)
        {
            DbConnection = context.GetConnectionString();
        }

        public bool BalancePay(string accountId, string orderCode, string password, double amount,string tradeId, string sign)
        {
            bool ret = false;
            using (var db = OpenDbConnection())
            {
                using (var cmd = db.SqlProc("BalancePay", new { AccountId = accountId, OrderCode = orderCode, PassWord=password, Amount = amount, TradeId= tradeId,Sign= sign }))
                {
                    var results = cmd.ExecuteScalar();
                    System.Console.WriteLine("BalancePay:"+ results.ToString());
                    if(results.ToString() == "1")
                    {
                        ret = true;
                    }
                }
            }
            return ret;
        }
    }
}
