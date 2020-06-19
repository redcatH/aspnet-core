using System;
using System.Collections.Generic;
using System.Text;
using Redcat.Abp.AppManagement.Apps;
using Volo.Abp.DependencyInjection;

namespace Redcat.Abp.Mall.Definitions
{
    public class MallAppDefinitionProvider : AppDefinitionProvider
    {
        public override void Define(IAppDefinitionContext context)
        {
            context.Add(new AppDefinition(
                "mall",
                "Mall_App"));
        }
    }

    public abstract class AppDefinitionProvider : IAppDefinitionProvider, ITransientDependency
    {
        public abstract void Define(IAppDefinitionContext context);
    }

}
