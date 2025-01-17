﻿using CRM.Core.Domain.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeVeeraApp.ViewModels.User
{
    public partial class UserModel : BaseEntityModel
    {
        public UserModel()
        {
            this.AvailableUsers = new List<SelectListItem>();
            this.UserQuestionAnswerResponse = new List<UserQuestionAnswerResponse>();
            this.LandingPageModel = new LandingPageModel();
        }

        public string Username { get; set; }
        public string countryCode { get; set; }

        

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Enter email address")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Incorrect Email Format")]

        public string Email { get; set; }
        
        //[DataType(DataType.PhoneNumber)]
        //[Required]
        public string MobileNumber { get; set; }


        public string ErrorMessage2 { get; set; }
        public string ErrorMessage { get; set; }

        public string UserprofilechangeLang { get; set; }
        public Gender? GenderType { get; set; }

        public GenderSpanish? GenderTypeSpanish { get; set; }
        public int Age { get; set; }
        public string Occupation { get; set; }
        public Education EducationType { get; set; }
        public EducationSpanish EducationTypeSpanish { get; set; }

        public Income IncomeAboveOrBelow80K { get; set; }
        public FamilyOrRelationship FamilyOrRelationshipType { get; set; }
        public FamilyOrRelationshipTypeSpanish FamilyOrRelationshipTypeSpanish { get; set; }
        
        public string ImageUrl { get; set; }
        public string BannerImageUrl { get; set; }
        public UserPassword UserPassword { get; set; }
        public LandingPageModel LandingPageModel { get; set; }
        [NotMapped]
        public string OldPassword { get; set; }

        //[NotMapped]
        [Required(ErrorMessage = "Please Enter Confirm Password ")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,25}$",
        ErrorMessage = "The password length must be minimum 8 characters.The password must contain one or more special characters,uppercase characters,lowercase characters,numeric values..!!")]


        //[DataType(DataType.Password)]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        //[Display(Name = "Password")]
        //[RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        public string ConfirmPassword { get; set; }




        [Required(ErrorMessage = "Please Enter Password ")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,25}$",
         ErrorMessage = "The password length must be minimum 8 characters.The password must contain one or more special characters,uppercase characters,lowercase characters,numeric values..!!")]


        //[DataType(DataType.Password)]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        //[Display(Name = "Password")]
        //[RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        public string PasswordUpdate { get; set; }


       
        [Display(Name = "Profile Picture")]
        public string ProfileImage { get; set; }

        public string ProfileImageUrl { get; set; }

        [NotMapped]
        public bool TwoFactorAuthentication { get; set; }
        public string CompanyName { get; set; }
        public int? LastLevel { get; set; }
        public int? ActiveModule { get; set; }
        public string LevelTitle { get; set; }
        public string ModuleTitle { get; set; }
        public string ActiveModuleLevelName { get; set; }
        public string UserRoleName { get; set; }
        public List<UserQuestionAnswerResponse>  UserQuestionAnswerResponse { get; set; }
        /// <summary>
        /// Gets or sets the username
        /// </summary>
        public string Alias { get; set; }

        //billing info
        

        public string ActiveTab { get; set; }

        public int UserRoleId { get; set; }
        public UserRoleModel UserRole { get; set; }
        public bool Active { get; set; }
     
        public IList<SelectListItem> AvailableUsers { get; set; }
      

        /// <summary>
        /// Gets or sets the User GUID
        /// </summary>
        public Guid UserGuid { get; set; }

     


        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the email that should be re-validated. Used in scenarios when a User is already registered and wants to change an email address.
        /// </summary>
        public string EmailToRevalidate { get; set; }

        /// <summary>
        /// Gets or sets the admin comment
        /// </summary>
        public string AdminComment { get; set; }

      

        /// <summary>
        /// Gets or sets a value indicating whether the User is required to re-login
        /// </summary>
        public bool RequireReLogin { get; set; }

        /// <summary>
        /// Gets or sets a value indicating number of failed login attempts (wrong password)
        /// </summary>
        public int FailedLoginAttempts { get; set; }

        /// <summary>
        /// Gets or sets the date and time until which a User cannot login (locked out)
        /// </summary>
        public DateTime? CannotLoginUntilDateUtc { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the User is active
        /// </summary>
    

        /// <summary>
        /// Gets or sets a value indicating whether the User has been deleted
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the User account is system
        /// </summary>
        public bool IsSystemAccount { get; set; }

        /// <summary>
        /// Gets or sets the User system name
        /// </summary>
        public string SystemName { get; set; }
        /// <summary>
        /// Gets or sets the User Role Id
        /// </summary>
    

        /// <summary>
        /// Gets or sets the User Role Id
        /// </summary>
        public int? ParentUserId { get; set; }

        /// <summary>
        /// Gets or sets the last IP address
        /// </summary>
        public string LastIpAddress { get; set; }

        /// <summary>
        /// Gets or sets the date and time of entity creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of last login
        /// </summary>
        public DateTime? LastLoginDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of last activity
        /// </summary>
        public DateTime LastActivityDateUtc { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string OTP { get; set; }



    }




    public class UserQuestionAnswerResponse
    {
        public int ModuleId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string ModuleName { get; set; }
        public DateTime AnsweredOn { get; set; }
    }
    public enum Gender
    {
        Male = 1,
        Female = 2,
        Other = 3,
        [Display(Name = "Dont want to say.")]
        DontWantToSay = 4
    }

    public enum GenderSpanish
    {
        Masculina = 1,
        Mujer = 2,
        Otra = 3,
        [Display(Name = "No quiero decir.")]
        Noquierodecir = 4
    }

    public enum Income
    {
        IncomeAbove80K = 1,
        IncomeBelow80K = 2
    }

    public enum Education
    {
        HighSchool = 1,
        AssociateDegree = 2,
        Bachelor = 3,
        Master = 4,
        Doctorate = 5
    }

    public enum EducationSpanish
    {
        Escuelasecundaria = 1,
        Gradoasociado = 2,
        Soltero = 3,
        Maestra = 4,
        Doctorado = 5
    }

    public enum FamilyOrRelationship
    {
        Married = 1,
        Divorced = 2,
        Separated = 3,
        Other = 4,
        Single = 5
    }
    public enum FamilyOrRelationshipTypeSpanish
    {
        Casada = 1,
        Divorciada = 2,
        Apartada = 3,
        Otra = 4,
        Soltera = 5
    }
    
}
