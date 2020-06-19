using Microsoft.EntityFrameworkCore;
using Redcat.Abp.Shops.Domin;

namespace Redcat.Abp.Shops.EntityFrameworkCore
{
    public interface IShopsDbContext
    {
        DbSet<Shop> Shops { get; set; }
    }
}