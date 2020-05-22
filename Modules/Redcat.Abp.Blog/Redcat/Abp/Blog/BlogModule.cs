using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectMapping;

namespace Redcat.Abp.Blog.Redcat.Abp.Blog
{
    [DependsOn(
       typeof(AbpAspNetCoreMvcModule),
       typeof(AbpObjectMappingModule)
       )]
    public class BlogModule
    {

    }
}
