using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using Redcat.Abp.Mall.Domain;
using Volo.Abp.Application.Dtos;

namespace Redcat.Abp.Mall.Application.Contracts
{
    public class ProductCategoryCreateOrUpdateDto
    {
        public JToken apps { get; set; }
        public string Name { get; set; }
        public string ShortNmae { get; set; }
        public string LogoImage { get; set; }
        public string RedirectUrl { get; set; }

    }

    public class appsDto{
        public string appName { get; set; }
    }
}
