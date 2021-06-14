using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeVeeraApp.ViewModels.Images;
using CRM.Services;
using CRM.Core.Domain;
using DeVeeraApp.Utils;

namespace DeVeeraApp.Areas.Admin.ViewComponents
{
    public class ImageSelectionViewComponent: ViewComponent
    {
        #region field
        private readonly IImageMasterService _imageMasterService;
        #endregion
        public ImageSelectionViewComponent(IImageMasterService imageMasterService)
        {
            _imageMasterService = imageMasterService;
        }
        public IViewComponentResult Invoke()
        {
            var ImageSelectionModel = new ImageSelectionModel();

            var images = _imageMasterService.GetAllImages().ToList();
            ImageSelectionModel.ImageSelectionListModel = images.ToList().ToModelList<Image, ImageSelectionListModel>(ImageSelectionModel.ImageSelectionListModel);

            return View(ImageSelectionModel);
        }
    }
}
