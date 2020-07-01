using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Redcat.Abp.Core;
using Redcat.Abp.Shops.Appliction.Dto;
using Redcat.Abp.Shops.Domin;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Linq;
using Volo.Abp.MultiTenancy;

namespace Redcat.Abp.Shops.Appliction
{
    public class ShopAppService : CrudAppService<Shop,ShopDto,Guid,PagedAndSortedResultRequestDto,ShopCreateOrUpdateDto,ShopCreateOrUpdateDto>
    {
        private readonly IRepository<Shop, Guid> _shopRepository;
        private readonly ICurrentTenant _currentTenant;
        
        public ShopAppService(IRepository<Shop, Guid> shopRepository, ICurrentTenant currentTenant):base(shopRepository)
        {
            ObjectMapperContext = typeof(ShopModule);
            this._shopRepository = shopRepository;
            this._currentTenant = currentTenant;
        }

        public async Task<GetForEditOutput<ShopCreateOrUpdateDto>> GetForEdit(Guid id)
        {
            var shop = await _shopRepository.FirstOrDefaultAsync(p=>p.Id==id);
            return new GetForEditOutput<ShopCreateOrUpdateDto>(ObjectMapper.Map<Shop,ShopCreateOrUpdateDto>(shop??new Shop()));
        }



        #region 不使用CrudAppService
        //public async Task<ShopDto> GetAsync(Guid id)
        //{
        //    var find = await _shopRepository.FirstOrDefaultAsync(p => p.Id == id);
        //    if (find == null)
        //        throw new EntityNotFoundException(typeof(Shop), id);
        //    return ObjectMapper.Map<Shop, ShopDto>(find);
        //}


        //public async Task<PagedResultDto<ShopDto>> GetListAsync(PagedResultRequestDto input)
        //{
        //    var query = _shopRepository ?? throw new ArgumentNullException(nameof(_shopRepository));
        //    var totalCount = await query.GetCountAsync();
        //    var shops = await query.PageBy(input).ToListAsync();
        //    return new PagedResultDto<ShopDto>(totalCount, ObjectMapper.Map<List<Shop>, List<ShopDto>>(shops));
        //}

        //public async Task<ShopDto> GetShopContains(ShopCreateOrUpdateDto dto)
        //{
        //    var find = await _shopRepository.FirstOrDefaultAsync(p => p.Name == dto.Name);

        //   // return find == null;
        //}

        public override async Task<ShopDto> CreateAsync(ShopCreateOrUpdateDto dto)
        {
            var find = await _shopRepository.FirstOrDefaultAsync(p => p.Name == dto.Name);
            if (find != null)
            {
                throw new UserFriendlyException("新店铺创建失败",details:$"店铺名称 name:\"{find.Name}\"已存在!");
            }
            var shop = await _shopRepository.InsertAsync(
                new Shop(GuidGenerator.Create(),
                    dto.Name,
                    dto.ShortName,
                    dto.LogoImage,
                    dto.Description)
                {
                    CoverImage = dto.CoverImage
                }
            );
            if (shop == null)
            {
                throw new UserFriendlyException("新店铺添加失败",details:$"店铺添加失败原因未知.");
            }
            return ObjectMapper.Map<Shop, ShopDto>(shop);
        }

        //public override Task<ShopDto> UpdateAsync(Guid id, ShopCreateOrUpdateDto input)
        //{
        //    var find = _shopRepository.FirstOrDefault(p => p.Name == input.Name);
        //    if (find == null)
        //    {
        //        throw new EntityNotFoundException(typeof(ShopCreateOrUpdateDto), input.Name);
        //    }
        //    find.SetName(input.Name);
        //    find.SetCoverImage(input.CoverImage);
        //    find.SetLogoImg(input.LogoImage);
        //    find.SetShortName(input.ShortName);
        //    find.SetDescription(input.Description);
        //    return ObjectMapper.Map<Shop, ShopDto>(find);
        //    //return base.UpdateAsync(id, input);
        //}

        public override async Task<ShopDto> UpdateAsync(Guid id, ShopCreateOrUpdateDto dto)
        {
            var find =await _shopRepository.FirstOrDefaultAsync(p => p.Id == id);
            if (find == null)
            {
                throw new EntityNotFoundException(typeof(ShopCreateOrUpdateDto), dto.Name);
            }
            find.SetName(dto.Name);
            find.SetCoverImage(dto.CoverImage);
            find.SetLogoImg(dto.LogoImage);
            find.SetShortName(dto.ShortName);
            find.SetDescription(dto.Description);
            return ObjectMapper.Map<Shop, ShopDto>(find);
        }
        #endregion

    }
}