﻿using CRM.Core.Domain.VideoModules;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.ViewModels
{
    public class LevelModel : BaseEntityModel
    {
        public LevelModel()
        {
            Modules = new Modules();
            this.AvailableVideoUrl = new List<SelectListItem>();
        }
        public int VideoId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Subtitle { get; set; }

        [Required]
        public string Quote { get; set; }
        [Required]
        public string VideoURL { get; set; }
        public string VideoName { get; set; }

        [Required]
        public string FullDescription { get; set; }
        public Modules Modules { get; set; }
        public IList<Modules> ModuleList { get; set; }
        public IList<SelectListItem> AvailableVideoUrl { get; set; }

        [NotMapped]
        public int srno { get; set; }


        [NotMapped]
        public string DiaryText { get; set; }

        [NotMapped]
        public string DiaryLatestUpdateDate { get; set; }

    }
}