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
                                  ITranslationService translationService
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
        }

        #endregion

        #region Utilities


        public virtual void PrepareLanguages(LanguageModel model)
        {

            model.AvailableLanguages.Add(new SelectListItem { Text = "Select Language", Value = "0" });
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
            model.BannerImageUrl = data?.BannerTwoImageId > 0 ? _imageMasterService.GetImageById(data.BannerTwoImageId)?.ImageUrl : null;
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Login(DeVeeraApp.ViewModels.User.LoginModel model, string returnUrl, bool captchaValid)
        {

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
            var data = _LayoutSetupService.GetAllLayoutSetups().FirstOrDefault();
            model.BannerImageUrl = data?.BannerOneImageId > 0 ? _imageMasterService.GetImageById(data.BannerOneImageId)?.ImageUrl : null;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SendOTP(UserModel model)
        {
            if (model.countryCode == null)
            {
                ModelState.AddModelError("countryCode", "please select country code.!!!");
                return RedirectToAction("Register", "User");
            }
            if (model.MobileNumber == null)
            {
                ModelState.AddModelError("MobileNumber", "please enter your mobile No.!!!");
                return RedirectToAction("Register", "User");

            }

            if (_UserService.GetUserByEmail(model.Email) != null)
            {
                TempData["Email"] = "Email Already Registered";
                return RedirectToAction("Register");
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
                    return RedirectToAction("Register", "User");
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
                TempData["Message"] = "Mobile Number is already Registered";
                return RedirectToAction("Register");
            }

        }



        public IActionResult VerifyOTP(UserModel model)
        {
            UserPassword password = new UserPassword
            {
                Password = model.ConfirmPassword,
                CreatedOnUtc = DateTime.UtcNow,
            };
            model.UserPassword = password;
            if (model.ErrorMessage != null)
            {
                ModelState.AddModelError("ErrorMessage", "Wrong code. Try again..!!!");
            }
            return View(model);
        }

       
        public async Task<VerificationResult> ResendOTP(string channel)
        {
            var user = _UserService.GetUserById(_WorkContextService.CurrentUser.Id);
            return await _verificationService.StartVerificationAsync(user.MobileNumber, channel);

           
            // return new VerificationResult(new List<string> { "Your phone number is already verified" });
        }


        [HttpPost]
        public async Task<IActionResult> VerifyOTP(UserModel model, string[] OTP)
        {
       
            var final = string.Join(' ', OTP).Replace(" ", "").Length.ToString();
            if (final == "6") { 
            string FinalOTP = string.Join(' ', OTP).Replace(" ", "");
            var result = await _verificationService.CheckVerificationAsync(model.MobileNumber, FinalOTP);
                if (result.IsValid== false)
                {
                    ModelState.AddModelError("ErrorMessage", "Wrong code. Try again..!!!");
                    model.ErrorMessage = "Wrong Passcode";
                    return RedirectToAction("VerifyOTP",model); 
                }
            if (true)
            {
               ModelState.Remove("UserModel.ErrorMessage");
                if (ModelState.IsValid)
                {
                    if (model.countryCode == null)
                    {
                        ModelState.AddModelError("MobileNumber", "please select country code.!!!");
                            
                        }
                    if (model.MobileNumber == null)
                    {
                        ModelState.AddModelError("MobileNumber", "please enter your mobile No.!!!");
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

                                    _notificationService.SuccessNotification("User registered successfull.");
                                    
                                         //return RedirectToAction("Index", "Dashboard");
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
                        TempData["Email"] = "Email Already Exists";
                        return RedirectToAction("Register");
                    }

                }
                else
                {
                    ModelState.AddModelError("Email", "Email Already Exists");
                   

                }
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

                                _notificationService.SuccessNotification("User registered successfull.");

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

        public IActionResult ForgetPassword()
        {
            return View();
        }


        public IActionResult UserProfile(int userId)
        {
            AddBreadcrumbs("User", "Profile", $"/User/UserProfile?userId={userId}", $"/User/UserProfile?userId={userId}");
            var model = new UserModel();

            if (userId != 0)
            {

                var userData = _UserService.GetUserById(userId);
                if (userData != null)
                {
                    model.Id = userData.Id;
                    model.Email = userData.Email;
                    model.MobileNumber = userData.MobileNumber;
                    model.Age = (int)userData.Age;
                    model.GenderType = userData.GenderType != null ? (DeVeeraApp.ViewModels.User.Gender)userData.GenderType : 0;
                    model.EducationType = userData.EducationType != null ? (DeVeeraApp.ViewModels.User.Education)userData.EducationType : 0;
                    model.FamilyOrRelationshipType = userData.FamilyOrRelationshipType != null ? (DeVeeraApp.ViewModels.User.FamilyOrRelationship)userData.FamilyOrRelationshipType : 0;
                    model.Occupation = userData.Occupation;
                    model.UserRoleName = userData.UserRole.Name;
                    model.TwoFactorAuthentication = userData.TwoFactorAuthentication;

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
                PrepareLanguages(model.LandingPageModel.Language);
                return View(model);

            }
            PrepareLanguages(model.LandingPageModel.Language);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult UserProfile(UserModel model)
        {
            AddBreadcrumbs("User", "Profile", $"/User/UserProfile?userId={model.Id}", $"/User/UserProfile?userId={model.Id}");

            var User = _UserService.GetUserById(model.Id);

            if (ModelState.IsValid)
            {
                if (User != null)
                {
                    if (!string.IsNullOrWhiteSpace(model.UserPassword?.Password) && model.ConfirmPassword == model.UserPassword.Password)
                    {
                        var userPassword = _Userpasswordservice.GetPasswordByUserId(User.Id);
                        userPassword.Password = model.UserPassword.Password;

                        _Userpasswordservice.UpdatePassword(userPassword);
                        _notificationService.SuccessNotification("Password updated successfull.");
                    }
                    else
                    {
                        User.Username = model.Username;
                        User.GenderType = (CRM.Core.Domain.Users.Gender)model.GenderType;
                        User.Age = model.Age;
                        User.Occupation = model.Occupation;
                        User.EducationType = (CRM.Core.Domain.Users.Education)model.EducationType;
                        User.FamilyOrRelationshipType = (CRM.Core.Domain.Users.FamilyOrRelationship)model.FamilyOrRelationshipType;
                        _notificationService.SuccessNotification("User info updated successfull.");

                        _UserService.UpdateUser(User);

                    }

                    model = User.ToModel<UserModel>();

                    return View(model);
                }

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

                _notificationService.SuccessNotification("User info updated successfull.");
                return RedirectToAction("Next", "Lesson", new { levelno = model.LevelNo, srno = model.SrNo });
            }

            return View(model);
        }


        #region 2-FactorAuthentication

        public async Task<IActionResult> TwoFactorAuthentication(int UserId)
        {
            var model = new TwoFactorAuthModel()
            {
                UserId = UserId
            };
            var currentUser = _UserService.GetUserById(_WorkContextService.CurrentUser.Id);
            var verifymobno = currentUser?.MobileNumber;
            model.MobileNumber = verifymobno;
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

        [HttpPost]
        public async Task<IActionResult> TwoFactorAuthentication(TwoFactorAuthModel model)
        {
            if (ModelState.IsValid)
            {
                var currentUser = _UserService.GetUserById(model.UserId);
                var enterpass = model.OTP.Length;
                if (enterpass==6) {
                    //string passcode = "1234";
                    var result = await _verificationService.CheckVerificationAsync(currentUser.MobileNumber, model.OTP);
                    if (result.IsValid == false)
                    {
                        ModelState.AddModelError("Passcode", "Passcode Doesn't match");
                    }
                }
                else
                {
                    ModelState.AddModelError("Passcode", "Add Proper Passcode !! ");
                }
               
                //if (model.OTP == null)
                //{
                //    ModelState.AddModelError("", "Invalid Code");

                //    return View();
                //}
                    
                    if (true) {
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

                    return RedirectToAction("ChangePasscode", "Diary");
                    }
                

            }
            return View();
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

        //[HttpPost]
        //public IActionResult ForgetPassword(DeVeeraApp.ViewModels.User.LoginModel model)
        //{
        //    if(model.Email != null)
        //    {
        //        var checkUserEmail = _UserService.GetUserByEmail(model.Email);

        //        if(checkUserEmail != null)
        //        {

        //        }
        //    }

        //    return View();
        //}
        #endregion


    }
}