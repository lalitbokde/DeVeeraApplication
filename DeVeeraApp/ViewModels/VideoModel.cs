using CRM.Core.ViewModels;
using DeVeeraApp.Utils;
using DeVeeraApp.ViewModels.Enum;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeVeeraApp.ViewModels
{
    public class VideoModel : BaseEntityModel
    {

        [StringLength(80)]
        [Required(ErrorMessage ="Please enter video name ")]
        public string Name { get; set; }
        public string VideoUrl { get; set; }


        public string Key { get; set; }

        public string FileName { get; set; }
        public bool IsNew { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        public string Link_2_bannerImage { get; set; }
        public string Link_2_Title { get; set; }


        public int SortTypeId { get; set; }
        public string SearchByDate { get; set; }

        [NotMapped]
        public SortType SortType
        {
            get
            {
                return (SortType)SortTypeId;
            }
            set
            {
                SortTypeId = (int)value;
            }
        }
      

        public PagedResult<VideoViewModel> VideoListPaged { get; set; }



    }
}
