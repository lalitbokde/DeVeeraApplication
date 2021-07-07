using CRM.Core.Domain;
using CRM.Core.ViewModels;
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
        List<FeelGoodViewModel> GetAllFeelGoodStoriesSp(
          int page_size = 0,
          int page_num = 0,
          bool GetAll = false,
          string SortBy = "",
          int ImageId = 0
        );

    }
}
