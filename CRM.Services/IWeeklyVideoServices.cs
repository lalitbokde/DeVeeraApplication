using CRM.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Services
{
   public interface IWeeklyVideoServices
    {
        void DeleteWeeklyVideo(WeeklyVideo model);


        IList<WeeklyVideo> GetAllWeeklyVideos();



        WeeklyVideo GetWeeklyVideoById(int videoId);


        IList<WeeklyVideo> GetWeeklyVideoByIds(int[] VideoIds);


        void InsertWeeklyVideo(WeeklyVideo model);


        void UpdateWeeklyVideo(WeeklyVideo model);

    }
}
