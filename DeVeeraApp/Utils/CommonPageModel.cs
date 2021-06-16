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
            selectListItems.Add(new SelectListItem { Text = "10", Value = "10", Selected = (SelectedValue == "10") });
            selectListItems.Add(new SelectListItem { Text = "20", Value = "20", Selected = (SelectedValue == "20") });
            selectListItems.Add(new SelectListItem { Text = "30", Value = "30", Selected = (SelectedValue == "30") });
            selectListItems.Add(new SelectListItem { Text = "40", Value = "40", Selected = (SelectedValue == "40") });
            selectListItems.Add(new SelectListItem { Text = "50", Value = "50", Selected = (SelectedValue == "50") });
            selectListItems.Add(new SelectListItem { Text = "All", Value = "0", Selected = (SelectedValue == "0") });
            return selectListItems;
        }
    }
}
