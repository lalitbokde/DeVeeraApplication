using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.ViewModels.Common
{
    public class ResponseModel
    {
        public ResponseModel()
        {
            this.ResponseList = new List<SelectListItem>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }

        public IList<SelectListItem> ResponseList { get; set; }

    }
}
