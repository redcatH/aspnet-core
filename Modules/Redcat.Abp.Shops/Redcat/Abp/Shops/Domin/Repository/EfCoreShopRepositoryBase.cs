using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Redcat.Abp.Shops.Domin;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Uow;

namespace Redcat.Abp.Shops.Domin.Repository
{
    public class EfCoreShopRepositoryBase<TDbContext,TShop>:EfCoreRepository<TDbContext,TShop,Guid>, IShopRepository<TShop>
    where TDbContext: IEfCoreDbContext
    where TShop:class,IShop
    {
        public EfCoreShopRepositoryBase(IDbContextProvider<TDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        public async Task<TShop> FindByShopNameAsync(string shortName, CancellationToken cancellationToken = default)
        {
            return await DbSet.FirstOrDefaultAsync((x => x.ShortName == shortName), cancellationToken);
        }

        public async Task<List<TShop>> GetList(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        {
            return await DbSet.Where(p => ids.Contains(p.Id)).ToListAsync(cancellationToken);
        }
    }

    public interface IShopRepository<TShop>:IBasicRepository<TShop,Guid>
    where TShop : class,IShop
    {
        Task<TShop> FindByShopNameAsync(string shortName, CancellationToken cancellationToken = default);
        Task<List<TShop>> GetList(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
    }

    public abstract class ShopLookupService<TShop, TShopRepository> : IShopLookupService<TShop>,ITransientDependency
    where TShop:class,IShop
    where TShopRepository: IShopRepository<TShop>
    {
        private TShopRepository _localShopRepository;
        private readonly IExternalShopLookupService _externalShopLookupService;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        protected ShopLookupService(TShopRepository shopRepository, IExternalShopLookupService externalShopLookupService, IUnitOfWorkManager unitOfWorkManager)
        {
            _localShopRepository = shopRepository;
            this._externalShopLookupService = externalShopLookupService;
            _unitOfWorkManager = unitOfWorkManager;
        }
        public Task<TShop> FindShopById(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<TShop> ShopSync(Guid id, CancellationToken cancellationToken = default)
        {
            var localShop =await _localShopRepository.FindAsync(id, cancellationToken: cancellationToken);
            if (_externalShopLookupService == null)
            {
                return localShop;
            }

            var externalShopData = await _externalShopLookupService.FindShopData(id, cancellationToken);
            if (externalShopData == null)
                return localShop;
            if (localShop == null)
            {
                var create = CreateShop(externalShopData);
                await NewUow(() =>_localShopRepository.InsertAsync(create, cancellationToken: cancellationToken));

            }
            else
            {
                if (localShop is IShopUpdate update && update.ShopUpdate(externalShopData))
                {
                    await NewUow(() => _localShopRepository.UpdateAsync(localShop,cancellationToken:cancellationToken));
                }
                else
                {
                    return localShop;
                }
            }

            return localShop;
        }

        public abstract TShop CreateShop(IShopData shopData);

        protected async Task NewUow(Func<Task> func)
        {
            using var uow=_unitOfWorkManager.Begin(true);
            await func();
            await uow.SaveChangesAsync();
        }
    }

    public interface IShopLookupService<TShop>
    {
        Task<TShop> FindShopById(Guid id, CancellationToken cancellationToken = default);
        Task<TShop> ShopSync(Guid id, CancellationToken cancellationToken = default);
    }
}