﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.Product.DTO
{
    public class ProductSkuDto
    {
        public string SkuId { get; set; }
        public int ShopId { get; set; }
        public string ProductId { get; set; }
        public string SKU { get; set; }
        public DateTime EffectiveTime { get; set; }
        public int Stock { get; set; }
        public int AlertStock { get; set; }
        public double Price { get; set; }
        public string ProductName { get; set; }
        public string ShopName { get; set; }
        public int OrderNum { get; set; }
        public string AccountId { get; set; }
        public string DataName { get; set; }
    }
}
