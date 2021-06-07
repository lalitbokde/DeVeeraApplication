using CRM.Core.Domain.LayoutSetups;
using CRM.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CRM.Services.Layoutsetup
{
    public class LayoutSetupService:ILayoutSetupService
    {
        #region fields
        private readonly IRepository<LayoutSetup> _LayoutSetupRepository;

        #endregion

        #region ctor
        public LayoutSetupService(IRepository<LayoutSetup> LayoutSetupRepository)
        {
            _LayoutSetupRepository = LayoutSetupRepository;
        }
        #endregion


        #region Methods
        public void DeleteLayoutSetup(LayoutSetup LayoutSetup)
        {
            if (LayoutSetup == null)
                throw new ArgumentNullException(nameof(LayoutSetup));

            _LayoutSetupRepository.Delete(LayoutSetup);
        }

        public IList<LayoutSetup> GetAllLayoutSetups()
        {
            var query = from vdo in _LayoutSetupRepository.Table
                        orderby vdo.Id
                        select vdo;
            var quote = query.ToList();
            return quote;
        }

        public LayoutSetup GetLayoutSetupById(int LayoutSetupId)
        {
            if (LayoutSetupId == 0)
                return null;


            return _LayoutSetupRepository.GetById(LayoutSetupId);
        }

        public void InsertLayoutSetup(LayoutSetup LayoutSetup)
        {
            if (LayoutSetup == null)
                throw new ArgumentNullException(nameof(LayoutSetup));

            _LayoutSetupRepository.Insert(LayoutSetup);
        }

        public void UpdateLayoutSetup(LayoutSetup LayoutSetup)
        {
            if (LayoutSetup == null)
                throw new ArgumentNullException(nameof(LayoutSetup));



            _LayoutSetupRepository.Update(LayoutSetup);
        }

        #endregion
    }
}
