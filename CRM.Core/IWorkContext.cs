using CRM.Core.Domain.Users;


using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core
{
    /// <summary>
    /// Represents work context
    /// </summary>
    public interface IWorkContext
    {
        /// <summary>
        /// Gets or sets the current User
        /// </summary>
        User CurrentUser { get; set; }

        /// <summary>
        /// Gets the original User (in case the current one is impersonated)
        /// </summary>
        User OriginalUserIfImpersonated { get; }

      

        /// <summary>
        /// Gets or sets value indicating whether we're in admin area
        /// </summary>
        bool IsAdmin { get; set; }
    }
}
