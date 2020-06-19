using System;

namespace Redcat.Abp.Shops.MultiShop
{
    public interface IMultiShop
    {
        Guid? ShopId { get; }
    }
}