using System;
using System.ComponentModel.DataAnnotations;

namespace CRM.Core.Domain.Users
{
    /// <summary>
    /// Represents a User password
    /// </summary>
    public partial class UserPassword : BaseEntity
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public UserPassword()
        {
            this.PasswordFormat = PasswordFormat.Clear;
        }

        /// <summary>
        /// Gets or sets the User identifier
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the password
        /// </summary>
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$",
         ErrorMessage = "Password must be between 6 and 20 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the password format identifier
        /// </summary>
        public int PasswordFormatId { get; set; }

        /// <summary>
        /// Gets or sets the password salt
        /// </summary>
        public string PasswordSalt { get; set; }

        /// <summary>
        /// Gets or sets the date and time of entity creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the password format
        /// </summary>
        public PasswordFormat PasswordFormat
        {
            get { return (PasswordFormat)PasswordFormatId; }
            set { this.PasswordFormatId = (int)value; }
        }

        /// <summary>
        /// Gets or sets the User
        /// </summary>
        public virtual User User { get; set; }
    }
}
