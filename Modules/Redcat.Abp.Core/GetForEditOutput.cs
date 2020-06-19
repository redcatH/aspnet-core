using System;

namespace Redcat.Abp.Core
{
    public class GetForEditOutput<T>
    {
        public GetForEditOutput(T Dto)
        {
            this.Data = Dto;
        }

        public T Data { get; set; }
    }
}
