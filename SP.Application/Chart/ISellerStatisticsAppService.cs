using Lib.Application.Services;
using SP.Application.Chart.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.Chart
{
    public interface ISellerStatisticsAppService : IApplicationService
    {
        SellerStatisticsDto GetSellerStatistics(int sellerId, DateTime dateTime);
    }
}
