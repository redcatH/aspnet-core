using Mall.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp;
using Volo.Abp.Users;

namespace Mall.EntityFrameworkCore
{
    public static class MallDbContextModelCreatingExtensions
    {
        public static void ConfigureMallHost(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));


            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(MallConsts.DbTablePrefix + "YourEntities", MallConsts.DbSchema);

            //    //...
            //});
        }

        public static void ConfigureCustomUserProperties<TUser>(this EntityTypeBuilder<TUser> Tentity) where TUser : class,IUser
        {
            Tentity.Property<string>(nameof(AppUser.HeadImgUrl)).HasMaxLength(64);
            Tentity.Property<string>(nameof(AppUser.Nickname)).HasMaxLength(64);
        }
    }
}