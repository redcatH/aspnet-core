using System;
using System.Collections.Generic;
using System.Text;
using Redcat.Abp.Shops.Domin;

namespace Redcat.Abp.Shops.Domin
{
    public interface IShopUpdate
    {
        bool ShopUpdate(IShopData shopData);
    }
}
