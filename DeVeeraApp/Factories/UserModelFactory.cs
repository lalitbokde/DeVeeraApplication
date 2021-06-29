using System;
using CRM.Core;
using CRM.Core.Domain.Users;
using CRM.Core.Domain.Security;


using CRM.Services.Helpers;
using DeVeeraApp.ViewModels.UserLogin;
using CRM.Services.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;


namespace DeVeeraApp.Factories
{
    /// <summary>
    /// Represents the User model factory
    /// </summary>
    public partial class UserModelFactory : IUserModelFactory
    {
        #region Fields

      
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly DateTimeSettings _dateTimeSettings;
        private readonly IWorkContext _workContext;
        
        
        private readonly IEncryptionService _encryptionService;
        private readonly IConfiguration _configuration;
        private readonly SecuritySettings _securitySettings;
        private readonly IWebHostEnvironment _hostingEnvironment;
      
        #endregion

        #region Ctor

        public UserModelFactory(
            IDateTimeHelper dateTimeHelper,
            DateTimeSettings dateTimeSettings,
            IEncryptionService encryptionService,
            IWorkContext workContext,
            
                      
            SecuritySettings securitySettings,
            IConfiguration configuration,
            IWebHostEnvironment hostingEnvironment
        
           )
        {
           
            this._dateTimeHelper = dateTimeHelper;
            this._dateTimeSettings = dateTimeSettings;
            this._encryptionService = encryptionService;
            this._workContext = workContext;
            
            
            this._securitySettings = securitySettings;
            this._configuration = configuration;
            this._hostingEnvironment = hostingEnvironment;
            
        }

        #endregion

        #region Methods

      

        /// <summary>
        /// Prepare the login model
        /// </summary>
        /// <param name="checkoutAsGuest">Whether to checkout as guest is enabled</param>
        /// <returns>Login model</returns>
        public virtual ViewModels.User.LoginModel PrepareLoginModel()
        {
            var model = new ViewModels.User.LoginModel
            {
                UsernamesEnabled = true,
              
            };
            return model;
        }

        /// <summary>
        /// Prepare the password recovery model
        /// </summary>
        /// <returns>Password recovery model</returns>
        public virtual PasswordRecoveryModel PreparePasswordRecoveryModel()
        {
            var model = new PasswordRecoveryModel();
            return model;
        }

        /// <summary>
        /// Prepare the password recovery confirm model
        /// </summary>
        /// <returns>Password recovery confirm model</returns>
        public virtual PasswordRecoveryConfirmModel PreparePasswordRecoveryConfirmModel()
        {
            var model = new PasswordRecoveryConfirmModel();
            return model;
        }

        /// <summary>
        /// Prepare the register result model
        /// </summary>
        /// <param name="resultId">Value of UserRegistrationType enum</param>
        /// <returns>Register result model</returns>
        public virtual RegisterResultModel PrepareRegisterResultModel(int resultId)
        {
            var resultText = "";
           
            var model = new RegisterResultModel
            {
                Result = resultText
            };
            return model;
        }

      

       

       

       

        /// <summary>
        /// Prepare the change password model
        /// </summary>
        /// <returns>Change password model</returns>
        public virtual ChangePasswordModel PrepareChangePasswordModel()
        {
            var model = new ChangePasswordModel();
            return model;
        }



        public void PrepareResetPasswordEmail(User model)
        {
            var EncryptedId = _encryptionService.EncryptText(model.Id.ToString());

            //string serverURL = _configuration.GetSection("AppSetting").Get<Dictionary<string, string>>().Where(x => x.Key == "ServerURL").SingleOrDefault().Value; //"http://52.4.251.162:8086";
            string serverURL = "http://dermtech.12skiestech.com";
            string filePath = _hostingEnvironment.ContentRootPath + "/wwwroot/Templates/";

            string reportHTML = System.IO.File.ReadAllText(filePath + "ForgetPassword.html");

            reportHTML = reportHTML.Replace("[%ServerURL%]", serverURL);

            reportHTML = reportHTML.Replace("[%TodayDate%]", DateTime.Now.ToString("dddd,dd MMMM yyyy"));

            try
            {
                string emailBody = "";
                emailBody += "<h1 style=\"color:#1e1e2d;font-weight:400;margin:0;font-size:32px;font-family:'Rubik',sans-serif;\"> You have requested to reset your password</h1>";
                emailBody += "<p style=\"color:#1e1e2d;font-family:'Rubik',sans-serif;\">The Password Reset link will expire in <b>5 minutes.</b></p>";
                emailBody += "<p><a href=\"" + serverURL + "/User/ResetPassword/?encryptedId=" + EncryptedId + " \"style=\"background:#0071BC;text-decoration:none!important;font-weight:500;margin-top:35px;color:#fff;text-transform:uppercase;font-size:14px;padding:10px 24px;display:inline-block;border-radius:50px;\">Reset Password</a></p>";
                emailBody += "<p style=\"color:#1e1e2d;font-family:'Rubik',sans-serif;\">If you did not request password reset, please let us know immediately by replying to this message.</p>";
                reportHTML = reportHTML.Replace("[%EmailBody%]", emailBody);
                string body = reportHTML;

                
               

            }
            catch (Exception ex)
            {
                throw ex;
            }



        }


        #endregion

    }
}
