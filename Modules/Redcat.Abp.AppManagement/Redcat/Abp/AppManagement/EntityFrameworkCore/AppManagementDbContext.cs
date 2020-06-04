using Microsoft.EntityFrameworkCore;
using Redcat.Abp.AppManagement.Domain;
using Volo.Abp.EntityFrameworkCore;

namespace Redcat.Abp.AppManagement.EntityFrameworkCore
{
    public class AppManagementDbContext:AbpDbContext<AppManagementDbContext>, IAppManagementDbContext
    {

        public AppManagementDbContext(DbContextOptions<AppManagementDbContext> options) : base(options)
        {
        }

        public DbSet<App> Apps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ConfigureAppManagement();
        }
    }
}