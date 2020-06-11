using OdeToFood.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OdeToFood.WebSite.Controllers
{
    public class RestaurantController : Controller
    {
        readonly IRestaurantData db;

        public RestaurantController()
        {
            db = new InMemoryRestaurantData();
        }
        public ActionResult Index()
        {
            var model = db.GetAll();
            return View(model);
        }

        
    }
}