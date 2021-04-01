using System;
using System.Linq;
using CRM.Core;
using CRM.Core.Domain.Users;
using CRM.Core.Domain.Security;
using CRM.Services.Common;
using CRM.Services.Directory;
using CRM.Services.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using DeVeeraApp.ViewModels.UserLogin;
using CRM.Services.Security;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
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
        private readonly ICountryService _countryService;
        private readonly IStateProvinceService _stateProvinceService;
        private readonly IEncryptionService _encryptionService;
        private readonly IConfiguration _configuration;
        private readonly SecuritySettings _securitySettings;
        private readonly IHostingEnvironment _hostingEnvironment;
      
        #endregion

        #region Ctor

        public UserModelFactory(
            IDateTimeHelper dateTimeHelper,
            DateTimeSettings dateTimeSettings,
            IEncryptionService encryptionService,
            IWorkContext workContext,
            ICountryService countryService,
            IStateProvinceService stateProvinceService,          
            SecuritySettings securitySettings,
            IConfiguration configuration,
            IHostingEnvironment hostingEnvironment
        
           )
        {
           
            this._dateTimeHelper = dateTimeHelper;
            this._dateTimeSettings = dateTimeSettings;
            this._encryptionService = encryptionService;
            this._workContext = workContext;
            this._countryService = countryService;
            this._stateProvinceService = stateProvinceService;
            this._securitySettings = securitySettings;
            this._configuration = configuration;
            this._hostingEnvironment = hostingEnvironment;
            
        }

        #endregion

        #region Methods

       

        /// <summary>
        /// Prepare the User info model
        /// </summary>
        /// <param name="model">User info model</param>
        /// <param name="User">User</param>
        /// <param name="excludeProperties">Whether to exclude populating of model properties from the entity</param>
        /// <param name="overrideCustomUserAttributesXml">Overridden User attributes in XML format; pass null to use CustomUserAttributes of User</param>
        /// <returns>User info model</returns>
        public virtual UserInfoModel PrepareUserInfoModel(UserInfoModel model, User User,
            bool excludeProperties, string overrideCustomUserAttributesXml = "")
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (User == null)
                throw new ArgumentNullException(nameof(User));

      
            foreach (var tzi in _dateTimeHelper.GetSystemTimeZones())
                model.AvailableTimeZones.Add(new SelectListItem { Text = tzi.DisplayName, Value = tzi.Id, Selected = (excludeProperties ? tzi.Id == model.TimeZoneId : tzi.Id == _dateTimeHelper.CurrentTimeZone.Id) });

      
            //countries
            model.AvailableCountries.Add(new SelectListItem { Text = "Select Country", Value = "0" });

            foreach (var c in _countryService.GetAllCountries(showHidden: true))
            {
                model.AvailableCountries.Add(new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                    Selected = c.Id == model.CountryId
                });
            }

            //states
            var states = _stateProvinceService.GetStateProvincesByCountryId(model.CountryId).ToList();
            if (states.Any())
            {
                model.AvailableStates.Add(new SelectListItem { Text = "Select State", Value = null });

                foreach (var s in states)
                {
                    model.AvailableStates.Add(new SelectListItem { Text = s.Name, Value = s.Id.ToString(), Selected = (s.Id == model.StateProvinceId) });
                }
            }
            else
            {
                var anyCountrySelected = model.AvailableCountries.Any(x => x.Selected);

                model.AvailableStates.Add(new SelectListItem
                {
                    Text = "Select State",
                    Value = null
                });
            }
          

           

           
            
           

           

            return model;
        }

        /// <summary>
        /// Prepare the User register model
        /// </summary>
        /// <param name="model">User register model</param>
        /// <param name="excludeProperties">Whether to exclude populating of model properties from the entity</param>
        /// <param name="overrideCustomUserAttributesXml">Overridden User attributes in XML format; pass null to use CustomUserAttributes of User</param>
        /// <param name="setDefaultValues">Whether to populate model properties by default values</param>
        /// <returns>User register model</returns>
        public virtual RegistrationModel PrepareRegisterModel(RegistrationModel model, bool excludeProperties,
            string overrideCustomUserAttributesXml = "", bool setDefaultValues = false)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            foreach (var tzi in _dateTimeHelper.GetSystemTimeZones())
                model.AvailableTimeZones.Add(new SelectListItem { Text = tzi.DisplayName, Value = tzi.Id, Selected = (excludeProperties ? tzi.Id == model.TimeZoneId : tzi.Id == _dateTimeHelper.CurrentTimeZone.Id) });

          


            //countries
            model.AvailableCountries.Add(new SelectListItem { Text = "Select Country", Value = "0" });

            foreach (var c in _countryService.GetAllCountries(showHidden: true))
            {
                model.AvailableCountries.Add(new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                    Selected = c.Id == model.CountryId
                });
            }

            //states
            var states = _stateProvinceService.GetStateProvincesByCountryId(model.CountryId).ToList();
            if (states.Any())
            {
                model.AvailableStates.Add(new SelectListItem { Text = "Select State", Value = null });

                foreach (var s in states)
                {
                    model.AvailableStates.Add(new SelectListItem { Text = s.Name, Value = s.Id.ToString(), Selected = (s.Id == model.StateProvinceId) });
                }
            }
            else
            {
                var anyCountrySelected = model.AvailableCountries.Any(x => x.Selected);

                model.AvailableStates.Add(new SelectListItem
                {
                    Text = "Select State",
                    Value = null
                });
            }


           

            return model;
        }

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

            string reportHTML = "";



            //string serverURL = _configuration.GetSection("AppSetting").Get<Dictionary<string, string>>().Where(x => x.Key == "ServerURL").SingleOrDefault().Value; //"http://52.4.251.162:8086";
            string serverURL = "http://dermtech.12skiestech.com";
            string filePath = _hostingEnvironment.ContentRootPath + "/wwwroot/Templates/";

            reportHTML = System.IO.File.ReadAllText(filePath + "ForgetPassword.html");

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
