using CRM.Core;
using CRM.Core.Domain;
using CRM.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CRM.Services.Settings
{
    public class SettingService : ISettingService
    {
        #region fields
        private readonly IRepository<Setting> _settingRepository;
        private readonly IWorkContext _workContext;
        #endregion

        #region ctor
        public SettingService(IRepository<Setting> settingRepository,
            IWorkContext workContext)
        {
           
            _settingRepository = settingRepository;
            _workContext = workContext;
        }
        #endregion



        public IList<Setting> GetAllSetting()
        {
            var data = _settingRepository.Table.ToList();

            return data.ToList();
        }

        public Setting GetSetting()
        {
            var data = from q in _settingRepository.Table
                       select q;
            var val = data.FirstOrDefault();
            return val;
        }

        public Setting GetSettingByUserId(int UserId)
        {
            var data = _settingRepository.Table.Where(a => a.UserId == UserId).FirstOrDefault();
            return data;
        }

        public Setting GetSettingByUserIdAndProjectId(int UserId, int ProjectId)
        {
            var data = _settingRepository.Table.Where(a => a.UserId == UserId && a.ProjectId == ProjectId).FirstOrDefault();
            return data;
        }

        public void InsertSetting(Setting setting)
        {
            _settingRepository.Insert(setting);
        }

        public void UpdateSetting(Setting setting)
        {
            _settingRepository.Update(setting);
        }
    }
}
