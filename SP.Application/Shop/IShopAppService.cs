using Lib.Application.Services;
using SP.Application.Shop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.Shop
{
    public interface IShopAppService: IApplicationService
    {
        bool AddShop(string shopName, int displaySequence, string startTime, string endTime, int shopType, string shopLogo);
        bool DelShop(int id);
        List<ShopDto> GetShopList(int pageIndex, int pageSize);
        int GetShopListCount();
        List<ShopDto> SearchShopByUserName(string keyWord, int pageIndex, int pageSize);
        int SearchShopByUserNameCount(string keyWord);
        ShopDto GetShopDetail(int id);
        bool EditShop(ShopDto admin);
        bool AddShopProduct(int ShopId, string ProductId);
        bool DelShopProductByShopId(int ShopId);
        List<ShopDto> GetShopListByRegionId(int regionId);
    }
}
