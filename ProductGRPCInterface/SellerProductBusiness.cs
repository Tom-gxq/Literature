using SP.Api.Model.Seller;
using SP.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductGRPCInterface
{
    public class SellerProductBusiness
    {
        public static bool SelectSellerProduct(int supplierProductId, string accountId, bool isSelected)
        {
            var client = ProductClientHelper.GetClient();
            var request1 = new SelectSellerProductRequest()
            {
                 AccountId = accountId,
                 IsSelected = isSelected,
                 SupplierProductId = supplierProductId
            };
            var result = client.SelectSellerProduct(request1);
            if (result.Status == 10001)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool UpdateSupplierProductSaleStatus(string productId, int suppliersId, int saleStatus)
        {
            var client = ProductClientHelper.GetClient();
            var request1 = new SupplierProductSaleStatusRequest()
            {
                ProductId= productId,
                SuppliersId = suppliersId,
                SaleStatus = saleStatus
            };
            var result = client.UpdateSupplierProductSaleStatus(request1);
            if (result.Status == 10001)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool AddSuppliersProduct(string accountId,string productId, int suppliersId, double purchasePrice,int mainType,int secondType)
        {
            var client = ProductClientHelper.GetClient();
            var request1 = new AddSuppliersProductRequest()
            {
               ProductId = productId,
               SuppliersId = suppliersId,
               AccountId = accountId,
               PurchasePrice = purchasePrice,
               MainType = mainType,
               SecondType = secondType
            };
            var result = client.AddSuppliersProduct(request1);
            if (result.Status == 10001)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool UpdateSuppliersProduct( string productId, int suppliersId, double purchasePrice)
        {
            var client = ProductClientHelper.GetClient();
            var request1 = new ProductRequest()
            {
                ProductId = productId,
                SuppliersId = suppliersId,
                PurchasePrice = purchasePrice,
            };
            var result = client.UpdateSuppliersProduct(request1);
            if (result.Status == 10001)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static SuppliersProductModel GetSuppliersProductById( int suppliersPrductId)
        {
            var client = ProductClientHelper.GetClient();
            var request1 = new SupplierIdRequest()
            {
                SupplierId = suppliersPrductId
            };
            var result = client.GetSuppliersProductById(request1);
            var model = new SuppliersProductModel();
            if (result.Status == 10001)
            {
                model.ProductName = result.Product.ProductName;
                model.ProductId = result.Product.ProductId;
                model.PurchasePrice = result.Product.PurchasePrice;
                model.SaleStatus = result.Product.SaleStatus;
                model.SuppliersId = result.Product.SuppliersId;
            }
            return model;
        }

        public static ProductTypeModel GetSuppliersType(int suppliersId)
        {
            var client = ProductClientHelper.GetClient();
            var request1 = new SupplierIdRequest()
            {
                SupplierId = suppliersId
            };
            var result = client.GetSuppliersType(request1);
            var model = new ProductTypeModel();
            if (result.Status == 10001)
            {
                model.TypeId = result.ProductType.TypeId;
                model.TypeName = result.ProductType.TypeName;
                model.TypeLogo = result.ProductType.TypeLogo;
                model.TypePath = result.ProductType.TypePath;
                model.Remark = result.ProductType.Remark;
            }
            return model;
        }

        public static SupplierModel GetSupplierInfo(string accountId)
        {
            var client = ProductClientHelper.GetClient();
            var request1 = new AccountIdRequest()
            {
                 AccountId = accountId
            };
            var result = client.GetSupplierInfo(request1);
            var model = new SupplierModel();
            if (result.Status == 10001)
            {
                model.TypeId = result.TypeId;
                model.AccountId = result.AccountId;
                model.AlipayNo = result.AlipayNo;
                model.CellPhone = result.CellPhone;
                model.SuppliersId = result.SuppliersId;
                model.SuppliersName = result.SuppliersName;
            }
            return model;
        }

        public static List<SuppliersProduct> GetSuppliersProducts(int mainType, int secondType, int suppliersId)
        {
            var client = ProductClientHelper.GetClient();
            var request1 = new SuppliersProductRequest()
            {
                MainType = mainType,
                SecondType = secondType,
                SupplierId = suppliersId
            };
            var result = client.GetSuppliersProducts(request1);
            var modelList = new List<SuppliersProduct>();
            if (result.Status == 10001)
            {
                foreach(var item in modelList)
                {
                    var model = new SuppliersProduct();
                    model.ProductName = item.ProductName;
                    model.ProductId = item.ProductId;
                    model.PurchasePrice = item.PurchasePrice;
                    model.SaleStatus = item.SaleStatus;
                    model.SuppliersId = item.SuppliersId;
                    model.ImagePath = item.ImagePath;
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        public static List<SellerFoodProductModel> GetSellerFoodProductList(int regionId, string accountId, bool isSelected,int pageIndex,int pageSize)
        {
            var client = ProductClientHelper.GetClient();
            var request1 = new SellerFoodProductRequest()
            {
                 RegionId = regionId,
                 AccountId = accountId,
                 IsSelected = isSelected,
                 PageIndex = pageIndex,
                 PageSize = pageSize
            };
            var result = client.GetSellerFoodProductList(request1);
            var modelList = new List<SellerFoodProductModel>();
            if (result.Status == 10001)
            {
                foreach (var item in modelList)
                {
                    var model = new SellerFoodProductModel();
                    model.ProductName = item.ProductName;
                    model.ProductId = item.ProductId;
                    model.PurchasePrice = item.PurchasePrice;
                    model.SupplierProductId = item.SupplierProductId;
                    model.SuppliersId = item.SuppliersId;
                    model.ImagePath = item.ImagePath;
                    model.SelectedStatus = item.SelectedStatus;
                    modelList.Add(model);
                }
            }
            return modelList;
        }
    }
}
