using CRM.Core.Domain.Users;
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

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please enter username")]
        public string Email { get; set; }
        
        [DataType(DataType.PhoneNumber),MinLength(10,ErrorMessage ="Phone must size must be 10")]     
        [Required(ErrorMessage ="Please enter contact number")]
        public string MobileNumber { get; set; }
        public Gender? GenderType { get; set; }
        public int Age { get; set; }
        public string Occupation { get; set; }
        public Education EducationType { get; set; }
        public Income IncomeAboveOrBelow80K { get; set; }
        public FamilyOrRelationship FamilyOrRelationshipType { get; set; }
        public string ImageUrl { get; set; }
        public string BannerImageUrl { get; set; }
        public UserPassword UserPassword { get; set; }
        public LandingPageModel LandingPageModel { get; set; }
        [NotMapped]
        public string OldPassword { get; set; }

        //[NotMapped]
        //[Required(ErrorMessage ="Enter correct password")]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,25}$",
        //ErrorMessage = "The password length must be minimum 8 characters.\n The password must contain one or more special characters,uppercase characters,lowercase characters,numeric values..!!")]
        public string ConfirmPassword { get; set; }
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

     

        /// <summary>
        /// Gets or sets the username
        /// </summary>
        public string Companyname { get; set; }

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

    public enum FamilyOrRelationship
    {
        Married = 1,
        Divorced = 2,
        Separated = 3,
        Other = 4,
        Single = 5
    }
}
