using CRM.Core.Domain;
using System.Collections.Generic;

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
