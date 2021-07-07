using CRM.Core.ViewModels;
using DeVeeraApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.ViewModels
{
    public class FeelGoodListModel: CommonPageModel
    {
        public FeelGoodListModel()
        {
            FeelGoodModel = new FeelGoodStoryModel();
        }

        public FeelGoodStoryModel FeelGoodModel { get; set; }
        public PagedResult<FeelGoodViewModel> FeelGoodListPaged { get; set; }
    }
}
