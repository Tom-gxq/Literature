using System;

namespace SP.Data.Enum
{
    public enum OrderStatus
    {
        /// <summary>
        /// 等待买家付款
        /// </summary>
        WaitPay = 1,
        /// <summary>
        /// 买家已经付款
        /// </summary>
        Payed = 2,
        /// <summary>
        /// 卖方发货
        /// </summary>
        Shipping = 3,
        /// <summary>
        /// 已关闭
        /// </summary>
        Closed = 4,
        /// <summary>
        /// 订单已完成
        /// </summary>
        Success = 5,
        /// <summary>
        /// 申请退款
        /// </summary>
        ApplyReturnMoney=6,
        /// <summary>
        /// 申请退货
        /// </summary>
        ApplyReturn = 7,
        /// <summary>
        /// 已退款
        /// </summary>
        ReturnedMoney = 9,
        /// <summary>
        /// 已退货
        /// </summary>
        Returned = 10
    }
}
