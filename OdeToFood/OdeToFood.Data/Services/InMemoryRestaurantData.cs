using OdeToFood.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdeToFood.Data.Services
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        readonly List<Restaurant> restaurants;

        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant() {  Id=1 ,Cuisine= CuisineType.Italian, Name="Pizza's Ugis" },
                new Restaurant() {  Id=2 ,Cuisine= CuisineType.French, Name="Tersiguels" },
                new Restaurant() {  Id=3 ,Cuisine= CuisineType.Indian, Name="Mango Grove" },
            };
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return restaurants.OrderBy(o=> o.Name);
        }
    }
}
