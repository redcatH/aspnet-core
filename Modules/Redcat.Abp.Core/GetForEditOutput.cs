using System;
using Newtonsoft.Json.Linq;

namespace Redcat.Abp.Core
{
    public class GetForEditOutput<T>
    {
        public GetForEditOutput(T Dto,JToken j)
        {
            this.Schema = j;
            this.Data = Dto;
        }

        public JToken Schema { get; set; }
        public T Data { get; set; }
    }
}
