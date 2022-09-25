namespace ServiceAutomation.Canvas.WebApi.Constants
{
    public static class Requests
    {
        public static class Home
        {
            public const string GetReferralLink = "GetReferralLink/{id}";
            public const string GetAuthAction = "GetAuth";
            public const string GetAction = "GetAction";
        }

        public static class User
        {
            public const string Login = "Login";
            public const string Logout = "Logout";
            public const string Register = "Register";
            public const string Refresh = "Refresh";
        }

        public static class Info
        {
            public const string GetThumbnails = "GetThumbnails";
            public const string GetThumbnailById = "GetThumbnailById/{id}";
        }

        public static class PackageTemplate
        {
            public const string GetPackTemplates = "GetPackTemplates";
            public const string BuyPackTemplate = "BuyPackTemplate";
        }

        public static class Withdraw
        {
            public const string GetWithdrawHistory = "GetWithdrawHistory/{id}";
            public const string MakeWithdraw = "Withdraw";
        }

        public static class Group
        {
            public const string GetTree = "GetTree";
            public const string AddUser = "AddUser";
        }

        public static class VideoTemplate
        {
            public const string GetVideos = "GetVideos";
            public const string GetVideo = "GetVideoById/{id}";
        }

        public static class UserProfile
        {
            public const string GetProfileInfo = "GetProfileInfo/{id}";
            public const string UploadProfilePhoto = "UploadProfilePhoto";
            public const string UploadProfileInfo = "UploadProfileInfo";
        }
    }
}