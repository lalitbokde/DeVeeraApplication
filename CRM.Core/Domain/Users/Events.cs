using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Domain.Users
{
    /// <summary>
    /// User logged-in event
    /// </summary>
    public class UserLoggedinEvent
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="User">User</param>
        public UserLoggedinEvent(User User)
        {
            this.User = User;
        }

        /// <summary>
        /// User
        /// </summary>
        public User User
        {
            get; private set;
        }
    }
    /// <summary>
    /// "User is logged out" event
    /// </summary>
    public class UserLoggedOutEvent
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="User">User</param>
        public UserLoggedOutEvent(User User)
        {
            this.User = User;
        }

        /// <summary>
        /// Get or set the User
        /// </summary>
        public User User { get; private set; }
    }

    /// <summary>
    /// User registered event
    /// </summary>
    public class UserRegisteredEvent
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="User">User</param>
        public UserRegisteredEvent(User User)
        {
            this.User = User;
        }

        /// <summary>
        /// User
        /// </summary>
        public User User
        {
            get; private set;
        }
    }

    /// <summary>
    /// User password changed event
    /// </summary>
    public class UserPasswordChangedEvent
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="password">Password</param>
        public UserPasswordChangedEvent(UserPassword password)
        {
            this.Password = password;
        }

        /// <summary>
        /// User password
        /// </summary>
        public UserPassword Password { get; private set; }
    }
}
