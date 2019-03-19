using System;
using System.Collections.Generic;
using System.Text;

namespace Grpc.Service.Core.Domain.Commands
{
    public enum CommandType
    {
        //Account
        BindOtherAccount,
        CreatAccount,
        CreatAddress,
        CreatAssociator,
        CreatAuthentication,
        CreateAccessToken,
        CreateAccountIDCard,
        CreateAccountPayPwd,
        CreateApplyPartner,
        CreateOtherAccount,
        DelAccessToken,
        DelAddress,
        EditAccount,
        EditAccountInfo,
        EditAccountMobile,
        EditAccountPayPwd,
        EditAccountPwd,
        EditAddress,
        EditAssociator,
        EditAuthentication,
        CreateWxOpenId,
        EditWxUnionId,
        //Order
        CreateCashApply,
        CreateOrder,
        CreatePurchaseOrder,
        EditOrderCode,
        EditOrder,
        EditPurchaseOrder,
        OrderDelStock,
        OrderRedoStock,
        //Product
        CreateProduct,
        CreateProductSkuDB,
        DelProduct,
        EditProduct,
        EditProductSku,
        EditProductSkuDB,
        EditSaleStatus,
        CreateSuppliersProduct,
        CreateSuppliersRegion,
        //ShoppingCart
        CreatShoppingCart,
        //Statistics
        SumMemberStatistics,
        SumOrderStatistics,
        SumSellerStatistics,
        SumUserStatistics,
        //StockShip
        CreatShipOrder,
        EditResidueSku,
        EditShipOrderStatus,
        //Token
        Generate,
        UpdateStatus,
        //Pay
        BalancePay,
        //
        
    }
}
