using System.Threading.Tasks;
using Volo.Abp.Settings;

namespace Redcat.Abp.OssManagement.Domain.Settings
{
    public class MallSettingValueProvider : SettingValueProvider
    {
        ISettingStore _settingStore;
        public MallSettingValueProvider(ISettingStore settingStore) : base(settingStore)
        {
            _settingStore = settingStore;
        }
        public override string Name => "Custom";

        public override Task<string> GetOrNullAsync(SettingDefinition setting)
        {

            string returnString = "";
            if (setting.Name == MallSettings.UploadHost)
                returnString = "https://upload-z1.qiniup.com";
            if (setting.Name == MallSettings.CdnHost)
                returnString = "cdn.kingmoo.cn";
            if (setting.Name == MallSettings.AccessKey)
                returnString = "Iw6hxh-YKb5mUG3gjd-WbgJUl7kTMpc3TvditNRy";
            if (setting.Name == MallSettings.SecretKey)
                returnString = "8xZ180KKYE9zscOFRZ8GOXZtQJCzSYiUD_pTTsyn";
            if (setting.Name == MallSettings.BucketName)
                returnString = "ly-blg-imagehosting";
            return Task<string>.FromResult(returnString);
        }
    }
}