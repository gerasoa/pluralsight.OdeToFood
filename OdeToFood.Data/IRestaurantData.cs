using OdeToFood.Core;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetRestaurantsByName(string name);
        Restaurant GetByID(int id);
    }

    public class InMemoryRestaurantData : IRestaurantData
    {
        readonly List<Restaurant> restaurants;

        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant {Id=1, Name="Scott's Pizza", Location="Maryland", Cusine= CusineType.Italian},
                new Restaurant {Id=2, Name="Cinnamon Club", Location="London", Cusine= CusineType.Indian},
                new Restaurant {Id=3, Name="La Costa", Location="California", Cusine= CusineType.Mexican}
            };
        }

        public Restaurant GetByID(int id)
        {
            return restaurants.SingleOrDefault(r => r.Id == id);
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name = null)
        {
            return from r in restaurants
                      where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                      orderby r.Name
                      select r;
        }      
    }
}
