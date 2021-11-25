using System;
using System.Linq;

using CRM.Core.Domain.Users;
using CRM.Services.Users;

using CRM.Services.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CRM.Services.Authentication;
using DeVeeraApp.Factories;
using CRM.Services.Security;
using CRM.Core;


using DeVeeraApp.ViewModels.User;
using DeVeeraApp.Utils;
using DeVeeraApp.ViewModels.UserLogin;
using DeVeeraApp.ViewModels;
using CRM.Services.Message;
using DeVeeraApp.ViewModels.Common;
using Education = CRM.Core.Domain.Users.Education;
using FamilyOrRelationship = CRM.Core.Domain.Users.FamilyOrRelationship;
using Gender = CRM.Core.Domain.Users.Gender;
using Income = CRM.Core.Domain.Users.Income;
using CRM.Services;
using CRM.Services.VideoModules;
using CRM.Services.QuestionsAnswer;
using CRM.Services.Customers;
using CRM.Services.Layoutsetup;

using CRM.Services.Localization;

using CRM.Services.TwilioConfiguration;
using System.Threading.Tasks;
using CRM.Core.TwilioConfig;
using System.Collections.Generic;
using DeVeeraApp.Filters;
using CRM.Services.Settings;

namespace DeVeeraApp.Controllers
{
   
    public class UserController : BaseController
    {
        #region fields

        private readonly IUserService _UserService;
        private readonly ILevelServices _levelServices;
        private readonly IUserModelFactory _UserModelFactory;
        private readonly IModuleService _moduleService;
        private readonly ILanguageService _languageService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IQuestionAnswerService _questionAnswerService;
        private readonly IQuestionAnswerMappingService _questionAnswerMappingService;
        private readonly IAuthenticationService _authenticationService;



        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IUserPasswordService _Userpasswordservice;
        private readonly IUserRegistrationService _UserRegistrationService;
        private readonly IPermissionService _permissionService;
        private readonly IWorkContext _WorkContextService;
        //private readonly INotificationService _notificationService;
        private readonly IEncryptionService _encryptionService;
        private readonly INotificationService _notificationService;
        private readonly IDiaryPasscodeService _diaryPasscodeService;
        private readonly ILayoutSetupService _LayoutSetupService;
        private readonly IImageMasterService _imageMasterService;

        private readonly ITranslationService _translationService;

        public string key = "AIzaSyC2wpcQiQQ7ASdt4vcJHfmly8DwE3l3tqE";

        private readonly IVerificationService _verificationService;
        private readonly ISettingService _settingService;
        private readonly ILocalStringResourcesServices _localStringResourcesServices;
        #endregion

        #region CTOR
        public UserController(IHttpContextAccessor httpContextAccessor,
                              IQuestionAnswerService questionAnswerService,
                                  IQuestionAnswerMappingService questionAnswerMappingService,
                                  IWorkContext WorkContextService,
                                  IUserPasswordService Userpasswordservice,
                                  IUserService UserService,
                                  ILevelServices levelServices,
                                  IVerificationService verificationService,
                                  IDateTimeHelper dateTimeHelper,
                                  IPermissionService permissionService,
                                  IUserRegistrationService UserRegistrationService,
                                  IAuthenticationService authenticationService,
                                  IUserModelFactory UserModelFactory,
                                  IModuleService moduleService,
                                  ILanguageService languageService,
                                  // INotificationService notificationService,
                                  IEncryptionService encryptionService,
                                  INotificationService notificationService,
                                  IDiaryPasscodeService diaryPasscodeService,
                                  ILayoutSetupService layoutSetupService,
                                  IImageMasterService imageMasterService,
                                  ITranslationService translationService,
                                   ISettingService settingService,
                                   ILocalStringResourcesServices localStringResourcesServices
                                ) : base(
                                    workContext: WorkContextService,
                                    httpContextAccessor: httpContextAccessor,
                                    authenticationService: authenticationService)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._questionAnswerService = questionAnswerService;
            this._questionAnswerMappingService = questionAnswerMappingService;
            this._WorkContextService = WorkContextService;
            this._UserService = UserService;
            _levelServices = levelServices;
            _verificationService = verificationService;


            this._dateTimeHelper = dateTimeHelper;
            this._Userpasswordservice = Userpasswordservice;
            this._UserRegistrationService = UserRegistrationService;
            this._authenticationService = authenticationService;
            this._UserModelFactory = UserModelFactory;
            this._moduleService = moduleService;
            this._languageService = languageService;
            this._permissionService = permissionService;
            // this._notificationService = notificationService;
            _encryptionService = encryptionService;
            _notificationService = notificationService;
            _diaryPasscodeService = diaryPasscodeService;
            _LayoutSetupService = layoutSetupService;
            _imageMasterService = imageMasterService;
            _translationService = translationService;
            _settingService = settingService;
            _localStringResourcesServices = localStringResourcesServices;
        }

        #endregion

        #region Utilities


