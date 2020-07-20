using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData RestaurantData;
        private readonly IHtmlHelper HtmlHelper;

        [BindProperty]
        public Restaurant Restaurant{ get; set; }
        public IEnumerable<SelectListItem> Cusines { get; set; }

        public EditModel(IRestaurantData restaurantData, 
                         IHtmlHelper htmlHelper)
        {
            this.RestaurantData = restaurantData;
            this.HtmlHelper = htmlHelper;
        }        

        public IActionResult OnGet(int? restaurantId)
        {
            Cusines = HtmlHelper.GetEnumSelectList<CusineType>();

            if (restaurantId.HasValue)
            {
                Restaurant = RestaurantData.GetByID(restaurantId.Value);
            }
            else
            {
                Restaurant = new Restaurant();
            }


            if (Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Cusines = HtmlHelper.GetEnumSelectList<CusineType>();
                return Page();
            }

            if (Restaurant.Id > 0)
            {
                RestaurantData.Update(Restaurant);
            }
            else
            {
                RestaurantData.Add(Restaurant);
            }

            RestaurantData.Commit();
            TempData["Message"] = "Restaurant saved!";
            return RedirectToPage("./Detail", new { restaurantId = Restaurant.Id });
        }
    }
}