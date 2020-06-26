using OdeToFood.Data.Model;
using OdeToFood.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OdeToFood.WebSite.Controllers
{
    [Authorize]
    public class RestaurantController : Controller
    {
        readonly BaseDataService<Restaurant> db;

        public RestaurantController()
        {
            db = new InMemoryRestaurantData();
        }
        public ActionResult Index()
        {
            var model = db.Get();
            return View(model);
        }

        
    }
}