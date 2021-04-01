using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using CRM.Core.Domain.Users;
using CRM.Data.Interfaces;

namespace CRM.Services.Users
{
   public class UserPasswordService :IUserPasswordService
    {
        #region Field
        private readonly IRepository<UserPassword> _UserPasswordRepository;
        #endregion
        #region Ctor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="vendorRepository">Vendor repository</param>
        public UserPasswordService(IRepository<UserPassword> UserPasswordRepository) {
            this._UserPasswordRepository = UserPasswordRepository;
        }

        #endregion
        #region Methods

        public void InsertUserPassword(UserPassword password)
        {
            if (password == null)
                throw new ArgumentNullException(nameof(password));

            _UserPasswordRepository.Insert(password);
        }
        /// <summary>
        /// Gets Password by User identifier
        /// </summary>
        /// <param name="UserId">User identifier</param>
        /// <returns>Number of addresses</returns>
        public  UserPassword GetPasswordByUserId(int UserId)
        {
            if (UserId == 0)
                throw new ArgumentNullException(nameof(UserId));

            var query = from a in _UserPasswordRepository.Table
                        where a.UserId == UserId
                        select a;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Updates the Password
        /// </summary>
        /// <param name="Password">Password</param>
        public virtual void UpdatePassword(UserPassword password)
        {
            if (password == null)
                throw new ArgumentNullException(nameof(password));

            _UserPasswordRepository.Update(password);

        }

        #endregion

    }
}
