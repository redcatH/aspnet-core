using System.Threading.Tasks;
using Qiniu.Storage;
using Qiniu.Util;
using Redcat.Abp.OssManagement.Domain.Settings;
using Redcat.Abp.OssManagement.Redcat.Abp.OssManagement.Application.Contracts;
using Volo.Abp.Application.Services;
using Volo.Abp.Guids;
using Volo.Abp.Settings;

namespace Redcat.Abp.OssManagement.Redcat.Abp.OssManagement.Application
{
    public class OssAppService: ApplicationService
    {
        ISettingProvider _setting { get; set; }
        IGuidGenerator _guidGenerator { get; set; }
        public OssAppService(ISettingProvider setting, IGuidGenerator guidGenerator)
        {
            _setting = setting;
            _guidGenerator = guidGenerator;
        }

        public async Task<OssConfigDto> GetConfigAsync()
        {
            var cndHost = await _setting.GetOrNullAsync(MallSettings.CdnHost);
            var uploadHost = await _setting.GetOrNullAsync(MallSettings.UploadHost);

            return await Task.FromResult(
                new OssConfigDto()
                {
                    cdnHost = cndHost,
                    uploadHost = uploadHost
                });
        }

        public async Task<object> GetToken()
        {
            string ak = await _setting.GetOrNullAsync(MallSettings.AccessKey);
            string sk = await _setting.GetOrNullAsync(MallSettings.SecretKey);
            string bucketName= await _setting.GetOrNullAsync(MallSettings.BucketName);
            Mac mac = new Mac(ak, sk);
            Auth auth = new Auth(mac);
            PutPolicy putPolicy = new PutPolicy();
            putPolicy.MimeLimit = "image/jpeg;image/png";
            putPolicy.SetExpires(3600);
            putPolicy.Scope = bucketName;// "ly-blg-imagehosting";
            putPolicy.SaveKey = _guidGenerator.Create().ToString() + "$(ext)";
            string token = Auth.CreateUploadToken(mac, putPolicy.ToJsonString());
            return await Task.FromResult(new { token });
        }
    }
}