using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Options;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.Conventions;
using Volo.Abp.Http;
using Volo.Abp.Reflection;

namespace Mall
{
    public class RedcatAbpServiceConvention: AbpServiceConvention
    {
        public RedcatAbpServiceConvention(IOptions<AbpAspNetCoreMvcOptions> options) : base(options)
        {
        }


        protected override string CalculateRouteTemplate(string rootPath, string controllerName, ActionModel action, string httpMethod, [CanBeNull] ConventionalControllerSetting configuration)
        {
            var controllerNameInUrl = NormalizeUrlControllerName(rootPath, controllerName, action, httpMethod, configuration);

            var url = $"api/{rootPath}/{controllerNameInUrl.ToCamelCase()}";

            //Add {id} path if needed

            //var idParameterModel = action.Parameters.FirstOrDefault(p => p.ParameterName == "id");
            //if (idParameterModel != null)
            //{
            //    if (TypeHelper.IsPrimitiveExtended(idParameterModel.ParameterType, includeEnums: true))
            //    {
            //        url += "/{id}";
            //    }
            //    else
            //    {
            //        var properties = idParameterModel
            //            .ParameterType
            //            .GetProperties(BindingFlags.Instance | BindingFlags.Public);

            //        foreach (var property in properties)
            //        {
            //            url += "/{" + property.Name + "}";
            //        }
            //    }
            //}

            //Add action name if needed
            var actionNameInUrl = NormalizeUrlActionName(rootPath, controllerName, action, httpMethod, configuration);
            if (!actionNameInUrl.IsNullOrEmpty())
            {
                url += $"/{actionNameInUrl.ToCamelCase()}";

                //Add secondary Id
                var secondaryIds = action.Parameters.Where(p => p.ParameterName.EndsWith("Id", StringComparison.Ordinal)).ToList();
                if (secondaryIds.Count == 1)
                {
                    url += $"/{{{secondaryIds[0].ParameterName}}}";
                }
            }

            return url;
        }

        protected override string NormalizeUrlActionName(string rootPath, string controllerName, ActionModel action, string httpMethod,
            ConventionalControllerSetting configuration)
        {
            var actionNameInUrl =
                //HttpMethodHelper
                //.RemoveHttpMethodPrefix(action.ActionName, httpMethod)
                action.ActionName.RemovePostFix("Async");

            if (configuration?.UrlActionNameNormalizer == null)
            {
                return actionNameInUrl;
            }

            return configuration.UrlActionNameNormalizer(
                new UrlActionNameNormalizerContext(
                    rootPath,
                    controllerName,
                    action,
                    actionNameInUrl,
                    httpMethod
                )
            );
        }
    }
}
