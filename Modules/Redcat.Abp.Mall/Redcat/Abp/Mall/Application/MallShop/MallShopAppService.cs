using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Redcat.Abp.Mall.Domain;
using Redcat.Abp.Mall.Domain.Repository;
using Redcat.Abp.Shops.Domin;
using Redcat.Abp.Shops.Domin.Repository;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Redcat.Abp.Mall.Application
{
    public class MallShopAppService:ApplicationService, IMallShopAppService
    {
        private readonly IMallShopLookupService _shopLookupService;
        private IMallShopRepository _mallShopRepository;
        public MallShopAppService(IMallShopRepository mallShopRepository, IMallShopLookupService shopLookupService)
        {
            _mallShopRepository = mallShopRepository;
            this._shopLookupService = shopLookupService;
            ObjectMapperContext = typeof(MallModule);
        }

        public async Task ShopSync(ShopSyncInputDto shopSyncInputDto)
        {
            foreach (var id in shopSyncInputDto.ids)
            {
                await this._shopLookupService.ShopSync(id);
            }
            await  Task.CompletedTask;
        }

        public async Task<PagedResultDto<MallShopDto>> GetListAsync(PagedResultRequestDto input)
        {

            var count = await _mallShopRepository.GetCountAsync();
            var list=await _mallShopRepository.GetDbSet().PageBy(input).AsNoTracking().ToListAsync();
            var dto = ObjectMapper.Map<List<MallShop>, List<MallShopDto>>(list);
            return new PagedResultDto<MallShopDto>(count, dto);
        }
    }

    public class ShopSyncInputDto
    {
        public List<Guid> ids { get; set; }
    }

    public class MallShopDto:EntityDto<Guid>
    {
        public string Name { get; private set; }
        public string ShortName { get; private set; }
        public string LogoImage { get; private set; }
        public string CoverImage { get; private set; }
        public string Description { get; private set; }
    }
}
