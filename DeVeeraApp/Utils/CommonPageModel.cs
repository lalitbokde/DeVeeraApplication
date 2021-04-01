using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.Utils
{
    public class CommonPageModel
    {
        public string SelectedPageSize { get; set; }

        public IList<SelectListItem> AvailablePageSize
        {
            get
            {
                return PageSizeDropdown.GetPageSizeDropdown(SelectedPageSize);
            }
        }


    }

    public static class PageSizeDropdown
    {
        public static List<SelectListItem> GetPageSizeDropdown(string SelectedValue = null)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            selectListItems.Add(new SelectListItem { Text = "12", Value = "12", Selected = (SelectedValue == "12") });
            selectListItems.Add(new SelectListItem { Text = "24", Value = "24", Selected = (SelectedValue == "24") });
            selectListItems.Add(new SelectListItem { Text = "36", Value = "36", Selected = (SelectedValue == "36") });
            selectListItems.Add(new SelectListItem { Text = "48", Value = "48", Selected = (SelectedValue == "48") });
            selectListItems.Add(new SelectListItem { Text = "60", Value = "60", Selected = (SelectedValue == "60") });
            selectListItems.Add(new SelectListItem { Text = "All", Value = "0", Selected = (SelectedValue == "0") });
            return selectListItems;
        }
    }
}
