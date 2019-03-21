using Lib.Application.Services;
using SP.Application.Suppler.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.Suppler
{
    public interface ISupplerAppService : IApplicationService
    {
        bool AddSuppler(SupplerDto dto);
        bool DelSuppler(int id);
        bool DelSupplerById(int id);
        bool UpdateSeller(SupplerDto dto);
        List<SupplerDto> GetSupplerList();
        List<SupplerDto> GetSupplerList(int pageIndex, int pageSize);
        int GetSellerCount();
        List<SupplerDto> SearchSuppler(string productId,int supplerId,int type, int pageIndex, int pageSize);
        int SearchSupplerCount(string productId, int supplerId, int type);
        SupplerDto GetSupplerDetail(int id);
        List<SupplerDto> SearchSellerData(string name);
        SupplerDto GetSellerDataByAccountId(string accountId);
        List<SupplerDto> SearchSellerByName(string name, int pageIndex, int pageSize);
        int SearchSellerByNameCount(string name);
    }
}
