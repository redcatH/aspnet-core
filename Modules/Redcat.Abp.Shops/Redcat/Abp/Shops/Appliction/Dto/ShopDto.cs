﻿using System;
using Volo.Abp.Application.Dtos;

namespace Redcat.Abp.Shops.Appliction.Dto
{
    public class ShopDto:EntityDto<Guid>
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string LogoImage { get; set; }
        public string CoverImage { get; set; }
        public string Description { get; set; }
    }
}