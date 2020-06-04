using Microsoft.EntityFrameworkCore;
using Redcat.Abp.AppManagement.Domain;
using Volo.Abp.EntityFrameworkCore;

namespace Redcat.Abp.AppManagement.EntityFrameworkCore
{
    public interface IAppManagementDbContext:IEfCoreDbContext
    {
        DbSet<App> Apps { get; set; }
    }
}