using CRM.Core.Domain;
using System.Collections.Generic;

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
    }
}
