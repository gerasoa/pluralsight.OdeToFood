using OdeToFood.Core;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();
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

        public IEnumerable<Restaurant> GetAll()
        {
            return from r in restaurants
                   orderby r.Name
                   select r;
        }
    }
}
