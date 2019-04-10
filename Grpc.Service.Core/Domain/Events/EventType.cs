﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Grpc.Service.Core.Domain.Events
{
    public enum EventType
    {
        AccessTokenCreated,
        AccessTokenDel,
        AccountCreated,
        AliBindCreated,
        WxBindCreated,
        QQBindCreated,
        AccountEdit,
        AccountFinanceCreate,
        AccountInfoCreated,
        AccountInfoEdit,
        AccountPayPwdCreate,
        AccountPayPwdEdit,
        AddressCreated,
        AddressEdit,
        AliBind,
        WxBind,
        QQBind,
        AddShoppingCartNum,
        ApplyPartnerCreated,
        AssociatorCreated,
        AssociatorEdit,
        AuthenticationCreated,
        AuthenticationEdit,
        CashApplyCreated,
        CreatShoppingCart,
        DecreaseProductSku,
        DelAddress,
        DelShoppingCart,
        EditShipOrderStatus,
        HaveAmountEdit,
        KafkaAdd,
        OrderCreated,
        OrderEdit,
        OrderProductCreated,
        OrderStatisticsCreate,
        OrderStatisticsSum,
        OrderSubAmount,
        ProductCreated,
        ProductDel,
        ProductEdit,
        ProductImageCreated,
        ProductImageEdit,
        ProductSkuDBCreate,
        ProductSkuDBUpdate,
        ProductSkuEdit,
        ProductSkuOrderNum,
        RedoProductSku,
        ResidueSkuUpdate,
        SaleStatusEdit,
        SellerStatistics,
        SellerStatisticsSumOrder,
        SellerStatisticsTrade,
        ShipOrderCreated,
        ShipOrderEdit,
        SysStatisticsCreate,
        SysStatisticsSumBuyMember,
        SysStatisticsSumNewMember,
        SysStatisticsSumOrder,
        SysStatisticsSumUser,
        TokenCreated,
        TokenDisabled,
        TradeCreate,
        UpdateShoppingCartOrderID,
        UseAmountEdit,
        BalancePay,
        ConsumeTradeCreate,
        IncomeTradeCreate,
        SuppliersProductCreated,
        SuppliersRegionCreated,
        WxUnionIdEdit,
        WxOpenIdCreate,
        SellerProductCreated,
        SellerProductDel,
        CouponCreated,
        CouponPayed,
        CouponUsed,
    }
}
