using Volo.Abp.Settings;

namespace Redcat.Abp.OssManagement.Domain.Settings
{
    public class MallSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            /* Define module settings here.
             * Use names from MallSettings class.
             */
            //定义提供者
            context.Add(
                new SettingDefinition(MallSettings.UploadHost),
                new SettingDefinition(MallSettings.BucketName),
                new SettingDefinition(MallSettings.AccessKey),
                new SettingDefinition(MallSettings.SecretKey),
                new SettingDefinition(MallSettings.CdnHost));
        }
    }
}