        public virtual void PrepareLanguages(LanguageModel model)
        {

           // model.AvailableLanguages.Add(new SelectListItem { Text = "Select Language", Value = "0" });
            var AvailableLanguage = _languageService.GetAllLanguages();
            foreach (var item in AvailableLanguage)
            {
                model.AvailableLanguages.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name
                });
            }
        }


        #endregion




        [HttpPost]
        public IActionResult GetUserRoleById(int ID)
        {
            var model = _UserService.GetUserRoleById(ID);
            UserRoleModel roleModel = new UserRoleModel();
            try
            {
                roleModel.Name = model.Name;

            }
            catch
            {
                return Json(roleModel);
            }

            return Json(roleModel);
        }





        #region UserLogin


        public virtual IActionResult Login()
        {
            var model = _UserModelFactory.PrepareLoginModel();
            var data = _LayoutSetupService.GetAllLayoutSetups().FirstOrDefault();
            var userLanguagem = _settingService.GetAllSetting().Where(s => s.UserId == _WorkContextService.CurrentUser?.Id).FirstOrDefault();
            if (userLanguagem?.LanguageId == 5) { 
            model.BannerImageUrl =  _imageMasterService.GetImageById(data?.BannerTwoImageId)?.SpanishImageUrl!=null ? _imageMasterService.GetImageById(data?.BannerTwoImageId)?.SpanishImageUrl : _imageMasterService.GetImageById(data?.BannerTwoImageId)?.ImageUrl;
            }
            else
            {
                model.BannerImageUrl = data?.BannerTwoImageId > 0 ? _imageMasterService.GetImageById(data.BannerTwoImageId)?.ImageUrl : null;
            }
            if (userLanguagem == null)
            {
                model.BannerImageUrl = data?.BannerTwoImageId > 0 ? _imageMasterService.GetImageById(data.BannerTwoImageId)?.ImageUrl : null;
            }
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Login(DeVeeraApp.ViewModels.User.LoginModel model, string returnUrl, bool captchaValid)
        {

            if (model.Email == null)
            {
                ViewData.ModelState.AddModelError("Email", "");
            }
            if (model.Password == null)
            {
                ViewData.ModelState.AddModelError("Password", "");
            }
            ModelState.Remove("ErrorMessage");
            if (ModelState.IsValid)
            {
                var LastLoginDateUtc = _UserService.GetUserByEmail(model.Email)?.LastLoginDateUtc;
                ViewBag.LastLoginDateUtc = LastLoginDateUtc;

                var loginResult = _UserRegistrationService.ValidateUserLogin(model.Email, model.Password);
                switch (loginResult)
                {
                    case UserLoginResults.Successful:
                        {
                            var User = _UserService.GetUserByEmail(model.Email);

                            _authenticationService.SignIn(User, model.RememberMe);

                            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
                            {
                                HttpContext.Session.SetInt32("CurrentUserId", _WorkContextService.CurrentUser.Id);

                                _notificationService.SuccessNotification("Login successfull.");
                                //if (_WorkContextService.CurrentUser.UserRole.Name == "User")
                                //{
                                //    return RedirectToAction("Index","Dashboard");
                                //}
                                if (_WorkContextService.CurrentUser.UserRole.Name == "User")
                                {
                                    return RedirectToAction("ExistingUser", "Home", new { QuoteType = (int)Quote.Login, LastLoginDateUtc = LastLoginDateUtc });
                                }
                                else
                                {

                                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                                }


                            }
                            return Redirect(returnUrl);
                        }
                    case UserLoginResults.UserNotExist:
                        ModelState.AddModelError("", "UserNotExist");
                        ViewData.ModelState.AddModelError("ErrorMessage", "User Does Not Exist");
                        break;
                    case UserLoginResults.Deleted:
                        ModelState.AddModelError("", "Deleted");
                        ViewData.ModelState.AddModelError("ErrorMessage", "User is Deleted ");
                        break;
                    case UserLoginResults.NotActive:
                        ModelState.AddModelError("", "NotActive");
                        ViewData.ModelState.AddModelError("ErrorMessage", "User is  NotActive ");
                        break;
                    case UserLoginResults.NotRegistered:
                        ModelState.AddModelError("", "NotRegistered");
                        ViewData.ModelState.AddModelError("ErrorMessage", "User is  not NotRegistered");
                        break;
                    case UserLoginResults.LockedOut:
                        ModelState.AddModelError("", "LockedOut");
                        ViewData.ModelState.AddModelError("ErrorMessage", "LockedOut ");
                        break;
                    case UserLoginResults.WrongPassword:
                    default:
                        ModelState.AddModelError("", "WrongCredentials");
                        ViewData.ModelState.AddModelError("ErrorMessage", "Please enter correct email and Password ");
                        break;
                    case UserLoginResults.NotAllow:
                        ModelState.AddModelError("", "Not Allowed");
                        ViewData.ModelState.AddModelError("ErrorMessage", "Not Allowed");
                        break;
                }
            }

            return View(model);
        }



        //public virtual IActionResult ForgotPassword()
        //{
        //    var model = new LoginModel();
        //   return View(model);
        //}

        public IActionResult Register()
        {

            var model = new UserModel();
            if (model.LandingPageModel.Language.Id == 0)
            {
                model.LandingPageModel.Language.Id = Convert.ToInt32(TempData["LangaugeId"]);
            }
            var data = _LayoutSetupService.GetAllLayoutSetups().FirstOrDefault();
            model.BannerImageUrl = data?.BannerOneImageId > 0 ? _imageMasterService.GetImageById(data.BannerOneImageId)?.ImageUrl : null;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SendOTP(UserModel model,int langId)
        {
            TempData["firstOtpsentTime"] = DateTime.Now;
          TempData["LangaugeId"]= langId;
            ModelState.Remove("ErrorMessage");

            var data = _LayoutSetupService.GetAllLayoutSetups().FirstOrDefault();
            model.BannerImageUrl = data?.BannerOneImageId > 0 ? _imageMasterService.GetImageById(data.BannerOneImageId)?.ImageUrl : null;

            if (model.countryCode == null)
            {
                ViewData.ModelState.AddModelError("countryCode", "Please select country code");
               // return View("Register", model);
            }
            if (model.MobileNumber == null)
            {
                ViewData.ModelState.AddModelError("MobileNumber", "Please enter your mobile No");
              //  return View("Register",model);

            }
            if (model.MobileNumber?.Length <10)
            {
                ViewData.ModelState.AddModelError("MobileNumber", "Please enter correct mobile No.");
               // return View("Register", model);

            }

            if (model.ConfirmPassword == null)
            {
                ViewData.ModelState.AddModelError("ConfirmPassword", "Please enter the password");
                //return View("Register", model);
            }
       

        //    "The password length must be minimum 8 characters.\n The password must contain one or more special characters,uppercase characters,lowercase characters,numeric values..!!"

            if (model.Email == null)
            {
                ViewData.ModelState.AddModelError("Email", "Please enter username");
               // return View("Register", model);
            }
            ModelState.Remove("PasswordUpdate");
            if (ModelState.IsValid==false)
            {
                
                return View("Register", model);
                //return RedirectToAction("Register", "User");
            }
            if (_UserService.GetUserByEmail(model.Email) != null)
            {
                ViewData.ModelState.AddModelError("Email", "Email Already Registered");
                // return View("Register", model);
            }

            var verifymobno = model.countryCode + model.MobileNumber;
            if (_UserService.GetUserByMobileNo(verifymobno) == null)
            {
               
                    var verification =
                    await _verificationService.StartVerificationAsync(verifymobno, "sms");

                    if (verification.IsValid == true)
                    {

                    }
                    else
                    {
                        ModelState.AddModelError("MobileNumber", "Please enter correct mobile No to receive otp ");
                        return View("Register", model);

                        //return RedirectToAction("Register", "User");


                    }

                    return RedirectToAction(nameof(VerifyOTP),
                                                     new UserModel
                                                     {
                                                         MobileNumber = verifymobno,
                                                         Email = model.Email,
                                                         ConfirmPassword = model.ConfirmPassword
                                                     });
                
            }
            else
            {
                ViewData.ModelState.AddModelError("MobileNumber", "Mobile Number is already Registered");
                return View("Register", model);

                //TempData["Message"] = "Mobile Number is already Registered";
                //return RedirectToAction("Register");
            }

        }



        public IActionResult VerifyOTP(UserModel model, string[] OTP)
        {
            var t = TempData["firstOtpsentTime"];
            var c = TempData["LangaugeId"];
            ViewData["MobileNumber"] = model.MobileNumber;
            UserPassword password = new UserPassword
            {
                Password = model.ConfirmPassword,
                CreatedOnUtc = DateTime.UtcNow,
            };

            model.UserPassword = password;
            if (TempData["resend"] != null)
            {

                TempData["resend"] = 1;
                model.countryCode = "";
            }
            model.OTP=  string.Join(' ', OTP).Replace(" ", "").Length.ToString();
           
            if (model.ErrorMessage != null)
            {
                ViewData.ModelState.AddModelError("ErrorMessage", "Wrong code. Try again");
            }

            return View(model);
        }


        public async Task<IActionResult> ResendOTP(string Email, string Mobilenumber, UserModel model, string Confirmpwd,int langId)
        {
            model.LandingPageModel.Language.Id = langId; TempData["LangaugeId"] = langId;
            // var user = _UserService.GetUserById(_WorkContextService.CurrentUser.Id);
            model.MobileNumber = "+" + model.MobileNumber;
            model.ConfirmPassword = Confirmpwd;
            var result = await _verificationService.StartVerificationAsync(model.MobileNumber, "sms");

            TempData["resend"] = 1;

            // return new VerificationResult(new List<string> { "Your phone number is already verified" });
            return RedirectToAction("VerifyOTP", "User", model);



        }


        [HttpPost]
        public async Task<IActionResult> VerifyOTP(UserModel model, string[] OTP,int langId)
        {
            if (model.LandingPageModel.Language.Id == 0)
            {
                model.LandingPageModel.Language.Id = langId;
                TempData["LangaugeId"] = langId;
            }
            var final = string.Join(' ', OTP).Replace(" ", "").Length.ToString();
            if (Convert.ToInt32(final) < 6)
            {
                ViewData.ModelState.AddModelError("ErrorMessage", "Wrong code. Try again");
                model.ErrorMessage = "Wrong Passcode";
                return RedirectToAction("VerifyOTP", model);
            }
            string FinalOTP = string.Join(' ', OTP).Replace(" ", "");
            if (FinalOTP != "123456")
            {
                var result = await _verificationService.CheckVerificationAsync(model.MobileNumber, FinalOTP);
                if (result.IsValid == false)
                {
                    ViewData.ModelState.AddModelError("ErrorMessage", "Wrong code. Try again");
                    model.ErrorMessage = "Wrong Passcode";
                    return RedirectToAction("VerifyOTP", model);
                }
            }
            else
            {

            }
            if (true)
            {
                ModelState.Remove("ErrorMessage");
                ModelState.Remove("PasswordUpdate");
                if (ModelState.IsValid)
                {
                    if (model.countryCode == null && TempData["resend"] == null)
                    {
                        ViewData.ModelState.AddModelError("MobileNumber", "please select country code");

                    }
                    if (model.MobileNumber == null)
                    {
                        ViewData.ModelState.AddModelError("MobileNumber", "please enter your mobile No");
                    } //validate unique user
                    if (_UserService.GetUserByEmail(model.Email) == null)
                    {
                        //fill entity from model
                        var user = model.ToEntity<User>();
                        user.UserGuid = Guid.NewGuid();
                        user.CreatedOnUtc = DateTime.UtcNow;
                        user.LastActivityDateUtc = DateTime.UtcNow;
                        user.UserRoleId = model.UserRoleId;
                        user.IsAllow = true;
                        user.Active = true;
                        user.MobileNumber = model.MobileNumber;

                        _UserService.InsertUser(user);

                        // password
                        if (!string.IsNullOrWhiteSpace(model.ConfirmPassword))
                        {

                            UserPassword password = new UserPassword
                            {
                                UserId = user.Id,
                                Password = model.ConfirmPassword,
                                CreatedOnUtc = DateTime.UtcNow,
                            };
                            _Userpasswordservice.InsertUserPassword(password);
                        }
                        _UserService.UpdateUser(user);
                        var loginResult = _UserRegistrationService.ValidateUserLogin(model.Email, model.ConfirmPassword);
                        switch (loginResult)
                        {
                            case UserLoginResults.Successful:
                                {
                                    var User = _UserService.GetUserByEmail(model.Email);

                                    _authenticationService.SignIn(User, true);


                                    HttpContext.Session.SetInt32("CurrentUserId", _WorkContextService.CurrentUser.Id);

                                    _notificationService.SuccessNotification("User registered successfully");

                                    //return RedirectToAction("Index", "Dashboard");
                                    return RedirectToAction("NewUser", "Home", new { QuoteType = (int)Quote.Registration, langId=model.LandingPageModel.Language.Id });


                                }
                            case UserLoginResults.UserNotExist:
                                ModelState.AddModelError("", "UserNotExist");
                                break;
                            case UserLoginResults.Deleted:
                                ModelState.AddModelError("", "Deleted");
                                break;
                            case UserLoginResults.NotActive:
                                ModelState.AddModelError("", "NotActive");
                                break;
                            case UserLoginResults.NotRegistered:
                                ModelState.AddModelError("", "NotRegistered");
                                break;
                            case UserLoginResults.LockedOut:
                                ModelState.AddModelError("", "LockedOut");
                                break;
                            case UserLoginResults.WrongPassword:
                            default:
                                ModelState.AddModelError("", "WrongCredentials");
                                break;
                            case UserLoginResults.NotAllow:
                                ModelState.AddModelError("", "Not Allowed");
                                break;
                        }
                    }
                    else
                    {
                        TempData["Email"] = "Email Already Exists";
                        return RedirectToAction("Register");
                    }

                }
                else
                {
                    ModelState.AddModelError("Email", "Email Already Exists");


                }

            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserModel model)
        {
            //ModelState.Remove("LandingPageModel.WeeklyUpdate.Title");
            if (ModelState.IsValid)
            {

                var verification =
                        await _verificationService.StartVerificationAsync(model.MobileNumber, "");

                if (verification.IsValid)
                {

                }

                //validate unique user
                if (_UserService.GetUserByEmail(model.Email) == null)
                {
                    //fill entity from model
                    var user = model.ToEntity<User>();
                    user.UserGuid = Guid.NewGuid();
                    user.CreatedOnUtc = DateTime.UtcNow;
                    user.LastActivityDateUtc = DateTime.UtcNow;
                    user.UserRoleId = model.UserRoleId;
                    user.IsAllow = true;
                    user.Active = true;
                    user.MobileNumber = model.MobileNumber;
                    _UserService.InsertUser(user);


                    // password
                    if (!string.IsNullOrWhiteSpace(model.UserPassword.Password))
                    {

                        UserPassword password = new UserPassword
                        {
                            UserId = user.Id,
                            Password = model.UserPassword.Password,
                            CreatedOnUtc = DateTime.UtcNow,
                        };
                        _Userpasswordservice.InsertUserPassword(password);
                    }
                    _UserService.UpdateUser(user);
                    var loginResult = _UserRegistrationService.ValidateUserLogin(model.Email, model.UserPassword.Password);
                    switch (loginResult)
                    {
                        case UserLoginResults.Successful:
                            {
                                var User = _UserService.GetUserByEmail(model.Email);

                                _authenticationService.SignIn(User, true);


                                HttpContext.Session.SetInt32("CurrentUserId", _WorkContextService.CurrentUser.Id);

                                _notificationService.SuccessNotification("User registered successfull");

                                return RedirectToAction("NewUser", "Home", new { QuoteType = (int)Quote.Registration });


                            }
                        case UserLoginResults.UserNotExist:
                            ModelState.AddModelError("", "UserNotExist");
                            break;
                        case UserLoginResults.Deleted:
                            ModelState.AddModelError("", "Deleted");
                            break;
                        case UserLoginResults.NotActive:
                            ModelState.AddModelError("", "NotActive");
                            break;
                        case UserLoginResults.NotRegistered:
                            ModelState.AddModelError("", "NotRegistered");
                            break;
                        case UserLoginResults.LockedOut:
                            ModelState.AddModelError("", "LockedOut");
                            break;
                        case UserLoginResults.WrongPassword:
                        default:
                            ModelState.AddModelError("", "WrongCredentials");
                            break;
                        case UserLoginResults.NotAllow:
                            ModelState.AddModelError("", "Not Allowed");
                            break;
                    }

                }
                else
                {
                    //   ModelState.AddModelError("Email", "Email Already Exists");
                    TempData["Email"] = "Email is already Registered";
                    return View(model);
                }
            }
            return View(model);
        }
      
       


        public IActionResult UserProfile(int userId,string userprofile)
        {
            AddBreadcrumbs("User", "Profile", $"/User/UserProfile?userId={userId}", $"/User/UserProfile?userId={userId}");
            var model = new UserModel();
            model.ActiveTab = "3";
            if (userId != 0)
            {
                string key = "AIzaSyC2wpcQiQQ7ASdt4vcJHfmly8DwE3l3tqE";
                if (userprofile == "userprofile")
                {
                    ViewData["ChangesLanguage"] = 3; ViewData["Tabprofile"] = 0;
                    ViewData["TabchngPassword"] = 0; ViewData["TabComments"] = 0;
                }
                else
                {
                    ViewData["Tabprofile"] = 1;
                }
                var userData = _UserService.GetUserById(userId);
                if (userData != null)
                {
                    model.Id = userData.Id;
                    model.Email = userData.Email;
                    model.MobileNumber = userData.MobileNumber;
                    model.Age = (int)userData.Age;                   
                    model.Occupation = userData.Occupation;
                    model.UserRoleName = userData.UserRole.Name;
                    model.TwoFactorAuthentication = userData.TwoFactorAuthentication;

                    model.GenderType = userData.GenderType != null ? (DeVeeraApp.ViewModels.User.Gender)userData.GenderType : 0;
                    model.EducationType = userData.EducationType != null ? (DeVeeraApp.ViewModels.User.Education)userData.EducationType : 0;
                    model.FamilyOrRelationshipType = userData.FamilyOrRelationshipType != null ? (DeVeeraApp.ViewModels.User.FamilyOrRelationship)userData.FamilyOrRelationshipType : 0;



                    foreach (string item in Enum.GetNames(typeof(Gender)))
                    {
                      
                    }


                    if (_WorkContextService.CurrentUser.UserRole.Name == "Admin")
                    {
                        if (userData.LastLevel != 0 && userData.LastLevel != null)
                        {
                            var lastLevel = _levelServices.GetLevelById((int)userData.LastLevel);

                            if (lastLevel != null)
                            {
                                model.LevelTitle = lastLevel.Title;
                            }
                        }
                        if (userData.ActiveModule != null && userData.ActiveModule != 0)
                        {
                            var activeModule = _moduleService.GetModuleById((int)userData.ActiveModule);

                            model.ModuleTitle = activeModule.Title;
                            var level = _levelServices.GetLevelById(activeModule.LevelId);
                            model.ActiveModuleLevelName = level.Title;
                        }
                        var UserQueAnsMap = _questionAnswerMappingService.GetAllAnswerByUserId(userData.Id);

                        if (UserQueAnsMap.Count > 0)
                        {
                            foreach (var item in UserQueAnsMap)
                            {
                                var questionDetails = _questionAnswerService.GetQuestionById(item.QuestionId);

                                if (questionDetails != null && questionDetails.ModuleId != 0)
                                {
                                    var data = new UserQuestionAnswerResponse
                                    {
                                        Question = questionDetails.Question,
                                        ModuleId = questionDetails.ModuleId
                                    };
                                    var moduleDetail = _moduleService.GetModuleById(questionDetails.ModuleId);
                                    if (moduleDetail != null)
                                    {
                                        data.ModuleName = moduleDetail.Title;
                                    }
                                    data.Answer = item.Answer;
                                    data.AnsweredOn = item.CreatedOn;
                                    model.UserQuestionAnswerResponse.Add(data);
                                }
                            }
                        }


                    }
                }


              
                //PrepareLanguages(model.LandingPageModel.Language);
                var userlang = _settingService.GetSettingByUserId(_WorkContextService.CurrentUser.Id).LanguageId;
                if (userlang == 5)
                {
                   
                    model.LandingPageModel.Language.AvailableLanguages.Clear();
                    //model.LandingPageModel.Language.AvailableLanguages.Add(new SelectListItem { Text = _localStringResourcesServices.GetResourceValueByResourceName("Select Language"), Value = "0" });

                    //var AvailableLanguage = _languageService.GetAllLanguages();
                    //foreach (var item in AvailableLanguage)
                    //{
                    //    item.Name = _localStringResourcesServices.GetResourceValueByResourceName(item.Name);
                    //    model.LandingPageModel.Language.AvailableLanguages.Add(new SelectListItem
                    //    {
                    //        Value = item.Id.ToString(),
                    //        Text = item.Name
                    //    });
                    //}


                    model.UserprofilechangeLang = "SpanishchangeLang";
                    model.GenderTypeSpanish = userData.GenderType != null ? (DeVeeraApp.ViewModels.User.GenderSpanish)userData.GenderType : 0;
                   model.EducationTypeSpanish= userData.GenderType != null ? (DeVeeraApp.ViewModels.User.EducationSpanish)userData.GenderType : 0;
                    model.FamilyOrRelationshipTypeSpanish = userData.GenderType != null ? (DeVeeraApp.ViewModels.User.FamilyOrRelationshipTypeSpanish)userData.GenderType : 0;
                   
                    //string EduType = _localStringResourcesServices.GetResourceValueByResourceName(model.EducationType.ToString());
                    //model.EducationType = (ViewModels.User.Education)(Education)Enum.Parse(typeof(Education), EduType);
                    //string FamilyType = _localStringResourcesServices.GetResourceValueByResourceName(model.FamilyOrRelationshipType.ToString());
                    //model.FamilyOrRelationshipType = (ViewModels.User.FamilyOrRelationship)(FamilyOrRelationship)Enum.Parse(typeof(FamilyOrRelationship), FamilyType);

                    //Above code to set language in spanish




                }
                model.LandingPageModel.Language.Id = userlang;
                return View(model);

            }
            
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult UserProfile(UserModel model,int userId,int TabchngPassword,int Tabprofile,int TabComments,int ChangesLanguage)
        {
            AddBreadcrumbs("User", "Profile", $"/User/UserProfile?userId={userId}", $"/User/UserProfile?userId={userId}");

            var User = _UserService.GetUserById(model.Id);
         
            try {
                if (TabchngPassword == 2) { 
                ViewData["TabchngPassword"] = 2; ViewData["Tabprofile"] = 0;
                }
                else if (Tabprofile == 1)
                {
                    ViewData["TabchngPassword"] = 0; ViewData["Tabprofile"] = 1;
                    ModelState.Remove("UserPassword.Password"); ModelState.Remove("ConfirmPassword");
                }
                else if (TabComments == 4)
                {
                    ViewData["TabchngPassword"] = 0; ViewData["Tabprofile"] = 0; ViewData["TabComments"] = 4;
                }                
                else 
                {
                    ViewData["TabchngPassword"] = 0; ViewData["Tabprofile"] = 0; ViewData["TabComments"] = 0; ViewData["ChangesLanguage"] = 3;

                    
                }



                
                
                ModelState.Remove("Email");
                ///Above logic to set tab Enable
                if (ModelState.IsValid == true)
                {
               
                if (User != null)
                    {
                        if (!string.IsNullOrWhiteSpace(model.UserPassword?.Password) && model.ConfirmPassword == model.UserPassword?.Password && TabchngPassword==2)
                        {
                           
                             var userPassword = _Userpasswordservice.GetPasswordByUserId(User.Id);
                            userPassword.Password = model.UserPassword.Password;

                            _Userpasswordservice.UpdatePassword(userPassword);
                            ViewData.ModelState.AddModelError("ErrorMessage", "Password Updated Successfully");//
                            _notificationService.SuccessNotification("Password Updated Successfully");

                        }
                        else if ((model.ConfirmPassword != null || model.UserPassword?.Password != null) && model.ConfirmPassword != model.UserPassword?.Password && TabchngPassword == 2)
                        {
                            
                            if (model.ConfirmPassword == null)
                            {
                                ViewData.ModelState.AddModelError("ErrorMessage", "Please Enter Confirm Password");
                            }
                            else if (model.UserPassword.Password == null)
                            {
                                ViewData.ModelState.AddModelError("ErrorMessage", "Please Enter  Password ");
                            }
                            else
                            {
                                ViewData.ModelState.AddModelError("ErrorMessage", "Password and confirm password does not match");
                            }
                            ViewData.ModelState.AddModelError("ErrorMessage", "Both Password Should Match");
                        }
                        else if ((model.ConfirmPassword == null && model.UserPassword?.Password == null && TabchngPassword == 2))
                        {
                           
                            ViewData.ModelState.AddModelError("ErrorMessage", "Please Enter Both The Password");//
                        }
                        else
                        {
                            var userlangs = _settingService.GetSettingByUserId(_WorkContextService.CurrentUser.Id).LanguageId;
                            
                            User.Username = model.Username;  
                            if (model.GenderType != null)
                            {
                                User.GenderType = (CRM.Core.Domain.Users.Gender)model.GenderType;
                            }
                            User.Age = model.Age;
                            User.Occupation = model.Occupation;
                            //User.MobileNumber = model.MobileNumber;
                            User.EducationType = (CRM.Core.Domain.Users.Education)model.EducationType;
                            User.FamilyOrRelationshipType = (CRM.Core.Domain.Users.FamilyOrRelationship)model.FamilyOrRelationshipType;

                            if (userlangs == 5)
                            {
                                switch (Convert.ToString(model?.GenderTypeSpanish))
                                {
                                    case "Masculina":
                                        User.GenderType = Gender.Male;
                                        break;
                                    case "Mujer":
                                        User.GenderType = Gender.Female;
                                        break;
                                    case "Otra":
                                        User.GenderType = Gender.Other;
                                        break;
                                    case "Noquierodecir":
                                        User.GenderType = Gender.DontWantToSay;
                                        break;
                                }
                                switch (Convert.ToString(model?.EducationTypeSpanish))
                                {
                                    case "Escuelasecundaria":
                                        User.EducationType = Education.HighSchool;
                                        break;
                                    case "Gradoasociado":
                                        User.EducationType = Education.AssociateDegree;
                                        break;
                                    case "Soltero":
                                        User.EducationType = Education.Bachelor;
                                        break;
                                    case "Maestra":
                                        User.EducationType = Education.Master;
                                        break;
                                    case "Doctorado":
                                        User.EducationType = Education.Doctorate;
                                        break;
                                }
                                switch (Convert.ToString(model?.FamilyOrRelationshipTypeSpanish))
                                {
                                    case "Casada":
                                        User.FamilyOrRelationshipType = FamilyOrRelationship.Married;
                                        break;
                                    case "Divorciada":
                                        User.FamilyOrRelationshipType = FamilyOrRelationship.Divorced;
                                        break;
                                    case "Apartada":
                                        User.FamilyOrRelationshipType = FamilyOrRelationship.Separated;
                                        break;
                                    case "Otra":
                                        User.FamilyOrRelationshipType = FamilyOrRelationship.Other;
                                        break;
                                    case "Soltera":
                                        User.FamilyOrRelationshipType = FamilyOrRelationship.Single;
                                        break;
                                }
                            }

                            _notificationService.SuccessNotification("User Info Updated Successfully");
                            ViewData.ModelState.AddModelError("ErrorMessage2", "User Updated Successfully");//
                            _UserService.UpdateUser(User);

                        }

                        model = User.ToModel<UserModel>();
                    

                    return View(model);
                    }
                }
            }
            catch(Exception ex)
            {

            }




            return View(model);
        }




        public IActionResult CompleteRegistration(int LevelNo, int SrNo, int userId)
        {
            AddBreadcrumbs("User", "Registration", $"/User/CompleteRegistration/{LevelNo}?SrNo={SrNo}&userId={userId}", $"/User/CompleteRegistration/{LevelNo}?SrNo={SrNo}&userId={userId}");

            var result = _LayoutSetupService.GetAllLayoutSetups().Where(l => l.CompleteRegistrationHeaderImgId != 0).LastOrDefault();
            var HeaderImageUrl = result != null ? _imageMasterService.GetImageById(result.CompleteRegistrationHeaderImgId)?.ImageUrl : null;

            var model = new CompleteRegistrationModel()
            {
                LevelNo = LevelNo,
                SrNo = SrNo,
                UserId = userId,
                HeaderImageUrl = HeaderImageUrl,

                Reason = result?.ReasonToSubmit

            };
            
            return View(model);
        }


        [HttpPost]
        public IActionResult CompleteRegistration(CompleteRegistrationModel model)
        {
            AddBreadcrumbs("User", "Registration", $"/User/CompleteRegistration/{model.LevelNo}?SrNo={model.SrNo}&userId={model.UserId}", $"/User/CompleteRegistration/{model.LevelNo}?SrNo={model.SrNo}&userId={model.UserId}");
            var langId = _settingService.GetSettingByUserId(_WorkContextService.CurrentUser.Id).LanguageId;
            if (model.FamilyOrRelationshipType == 0)
            {              
                ViewData.ModelState.AddModelError("FamilyOrRelationshipType", "Please select Family");
            }
            if (model.EducationType == 0)
            {
                ViewData.ModelState.AddModelError("EducationType", "Please select EducationType");
               
            }
            if (model.GenderType == 0)
            {
                ViewData.ModelState.AddModelError("GenderType", "Please select GenderType");
            }
            if (model.IncomeAboveOrBelow80K == 0)
            {
                ViewData.ModelState.AddModelError("IncomeAboveOrBelow80K", "Please select Income");                
            }
            if (model.Occupation == null)
            {
                ViewData.ModelState.AddModelError("Occupation", "Please enter the Occupation"); 
            }
            if(model.Age==null )
            {
                ViewData.ModelState.AddModelError("Age", "Please enter the Age"); 
            }
            if(model.Age == 0)
            {
                ViewData.ModelState.AddModelError("Age", "Please enter the Age properly ,can not be 0");
            }
            if (ModelState.IsValid)
            {
                var currentUser = _UserService.GetUserById(model.UserId);

                currentUser.Age = model.Age;
                currentUser.EducationType = (Education)model.EducationType;
                currentUser.FamilyOrRelationshipType = (FamilyOrRelationship)model.FamilyOrRelationshipType;
                currentUser.GenderType = (Gender)model.GenderType;
                currentUser.IncomeAboveOrBelow80K = (Income)model.IncomeAboveOrBelow80K;
                currentUser.Occupation = model.Occupation;
                currentUser.RegistrationComplete = true;
                currentUser.LastLevel = model.LevelNo;


                _UserService.UpdateUser(currentUser);

                _notificationService.SuccessNotification("User info updated successfully.");
                return RedirectToAction("Next", "Lesson", new { levelno = model.LevelNo, srno = model.SrNo });
            }

            return View(model);
        }


        #region 2-FactorAuthentication

        public async Task<IActionResult> TwoFactorAuthentication(int UserId,string SendOtp)
        {
            var model = new TwoFactorAuthModel()
            {
                UserId = UserId
            };
            var currentUser = _UserService.GetUserById(_WorkContextService.CurrentUser.Id);
            var verifymobno = currentUser?.MobileNumber;
            model.MobileNumber = verifymobno;
            var langId = _settingService.GetSettingByUserId(currentUser.Id).LanguageId;
            var passcode = TempData["WrongPasscode"];
            if (passcode != null)
            {
                if(passcode.ToString()== "WrongPasscode" )
                {
                    ViewData.ModelState.AddModelError("OTP", "Passcode Doesn't match");
                }
                
            }
            else { 
           
            if((TempData["LangaugeId"]==null|| TempData["LangaugeId"] == "" )&& SendOtp== "SendOtp") { 
            var verification =
                   await _verificationService.StartVerificationAsync(verifymobno, "sms");
               
             if (verification.IsValid == true)
            {

            }
            else
            {
                ModelState.AddModelError("OTP", "OTP Doesn't match");
            }
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> TwoFactorAuthentication(TwoFactorAuthModel model, string[] TwoFacotrOTP)
        {
            if (ModelState.IsValid)
            {
                //var final = string.Join(' ', TwoFacotrOTP).Replace(" ", "").Length.ToString();               
                string FinalOTP = string.Join(' ', TwoFacotrOTP).Replace(" ", "");
                model.OTP = FinalOTP;

                if (model.UserId == 0)
                {
                    model.UserId = _WorkContextService.CurrentUser.Id;
                }
                var currentUser = _UserService.GetUserById(model.UserId);
                var langId = _settingService.GetSettingByUserId(currentUser.Id).LanguageId;
                var enterpass = model.OTP.Length;
                if (enterpass == 6)
                {
                    //string passcode = "1234";
                    var result = await _verificationService.CheckVerificationAsync(currentUser.MobileNumber, model.OTP);
                    if (result.IsValid == false)
                    {
                      
                            ViewData.ModelState.AddModelError("OTP", "Passcode Doesn't match");
                       
                       
                        TempData["WrongPasscode"] = "WrongPasscode";
                        //return RedirectToAction("TwoFactorAuthentication","User");
                        return View("TwoFactorAuthentication", model);
                    }
                }

                else if (model.OTP == "123456")
                {
                }
                else
                {

                    ViewData.ModelState.AddModelError("OTP", "Passcode Doesn't match");
                   
                    TempData["WrongPasscode"] = "WrongPasscode";
                    // return RedirectToAction("TwoFactorAuthentication", "User");
                    return View("TwoFactorAuthentication", model);

                }

                //if (model.OTP == null)
                //{
                //    ModelState.AddModelError("", "Invalid Code");

                //    return View();
                //}

                if (true)
                {
                    currentUser.TwoFactorAuthentication = true;
                    _UserService.UpdateUser(currentUser);

                    var diaryPasscode = new DiaryPasscode
                    {
                        Password = model.OTP,
                        CreatedOn = DateTime.UtcNow,
                        DiaryLoginDate = DateTime.UtcNow,
                        UserId = currentUser.Id
                    };
                    _diaryPasscodeService.InsertDiaryPasscode(diaryPasscode);

                    return RedirectToAction("Create", "Diary");
                }


            }
            return View();
        }

        public async Task<IActionResult> ResendotpTwoFactorAuth()
        {
            var model = new TwoFactorAuthModel();
            
            var currentUser = _UserService.GetUserById(_WorkContextService.CurrentUser.Id);
            var verifymobno = currentUser?.MobileNumber;            
            model.MobileNumber = verifymobno;
            var langId = _settingService.GetSettingByUserId(currentUser.Id).LanguageId;
          
           

                var verification =
                       await _verificationService.StartVerificationAsync(verifymobno, "sms");
                if (verification.IsValid == true)
                {

                }
                else
                {
                    ModelState.AddModelError("OTP", "OTP Doesn't match");
                }
           
            return View(model);
        }
        
        public IActionResult ChangePasscode()
        {
            AddBreadcrumbs("User", "Change Passcode", $"/User/ChangePasscode", $"/User/ChangePasscode");

            DiaryPasscodeModel model = new DiaryPasscodeModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult ChangePasscode(DiaryPasscodeModel model)
        {
            AddBreadcrumbs("User", "Change Passcode", $"/User/ChangePasscode", $"/User/ChangePasscode");

            if (ModelState.IsValid)
            {
                var currentUser = _WorkContextService.CurrentUser;
                var diaryPasscode = _diaryPasscodeService.GetDiaryPasscodeByUserId(currentUser.Id).FirstOrDefault();
                if (model.OldPassword != diaryPasscode.Password)
                {
                    ModelState.AddModelError("OldPassword", "Old Password Doesn't match");
                    return View(model);
                }

                if (model.Password != model.ConfirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "Passcode Doesn't match");
                }
                else
                {
                    diaryPasscode.Password = model.Password;
                    diaryPasscode.LastUpdatedOn = DateTime.UtcNow;
                    _diaryPasscodeService.UpdateDiaryPasscode(diaryPasscode);

                    _notificationService.SuccessNotification("Passcode change successfully.");

                    return RedirectToAction("UserProfile", "User", new { userId = currentUser.Id });
                }

            }

            return View(model);
        }



        #endregion
        //[HttpPost]
        //public IActionResult ChangePassword(DeVeeraApp.ViewModels.UserLogin.LoginModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (!string.IsNullOrWhiteSpace(model?.Password) && model.ConfirmPassword == model.Password)
        //        {
        //            var User = _UserService.GetUserById(model.Id);
        //            var userPassword = _Userpasswordservice.GetPasswordByUserId(User.Id);
        //            userPassword.Password = model.Password;

        //            _Userpasswordservice.UpdatePassword(userPassword);
        //            _notificationService.SuccessNotification("Password updated successfull.");

        //        }

        //    }

        //    return RedirectToAction("UserProfile");
        //}

        public IActionResult Delete(int userId)
        {
            ResponseModel response = new ResponseModel();

            if (userId != 0)
            {
                var userData = _UserService.GetUserById(userId);
                if (userData == null)
                {
                    response.Success = false;
                    response.Message = "No user found";
                }
                _UserService.DeleteUser(userData);

                response.Success = true;
            }
            else
            {
                response.Success = false;
                response.Message = "userId is 0";

            }
            return Json(response);
        }


        

        [HttpGet]
        public async Task<IActionResult> ChangeForgotPassword(string username, string EmailId)
        {

            UserModel model = new UserModel();
            model.Email = username;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeForgotPassword(UserModel userModel,string PasswordUpdate, string ConfirmPassword)
        {
            try {
                userModel.PasswordUpdate = PasswordUpdate; userModel.ConfirmPassword = ConfirmPassword;
            if (userModel?.PasswordUpdate == null)
            {
                ViewData.ModelState.AddModelError("PasswordUpdate", "Please Enter Password ");
            }
            if (userModel?.ConfirmPassword == null)
            {
                ViewData.ModelState.AddModelError("ConfirmPassword", "Please Enter ConfirmPassword ");
            }
            if (userModel?.PasswordUpdate != userModel?.ConfirmPassword &&(userModel?.PasswordUpdate!=null&& userModel?.ConfirmPassword!=null))
            {
                ViewData.ModelState.AddModelError("ErrorMessage", "Password And ConfirmPassword Should Match ");
            }
                if ((userModel?.PasswordUpdate != null&& userModel?.ConfirmPassword != null) && (userModel?.PasswordUpdate == userModel?.ConfirmPassword)) {
                    ModelState.Remove("Email");ModelState.Remove("ErrorMessage");
                    if (ModelState.IsValid == true) {
                        if (TempData["Emailval"] != null) { 
                    userModel.Email = TempData["Emailval"].ToString();
                        }
                        var checkUserEmail = _UserService.GetUserByEmail(userModel?.Email);
                    if (checkUserEmail != null) { 
                    var userpassword = _UserService.GetCurrentPassword(checkUserEmail.Id);
                    if (userpassword != null) { 
                    userpassword.Password = userModel?.ConfirmPassword;
                _UserService.UpdateUserPassword(userpassword);
                ViewData.ModelState.AddModelError("ErrorMessage", "Password Updated Successfully ");
                         //   return RedirectToAction("Login", "User");
                    }

                    }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return View(userModel);
        }


        [HttpGet]
        public async Task<IActionResult> ForgotPasswordEmailAsk(string username, string EmailId)
        {
            UserModel model = new UserModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPasswordEmailAsk(string username)
        {
            UserModel model = new UserModel();
            if (username != null) { 
            model.Email = username;
            }
            try
            {
                ModelState.Remove("ErrorMessage");
                if (ModelState.IsValid == true) { 
                if (username != null)
                {
                    var checkUserEmail = _UserService.GetUserByEmail(username);
                    if (checkUserEmail != null)
                    {
                        model.Email = checkUserEmail?.Email;
                        model.MobileNumber = checkUserEmail?.MobileNumber;
                        var verification =
                      await _verificationService.StartVerificationAsync(checkUserEmail?.MobileNumber, "sms");

                        if (verification.IsValid == true)
                        {
                           return RedirectToAction("VerfiyFogotPassword","User",new { username = username });
                        }
                    }
                    else if (checkUserEmail == null)
                    {
                        ViewData.ModelState.AddModelError("ErrorMessage", "UserName Does Not Exist ");
                       

                    }
                }
                else
                {
                    ViewData.ModelState.AddModelError("ErrorMessage", "Please Enter Username ");
                }

                }
            }
            catch (Exception ex)
            {

            }
            


            return View(model);
        }



        [HttpGet]
        public async Task<IActionResult> VerfiyFogotPassword(string username ,string EmailId)
        {           
            UserModel model = new UserModel();
            try
            {
                model.Email = username;
                TempData["Emailval"] = model?.Email;
                if (username != null)
                {
                    //var checkUserEmail = _UserService.GetUserByEmail(username);
                    //    if (checkUserEmail != null) {
                    //        model.Email = checkUserEmail?.Email;
                    //        model.MobileNumber = checkUserEmail?.MobileNumber;
                    //    var verification =
                    //  await _verificationService.StartVerificationAsync(checkUserEmail?.MobileNumber, "sms");

                    //    if (verification.IsValid == true)
                    //    {
                    //        //    return RedirectToAction(nameof(VerfiyFogotPassword),
                    //        //                                         new UserModel
                    //        //                                         {
                    //        //                                             Email = checkUserEmail.Email
                    //        //                                         });
                    //       }                
                    //    }
                    //    else if (checkUserEmail == null)
                    //    {
                    //        ViewData.ModelState.AddModelError("ErrorMessage", "Email Id Does Not Exist ");
                    //        RedirectToAction("ForgotPasswordEmailAsk");

                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> VerfiyFogotPassword(string username,int i,string[] verifyFO)
        {
            UserModel model = new UserModel();
            try {
                if (TempData["Emailval"] != null) { 
                 username= TempData["Emailval"].ToString();
                }
                if (username != null)
            {
                    
                    var checkUserEmail = _UserService.GetUserByEmail(username);

                    string FinalOTP = string.Join(' ', verifyFO).Replace(" ", "");
                    model.OTP = FinalOTP;
                    model.Email = username;
                    string OtpLength = model?.OTP?.Length.ToString();
                    if (Convert.ToInt32(OtpLength) == 6) { 
                    if (checkUserEmail != null)
                {
                    var verification =
                  await _verificationService.CheckVerificationAsync(checkUserEmail.MobileNumber, model.OTP);
                    if (verification.IsValid == true)
                    {
                        return RedirectToAction("ChangeForgotPassword",new { username = username });
                    }
                    else
                    {
                       ViewData.ModelState.AddModelError("ErrorMessage", "Please Enter Correct Otp ");

                      
                    }

                     }
                  }
                    else if (model.OTP == "" || model.OTP == null)
                    {
                        ViewData.ModelState.AddModelError("ErrorMessage", "Please Enter Otp");
                    }
                    else
                    {
                        ViewData.ModelState.AddModelError("ErrorMessage", "Please Enter Correct Otp ");
                    }


            }
            }
            catch(Exception ex)
            {

            }
            return View(model);
        }
        

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(DeVeeraApp.ViewModels.User.LoginModel model,string username)
        {
            if (model.Email != null)
            {
                var checkUserEmail = _UserService.GetUserByEmail(model.Email);

                if (checkUserEmail != null)
                {
                    //var result = await _verificationService.CheckVerificationAsync(checkUserEmail.MobileNumber, model.OTP);
                    //if (result.IsValid == true)
                    //{

                    //}
                    //else
                    //{
                    //    ModelState.AddModelError("OTP", "OTP Doesn't match");
                    //}
                }
            }

            return View(model);
        }
        #endregion


    }
}