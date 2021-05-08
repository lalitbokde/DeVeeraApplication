using CRM.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Services
{
    public interface IFeelGoodStoryServices
    {
        void DeleteFeelGoodStory(FeelGoodStory model);


        IList<FeelGoodStory> GetAllFeelGoodStorys();



        FeelGoodStory GetFeelGoodStoryById(int videoId);


        IList<FeelGoodStory> GetFeelGoodStoryByIds(int[] VideoIds);


        void InsertFeelGoodStory(FeelGoodStory model);


        void UpdateFeelGoodStory(FeelGoodStory model);

    }
}
