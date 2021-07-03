using CRM.Core.ViewModels;
using DeVeeraApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.ViewModels.Images
{
    public class ImageListModel: CommonPageModel
    {
       
        public PagedResult<ImageViewModel> ImageList { get; set; }


       
    }
}
