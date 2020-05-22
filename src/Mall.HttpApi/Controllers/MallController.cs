using Mall.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Mall.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class MallController : AbpController
    {
        protected MallController()
        {
            LocalizationResource = typeof(MallResource);
        }
    }
}