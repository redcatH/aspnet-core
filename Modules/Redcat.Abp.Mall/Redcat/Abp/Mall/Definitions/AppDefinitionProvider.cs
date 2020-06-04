using System;
using System.Collections.Generic;
using System.Text;
using Redcat.Abp.AppManagement.Apps;

namespace Redcat.Abp.Mall.Definitions
{
    public class MallAppDefinitionProvider: AppDefinitionProvider
    {
        public override void Define(IAppDefinitionContext context)
        {
            context.Add(new AppDefinition(
                "mall",
                "Mall_App"));
        }
    }
}
