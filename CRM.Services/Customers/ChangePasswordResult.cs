﻿using System.Collections.Generic;
using System.Linq;

namespace CRM.Services.Users
{
    /// <summary>
    /// Change password result
    /// </summary>
    public class ChangePasswordResult
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public ChangePasswordResult()
        {
            Errors = new List<string>();
        }

        /// <summary>
        /// Gets a value indicating whether request has been completed successfully
        /// </summary>
        public bool Success
        {
            get { return (!Errors.Any()); }
        }

        /// <summary>
        /// Add error
        /// </summary>
        /// <param name="error">Error</param>
        public void AddError(string error)
        {
            this.Errors.Add(error);
        }

        /// <summary>
        /// Errors
        /// </summary>
        public IList<string> Errors { get; set; }
    }
}