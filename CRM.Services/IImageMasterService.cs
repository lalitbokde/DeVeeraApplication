using CRM.Core.Domain;
using CRM.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;



namespace CRM.Services
{
    public interface IImageMasterService
    {
        void DeleteImage(Image model);


        IList<Image> GetAllImages(); 



        Image GetImageById(int videoId);


        IList<Image> GetImageByIds(int[] VideoIds);


        void InsertImage(Image model);


        void UpdateImage(Image model);
        List<ImageViewModel> GetAllImagesList(
              int page_size = 0,
             int page_num = 0,
             bool GetAll = false,
             string SortBy = ""
              );

    }
}
