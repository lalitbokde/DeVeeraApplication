
using CRM.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Core.Domain.Users
{
    /// <summary>
    /// User extensions
    /// </summary>
    public static class UserExtensions
    {
        #region User role

        /// <summary>
        /// Gets a value indicating whether User is in a certain User role
        /// </summary>
        /// <param name="User">User</param>
        /// <param name="UserRoleSystemName">User role system name</param>
        /// <param name="onlyActiveUserRoles">A value indicating whether we should look only in active User roles</param>
        /// <returns>Result</returns>
        public static bool IsInUserRole(this User User,
            string UserRoleSystemName, bool onlyActiveUserRoles = true)
        {
            if (User == null)
                throw new ArgumentNullException(nameof(User));

            if (string.IsNullOrEmpty(UserRoleSystemName))
                throw new ArgumentNullException(nameof(UserRoleSystemName));

            var result = ((!onlyActiveUserRoles || User.UserRole.Active) && (User.UserRole.SystemName == UserRoleSystemName)); 
                //.(cr => (!onlyActiveUserRoles || cr.Active) && (cr.SystemName == UserRoleSystemName)) != null;
            return result;
        }


        /// <summary>



        /// <summary>
        /// Gets a value indicating whether User is registered
        /// </summary>
        /// <param name="User">User</param>
        /// <param name="onlyActiveUserRoles">A value indicating whether we should look only in active User roles</param>
        /// <returns>Result</returns>
        public static bool IsRegistered(this User User, bool onlyActiveUserRoles = true)
        {
            return IsInUserRole(User, SystemUserRoleNames.Registered, onlyActiveUserRoles);
        }




       

        #endregion


    }
}
