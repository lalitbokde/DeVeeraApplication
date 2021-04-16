using CRM.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Services
{
   public interface IWeeklyUpdateServices
    {
        void DeleteWeeklyUpdate(WeeklyUpdate model);


        IList<WeeklyUpdate> GetAllWeeklyUpdates();



        WeeklyUpdate GetWeeklyUpdateById(int videoId);


        IList<WeeklyUpdate> GetWeeklyUpdatesByIds(int[] VideoIds);


        void InsertWeeklyUpdate(WeeklyUpdate model);


        void UpdateWeeklyUpdate(WeeklyUpdate model);

        void InActiveAllActiveQuotes(int quoteTypeId);
        WeeklyUpdate GetWeeklyUpdateByQuoteType(int typeId);
        IList<WeeklyUpdate> GetWeeklyUpdatesByQuoteType(int typeId);


    }
}
