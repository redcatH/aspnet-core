using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Redcat.Abp.Mall.Application.Contracts.MallShop
{
    public class MallShopDto : FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string LogoImage { get;  set; }
        public string CoverImage { get;  set; }
        public string Description { get;  set; }
    }

    public class MallShopCreateOrUpdateDto:EntityDto
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string LogoImage { get; set; }
        public string CoverImage { get; set; }
        public string Description { get; set; }
    }
}
