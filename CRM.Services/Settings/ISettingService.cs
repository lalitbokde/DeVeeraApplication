using CRM.Core.Domain;
using System.Collections.Generic;

namespace CRM.Services.Settings
{
    public partial interface ISettingService
    {
        /// <summary>
        /// Gets a permission
        /// </summary>
        /// <param name="permissionId">Permission identifier</param>
        /// <returns>Permission</returns>
        Setting GetSettingByUserId(int UserId);

        /// <summary>
        /// Gets all permission
        /// </summary>
        /// <param name="permission">Permission setting</param>
        /// <returns>Permission</returns>

        IList<Setting> GetAllSetting();


        Setting GetSettingByUserIdAndProjectId(int UserId, int ProjectId);

        /// <summary>
        /// Gets all permission
        /// </summary>
        /// <param name="permission">Permission setting</param>
        /// <returns>Permission</returns>
        Setting GetSetting();



        /// <summary>
        /// Inserts a permission
        /// </summary>
        /// <param name="permission">setting</param>
        void InsertSetting(Setting setting);

        /// <summary>
        /// Updates the permission
        /// </summary>
        /// <param name="permission">setting</param>
        void UpdateSetting(Setting setting);

    }
}
