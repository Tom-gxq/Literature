using Lib.Application.Services;
using LibMain.Dependency;
using SP.Application.Chart.DTO;
using SP.DataEntity;
using SP.ManageEntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.Chart
{
    public class SellerStatisticsAppService: ApplicationService, ISellerStatisticsAppService
    {
        public SellerStatisticsDto GetSellerStatistics(int sellerId, DateTime dateTime)
        {
            var repository = IocManager.Instance.Resolve<SellerStatisticsRespository>();
            var entity = repository.GetSellerStatistics(sellerId, dateTime);            

            return ConvertFromRepositoryEntity(entity);
        }

        private static SellerStatisticsDto ConvertFromRepositoryEntity(SellerStatisticsEntity accountInfo)
        {
            if (accountInfo == null)
            {
                return null;
            }
            var accountInfoDto = new SellerStatisticsDto
            {
                AccountId = accountInfo.AccountId,
                CreateTime = accountInfo.CreateTime.Value,
                NewOrder = accountInfo.Num_NewOrder != null ? accountInfo.Num_NewOrder.Value : 0,
                OrderAmount = accountInfo.Num_OrderAmount != null ? accountInfo.Num_OrderAmount.Value : 0,
                SSID = !string.IsNullOrEmpty(accountInfo.SSID) ? accountInfo.SSID : string.Empty,
                UpdateTime = accountInfo.UpdateTime != null ? accountInfo.UpdateTime.Value : DateTime.MinValue,
                IsChecked = accountInfo.IsChecked != null? accountInfo.IsChecked.Value:false
            };

            return accountInfoDto;
        }
    }
}
