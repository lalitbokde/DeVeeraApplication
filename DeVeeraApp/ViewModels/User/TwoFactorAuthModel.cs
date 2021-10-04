namespace DeVeeraApp.ViewModels.User
{
    public class TwoFactorAuthModel
    {
        public string OTP { get; set; }
        public string MobileNumber { get; set; }
        public int LevelId { get; set; }
        public int ModuleId { get; set; }
        public int UserId { get; set; }
    }
}
