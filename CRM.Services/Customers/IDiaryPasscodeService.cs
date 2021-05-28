using CRM.Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Services.Customers
{
    public interface IDiaryPasscodeService
    {
        void DeleteDiaryPasscode(DiaryPasscode DiaryPasscode);
        IList<DiaryPasscode> GetAllDiaryPasscodes();
        DiaryPasscode GetDiaryPasscodeById(int DiaryPasscodeId);
        IList<DiaryPasscode> GetDiaryPasscodeByUserId(int UserId);
        void InsertDiaryPasscode(DiaryPasscode DiaryPasscode);
        void UpdateDiaryPasscode(DiaryPasscode DiaryPasscode);
    }
}
