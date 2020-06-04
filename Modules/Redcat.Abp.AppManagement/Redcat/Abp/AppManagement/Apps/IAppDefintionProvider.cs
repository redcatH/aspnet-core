using Volo.Abp.DependencyInjection;

namespace Redcat.Abp.AppManagement.Apps
{
    public interface IAppDefinitionProvider
    {
        void Define(IAppDefinitionContext context);

    }

    public abstract class AppDefinitionProvider : IAppDefinitionProvider,ITransientDependency
    {
        public abstract void Define(IAppDefinitionContext context);
    }
}