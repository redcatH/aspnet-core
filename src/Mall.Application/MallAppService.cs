using System;
using System.Collections.Generic;
using System.Text;
using Mall.Localization;
using Volo.Abp.Application.Services;

namespace Mall
{
    /* Inherit your application services from this class.
     */
    public abstract class MallAppService : ApplicationService
    {
        protected MallAppService()
        {
            LocalizationResource = typeof(MallResource);
        }
    }
}
