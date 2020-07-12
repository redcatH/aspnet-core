using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Redcat.Abp.Mall.Application.Product
{
    public class ProductSpuCreateOrUpdateDto
    {
        public Guid? ShopId { get; set; }
        public Guid CategoryId { get; set; }
        public  string Code { get; set; }
        public  string Name { get; set; }
        public  string DescCommon { get; set; }
        public  string PurchaseNotesCommon { get; set; }
        public  DateTime? DateTimeStart { get; set; }
        public  DateTime? DateTimeEnd { get; set; }
        public  int? LimitBuyCount { get; set; }
        public  int? SoldCount { get; set; }

        public List<ProductSkuCreateOrUpdate> ProductSkus { get; set; } = new List<ProductSkuCreateOrUpdate>();
        public JToken Json { get; set; }
    }
}