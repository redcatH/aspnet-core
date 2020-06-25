namespace Redcat.Abp.OssManagement.Domain.Settings
{
    public static class MallSettings
    {
        public const string GroupName = "Mall";

        /* Add constants for setting names. Example:
         * public const string MySettingName = GroupName + ".MySettingName";
         */
        public const string UploadHost = GroupName + ".UploadHost";
        public const string BucketName = GroupName + ".BucketName";
        public const string AccessKey = GroupName + ".AccessKey";
        public const string SecretKey = GroupName + ".SecretKey";
        public const string CdnHost = GroupName + ".CdnHost";
    }
}