namespace ServiceAutomation.Canvas.WebApi.Constants
{
    public static class Requests
    {
        public static class Home
        {
            public const string GetReferralLink = "GetReferralLink";
            //public const string GetAuthAction = "GetAction";
            public const string GetPersonalPageInfo = "GetPersonalPageInfo";
        }

        public static class User
        {
            public const string Login = "Login";
            public const string Logout = "Logout";
            public const string Register = "Register";
            public const string Refresh = "Refresh";
            public const string GetUserId = "GetUserId";
        }

        public static class Info
        {
            public const string GetThumbnails = "GetThumbnails";
            public const string GetThumbnailById = "GetThumbnailById";
        }

        public static class Package
        {
            public const string GetPackages = nameof(GetPackages);
            public const string BuyPackage = nameof(BuyPackage);
            public const string GetUserPackage = nameof(GetUserPackage);
        }

        public static class Withdraw
        {
            public const string GetWithdrawHistory = "GetWithdrawHistory";
            public const string GetAccuralHistory = "GetAccuralHistory";
            public const string MakeWithdraw = "Withdraw";
        }

        public static class Structure
        {
            public const string GetUserGroup = nameof(GetUserGroup);
            public const string GetTree = "GetTree";
            public const string GetUserReferralGroup = nameof(GetUserReferralGroup);
            public const string GetReferralGroup = nameof(GetReferralGroup);
            public const string GetPartnersReferralGroups = nameof(GetPartnersReferralGroups);
        }

        public static class VideoTemplate
        {
            public const string GetVideos = "GetVideos";
            public const string GetVideo = "GetVideoById";
        }

        public static class UserProfile
        {
            public const string GetProfileInfo = "GetProfileInfo";
            public const string UploadProfilePhoto = "UploadProfilePhoto";
            public const string UploadProfileInfo = "UploadProfileInfo";
            public const string ChangePassword = "ChangePassword";
            public const string ChangeEmailAdress = "ChangeEmailAdress";
            public const string UploadPhoneNumber = "UploadPhoneNumber";
        }

        public static class UserDocument
        {
            public const string SendDataForVerification = "SendDataForVerification";
            public const string GetVerifiedData = "GetVerifiedData";
        }

        public static class Progress
        {
            public const string GetUserProgress = "GetUserProgress";
        }
    }
}