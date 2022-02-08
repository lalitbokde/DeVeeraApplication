using CRM.Core.Domain.Emotions;
using System;
using System.Collections.Generic;

namespace CRM.Core.Domain.Users
{
    public partial class User : BaseEntity
    {

      
        private ICollection<User_Emotion_Mapping> _User_Emotion_Mappings;

        /// <summary>
        /// Ctor
        /// </summary>
        public User()
        {
            this.UserGuid = Guid.NewGuid();
        }

        /// <summary>
        /// Gets or sets the User GUID
        /// </summary>
        public Guid UserGuid { get; set; }

        /// <summary>
        /// Gets or sets the username
        /// </summary>
        public string Username { get; set; }
        public string MobileNumber { get; set; }
        public Gender? GenderType { get; set; }
        public int? Age { get; set; }
        public string Occupation { get; set; }
        public Education? EducationType { get; set; }
        public Income? IncomeAboveOrBelow80K { get; set; }
        public FamilyOrRelationship? FamilyOrRelationshipType { get; set; }

        /// <summary>
        /// Gets or sets the username
        /// </summary>
        public string Alias { get; set; }


        /// <summary>
        /// Gets or sets the email
        /// </summary>
        public string Email { get; set; }

        public string ImageURL { get; set; }
        public string EmailToRevalidate { get; set; }

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
        public bool Active { get; set; }

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
        public int UserRoleId { get; set; }
        
        /// <summary>
        /// Gets or sets the User Role Id
        /// </summary>
        public int? ParentUserId { get; set; }

        public int? LastLevel { get; set; }

        public int? ActiveModule { get; set; }
        public bool RegistrationComplete { get; set; }

        public bool TwoFactorAuthentication { get; set; }

        public bool IsAllow { get; set; }

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

        public string ProfileImageUrl { get; set; }
        

        #region Navigation 
        /// <summary>
        /// Default billing address
        /// </summary>
        public int? UserAvailabilityId { get; set; }

        /// <summary>
        /// Default User Role
        /// </summary>
        public virtual UserRole UserRole { get; set; }

      
        public virtual ICollection<User_Emotion_Mapping> User_Emotion_Mappings
        {
            get { return _User_Emotion_Mappings ??= new List<User_Emotion_Mapping>(); }
            protected set { _User_Emotion_Mappings = value; }
        }

        #endregion
    }

    public enum Gender
    {
        Male = 1,
        Female = 2,
        Other = 3,
        DontWantToSay = 4
    }

    public enum Income
    {
        IncomeAbove80K = 1,
        IncomeBelow80K= 2
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
