﻿using Lib.Application.Services;
using SP.Application.Seller.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.Seller
{
    public interface ISuppliersRegionService : IApplicationService
    {
        bool AddSeller(SuppliersRegionDto data);
        bool DelSeller(int id);
        bool UpdateSeller(SuppliersRegionDto dto);
        List<SuppliersRegionDto> GetSuppliersRegionList(int pageIndex, int pageSize);
        long GetSuppliersRegionCount();
        List<SuppliersRegionDto> SearchSeller(int supplerId);
        int SearchSupplerCount(int supplerId);
        SuppliersRegionDto GetSupplerDetail(int id);
        List<SuppliersRegionDto> SearchSellerData(string name);
        List<SuppliersRegionDto> SearchRegionByName(string supplierName);
        int SearchRegionByNameCount(string supplierName);
    }
}
