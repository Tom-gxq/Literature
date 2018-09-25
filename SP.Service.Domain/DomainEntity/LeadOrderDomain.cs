using Grpc.Service.Core.Domain.Entity;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.DomainEntity
{
    public class LeadOrderDomain : OrderDomain
    {
        public AccountDomain Account { get; internal set; }
        public AccountAddressDomain Address { get; internal set; }
        public ShopDomain Shop { get; internal set; }
        public List<ShoppingCartsDomain> ShoppingCarts { get; internal set; }

        public void SetAddressMemento(BaseEntity memento)
        {
            Address = new AccountAddressDomain();
            Address.SetMemento(memento);
        }
        public void SetShopMemento(BaseEntity memento)
        {
            Shop = new ShopDomain();
            Shop.SetMemento(memento);
        }
        public void SetMemenShoppingCartto(List<ShoppingCartsDomain> memento)
        {
            if (memento != null)
            {
                this.ShoppingCarts = memento;
            }
        }
        public void SetAccountMemento(BaseEntity memento)
        {
            Account = new AccountDomain();
            Account.SetMemento(memento);
        }
        public void SetAccountInfoMemento(BaseEntity memento)
        {
            var entity = memento as AccountInfoEntity;
            Account.UserName = entity?.Fullname??string.Empty;
        }
    }
}
