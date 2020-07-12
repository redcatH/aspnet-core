using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Redcat.Abp.Mall.Application.Contracts
{
    public class ProductCategoryDto:EntityDto<Guid>
    {
        public string Name { get; set; }
        public string ShortNmae { get; set; }
        public string LogoImage { get; set; }
        public string RedirectUrl { get; set; }
    }
}
