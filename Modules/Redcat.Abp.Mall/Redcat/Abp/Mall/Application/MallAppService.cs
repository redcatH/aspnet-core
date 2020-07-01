using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Redcat.Abp.Core;
using Redcat.Abp.Mall.Application.Contracts;
using Redcat.Abp.Mall.Domain;
using Redcat.Abp.Shops.Appliction.Dto;
using Redcat.Abp.Shops.Domin;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Redcat.Abp.Mall.Application
{
    public class MallAppService : CrudAppService<ProductCategory, ProductCategoryDto, Guid, PagedAndSortedResultRequestDto, ProductCategoryCreateOrUpdateDto, ProductCategoryCreateOrUpdateDto>
    {
        private readonly IRepository<ProductCategory, Guid> _productCategoryRepository;
        public MallAppService(IRepository<ProductCategory, Guid> repository) : base(repository)
        {
            this._productCategoryRepository = repository;
            ObjectMapperContext = typeof(MallModule);
        }


        //protected override IQueryable<ProductCategory> CreateFilteredQuery(PagedAndSortedResultRequestDto input)
        //{
        //    return this.Repository;
        //}

       

        //public async Task<GetForEditOutput<ProductCategoryCreateOrUpdateDto>> GetForEdit(Guid id)
        //{
        //    var find = await _productCategoryRepository.FirstOrDefaultAsync(p => p.Id == id);
        //    return new GetForEditOutput<ProductCategoryCreateOrUpdateDto>(ObjectMapper.Map<ProductCategory, ProductCategoryCreateOrUpdateDto>(find ?? new ProductCategory()));
        //}


    }
}
