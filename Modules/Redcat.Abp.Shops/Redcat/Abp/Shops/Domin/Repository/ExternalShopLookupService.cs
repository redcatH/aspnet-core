using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Redcat.Abp.Shops.Domin.Repository
{
    public interface IExternalShopLookupService
    {
        Task<IShopData> FindShopData(Guid id, CancellationToken cancellationToken = default);

    }

    public class DefaultExternalShopLookupServiceProvider: IExternalShopLookupService
    {
        private IRepository<Shop> _shopreRepository;

        public DefaultExternalShopLookupServiceProvider(IRepository<Shop> shopreRepository)
        {
            _shopreRepository = shopreRepository;
        }

        public async Task<IShopData> FindShopData(Guid id, CancellationToken cancellationToken = default)
        {
            return (await _shopreRepository.FindAsync(p => p.Id == id, false, cancellationToken))?.ToShopData();
        }
    }
}