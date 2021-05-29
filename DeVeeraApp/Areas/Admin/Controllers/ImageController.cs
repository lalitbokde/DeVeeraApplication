using CRM.Core;
using CRM.Core.Domain;
using CRM.Services;
using CRM.Services.Authentication;
using CRM.Services.Message;
using DeVeeraApp.Utils;
using DeVeeraApp.ViewModels;
using DeVeeraApp.ViewModels.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ImageController : BaseController
    {
        #region fields

        private readonly IImageMasterService _imageMasterService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly INotificationService _notificationService;
        private readonly IS3BucketService _s3BucketService;

        #endregion



        #region ctor

        public ImageController(IImageMasterService imageMasterService,
                               IHostingEnvironment hostingEnvironment,
                               INotificationService notificationService,
                               IS3BucketService s3BucketService,
                               IWorkContext workContext,
                               IHttpContextAccessor httpContextAccessor,
                               IAuthenticationService authenticationService) : base(workContext: workContext,
                                                                                  httpContextAccessor: httpContextAccessor,
                                                                                  authenticationService: authenticationService)
        {
            _imageMasterService = imageMasterService;
            _hostingEnvironment = hostingEnvironment;
            _notificationService = notificationService;
            _s3BucketService = s3BucketService;
        }
        #endregion



        #region methods

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            AddBreadcrumbs("Image", "Create", "/Image/Create", "/Image/Create");
            return View();
        }

        [HttpPost]
        public IActionResult Create(ImageModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = model.ToEntity<Image>();
                    var imageUrl = UploadToAWS(model.FileName);
                    data.ImageUrl = imageUrl.Result.ToString();
                    data.Key = model.FileName;
                    _imageMasterService.InsertImage(data);
                    _notificationService.SuccessNotification("Image url added successfully");
                    return RedirectToAction("List");
                }
            }
            catch (Exception ex)
            {
                _notificationService.ErrorNotification(ex.Message);
                return RedirectToAction("List");
            }
            return View();
        }


        public IActionResult Edit(int id)
        {
            AddBreadcrumbs("Image", "Edit", $"/Image/Edit/{id}", $"/Image/Edit/{id}");
            if (id != 0)
            {
                var data = _imageMasterService.GetImageById(id);
                if (data != null)
                {
                    var model = data.ToModel<ImageModel>();
                    model.ImageUrl = _s3BucketService.GetPreSignedURL(model.Key).Result;
                    return View(model);
                }
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult Edit(ImageModel model)
        {

            if (ModelState.IsValid)
            {
                var imageData = _imageMasterService.GetImageById(model.Id);
                try
                {
                    if (model.FileName != null)
                    {
                        var url = UploadToAWS(model.FileName);
                        imageData.ImageUrl = url.Result;
                        imageData.Key = model.FileName;

                    }
                }
                catch (Exception ex)
                { }

                imageData.Name = model.Name;

                _imageMasterService.UpdateImage(imageData);
                _notificationService.SuccessNotification("Image url updated successfully.");
                return RedirectToAction("List");
            }
            return View();
        }

        public IActionResult List()
        {
            AddBreadcrumbs("Image", "List", "/Image/List", "/Image/List");
            var imageList = _imageMasterService.GetAllImages();
            var model = new List<ImageModel>();

            if(imageList.Count > 0)
            {
                foreach(var item in imageList)
                {
                    var data = new ImageModel();
                    data.ImageUrl = _s3BucketService.GetPreSignedURL(item?.Key).Result;
                    data.Name = item.Name;
                    data.Id = item.Id;
                    model.Add(data);
                }
               
            }
            return View(model);
        }


        public async Task<bool> Uploadlocal(IFormFile file)
        {
            var FileDic = "Files/Images";

            string FilePath = Path.Combine(_hostingEnvironment.WebRootPath, FileDic);

            if (!Directory.Exists(FilePath))

                Directory.CreateDirectory(FilePath);

            var filePath = Path.Combine(FilePath, file.FileName);

            using (FileStream fs = System.IO.File.Create(filePath))
            {
                file.CopyTo(fs);
            }

            return true;
        }


        public async Task<string> UploadToAWS(string fileName)
        {
            string val;

            var path = Path.Combine(_hostingEnvironment.WebRootPath + "//Files//Images", fileName);
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                val = await _s3BucketService.UploadFileAsync(stream, path, fileName);


            }
            System.IO.File.Delete(path);
            return val;
        }


        public IActionResult ImagePreview(int Id)
        {
            if (Id != 0)
            {
                var data = _imageMasterService.GetImageById(Id);

                data.ImageUrl = _s3BucketService.GetPreSignedURL(data.Key).Result;
                _imageMasterService.UpdateImage(data);
                var model = data.ToModel<ImageModel>();
                return View(model);

            }
            return RedirectToAction("List");
        }


        public IActionResult DeleteImage(int imageId)
        {
            ResponseModel response = new ResponseModel();

            if (imageId != 0)
            {
                var data = _imageMasterService.GetImageById(imageId);

                if (data == null)
                {
                    response.Success = false;
                    response.Message = "No video found";
                }
                _s3BucketService.DeleteFile(data.Key);

                data.Key = null;
                data.ImageUrl = null;

                _imageMasterService.UpdateImage(data);
                response.Success = true;

            }
            else
            {
                response.Success = false;
                response.Message = "imageId is 0";

            }
            return Json(response);
        }


        public IActionResult Delete(int imageId)
        {
            ResponseModel response = new ResponseModel();

            if (imageId != 0)
            {
                var imageData = _imageMasterService.GetImageById(imageId);
                if (imageData == null)
                {
                    response.Success = false;
                    response.Message = "No image found";
                }
                _s3BucketService.DeleteFile(imageData.Key);
                _imageMasterService.DeleteImage(imageData);

                response.Success = true;
            }
            else
            {
                response.Success = false;
                response.Message = "imageId is 0";

            }
            return Json(response);
        }

        #endregion
    }
}
