using Redcat.Abp.Mall.EntityFrameworkCore;
using Redcat.Abp.Shops.Domin;
using Redcat.Abp.Shops.Domin.Repository;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Uow;

namespace Redcat.Abp.Mall.Domain.Repository
{
    public class EfMallShopRepository:EfCoreShopRepositoryBase<IMallDbContext,MallShop>,IMallShopRepository
    {
        public EfMallShopRepository(IDbContextProvider<IMallDbContext> dbContextProvider) : base(dbContextProvider)
        {
            
        }
    }

    public interface IMallShopRepository: IShopRepository<MallShop>
    {
        
    }

    public class MallShopLookupService : ShopLookupService<MallShop, IMallShopRepository>, IMallShopLookupService
    {
        public MallShopLookupService(IMallShopRepository shopRepository, IExternalShopLookupService externalShopLookupService, IUnitOfWorkManager unitOfWorkManager) : base(shopRepository, externalShopLookupService, unitOfWorkManager)
        {
            
        }

        public override MallShop CreateShop(IShopData shopData)
        {
            return new MallShop(shopData);
        }
    }

    public interface IMallShopLookupService: IShopLookupService<MallShop>
    {

    }
}