using CRM.Core.Domain;
using CRM.Data.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using CRM.Data;
using Microsoft.EntityFrameworkCore;

using CRM.Core.ViewModels;

namespace CRM.Services
{
    public class ImageMasterService : IImageMasterService
    {
        #region fields

        private readonly IRepository<Image> _imageRepository;
        private readonly IS3BucketService _s3BucketService;
        protected readonly dbContextCRM _dbContext;

        #endregion

        #region ctor

        public ImageMasterService(IRepository<Image> imageRepository,
                                  IS3BucketService s3BucketService,
                                  dbContextCRM dbContext)
        {
            _imageRepository = imageRepository;
            _s3BucketService = s3BucketService;
            _dbContext = dbContext;
        }

        #endregion

        #region methods
        public void DeleteImage(Image model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _imageRepository.Delete(model);
        }

        public IList<Image> GetAllImages()
        {
            var query = from vdo in _imageRepository.Table
                        orderby vdo.Name
                        select vdo;
            var images = query.ToList();
            if(images.Count > 0)
            {
                foreach(var item in images)
                {
                    if(item.UpdatedOn.ToShortDateString() != DateTime.Now.ToShortDateString())
                    {
                        item.ImageUrl = _s3BucketService.GetPreSignedURL(item.Key).Result;
                        item.UpdatedOn = DateTime.Now;
                        UpdateImage(item);

                    }
                }
            }
            return images;
        }

        public List<ImageViewModel> GetAllImagesList(
            int page_size = 0, 
            int page_num = 0,
            bool GetAll = false,
            string SortBy = "")
        {
            try
            {

                string query = @"exec [sp_GetAllImages] @page_size = '" + ((page_size == 0) ? 12 : page_size) + "', " +
                                "@page_num  = '" + ((page_num == 0) ? 1 : page_num) + "', " +
                                "@sortBy ='" + SortBy + "' , " +
                                 "@GetAll ='" + GetAll + "'";
                var data = _dbContext.ImageViewModel.FromSql(query).ToList();
                var images= (data.FirstOrDefault() != null) ? data : new List<ImageViewModel>();
                if (images.Count > 0)
                {
                    foreach (var item in images)
                    {
                        if (item.UpdatedOn.ToShortDateString() != DateTime.Now.ToShortDateString())
                        {
                            item.ImageUrl = _s3BucketService.GetPreSignedURL(item.Key).Result;
                            item.UpdatedOn = DateTime.Now;
                            var img = new Image
                            {
                                ImageUrl = item.ImageUrl,
                                UpdatedOn = item.UpdatedOn
                            };
                            UpdateImage(img);

                        }
                    }
                }
                return images;
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public virtual Image GetImageById(int imageId)
        {
            if (imageId == 0)
                return null;

            var data = _imageRepository.GetById(imageId); 
            if(data != null)
            {
                if (data.UpdatedOn.ToShortDateString() != DateTime.Now.ToShortDateString())
                {
                    if(data.Key != null)
                    {
                        data.ImageUrl = _s3BucketService.GetPreSignedURL(data.Key).Result;
                        data.UpdatedOn = DateTime.Now;
                        UpdateImage(data);

                    }

                }
            }
            return data;
        }

        public IList<Image> GetImageByIds(int[] VideoIds)
        {
            if (VideoIds == null || VideoIds.Length == 0)
                return new List<Image>();

            var query = from c in _imageRepository.Table
                        where VideoIds.Contains(c.Id)
                        select c;
            var images = query.ToList();
            //sort by passed identifiers
            var sortedImages = new List<Image>();
            foreach (var id in VideoIds)
            {
                var User = sortedImages.Find(x => x.Id == id);
                if (User != null)
                    sortedImages.Add(User);
            }
            return sortedImages;
        }

        public void InsertImage(Image model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _imageRepository.Insert(model);
        }

        public void UpdateImage(Image model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));



            _imageRepository.Update(model);
        }

       

        #endregion
    }
}
