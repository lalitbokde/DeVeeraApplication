using CRM.Core.Domain.Users;
using CRM.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CRM.Services.Customers
{
    public class DiaryPasscodeService:IDiaryPasscodeService
    {
        #region fields
        private readonly IRepository<DiaryPasscode> _diaryPasscodeRepository;

        #endregion

        #region ctor
        public DiaryPasscodeService(IRepository<DiaryPasscode> diaryPasscodeRepository)
        {
            _diaryPasscodeRepository = diaryPasscodeRepository;
        }
        #endregion


        #region Methods
        public void DeleteDiaryPasscode(DiaryPasscode DiaryPasscode)
        {
            if (DiaryPasscode == null)
                throw new ArgumentNullException(nameof(DiaryPasscode));

            _diaryPasscodeRepository.Delete(DiaryPasscode);
        }

        public IList<DiaryPasscode> GetAllDiaryPasscodes()
        {
            var query = from a in _diaryPasscodeRepository.Table
                        orderby a.Id
                        select a;
            var emotion = query.ToList();
            return emotion;
        }

        public DiaryPasscode GetDiaryPasscodeById(int DiaryPasscodeId)
        {
            if (DiaryPasscodeId == 0)
                return null;

            return _diaryPasscodeRepository.GetById(DiaryPasscodeId);
        }
        public IList<DiaryPasscode> GetDiaryPasscodeByUserId(int UserId)
        {
            if (UserId == 0)
                return null;
            var query = from a in _diaryPasscodeRepository.Table
                        where a.UserId == UserId
                        select a;

            var data = query.ToList();

            return data;
        }
        public void InsertDiaryPasscode(DiaryPasscode DiaryPasscode)
        {
            if (DiaryPasscode == null)
                throw new ArgumentNullException(nameof(DiaryPasscode));

            _diaryPasscodeRepository.Insert(DiaryPasscode);
        }

        public void UpdateDiaryPasscode(DiaryPasscode DiaryPasscode)
        {
            if (DiaryPasscode == null)
                throw new ArgumentNullException(nameof(DiaryPasscode));

            _diaryPasscodeRepository.Update(DiaryPasscode);
        }

        #endregion
    }
}
