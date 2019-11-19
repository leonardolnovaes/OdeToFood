using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetRestaurantsByName(string name);
        Restaurant GetByID(int id);
        Restaurant Update(Restaurant updateRestaurant);
        Restaurant Add(Restaurant newRestaurant); 
        int Commit();
    }
    public class InMemoryRestaurantData : IRestaurantData
    {
        readonly List<Restaurant> restaurants;

        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
            new Restaurant { Id = 1, Name = "Scott's Pizza", Location = "Maryland", Cuisine = CuisineType.Italian},
            new Restaurant { Id = 2, Name = "Leo's Pizza", Location = "Mogi das Cruzes", Cuisine = CuisineType.Mexican },
            new Restaurant { Id = 3, Name = "Vicente's Pizza", Location = "São José dos Campos", Cuisine = CuisineType.None }
        };
        }

        public Restaurant GetByID(int id)
        {
            return restaurants.SingleOrDefault(r => r.Id == id);
        }

        public Restaurant Update(Restaurant updateRestaurant)
        {
            var restaurant = restaurants.SingleOrDefault(r => r.Id == updateRestaurant.Id);
            if (restaurant != null)
            {
                restaurant.Name =        updateRestaurant.Name;
                restaurant.Location = updateRestaurant.Location;
                restaurant.Cuisine = updateRestaurant.Cuisine;
            }
            return restaurant;
        }
        public Restaurant Add(Restaurant newRestaurant)
        {
            restaurants.Add(newRestaurant);
            newRestaurant.Id = restaurants.Max(r => r.Id) + 1;
            return newRestaurant;
        }
        public int Commit()
        {
            return 0;
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name = null)
        {
            {
                return from r in restaurants
                       where string.IsNullOrEmpty(name)  || r.Name.Contains(name)
                       orderby r.Name
                       select r;
            }
        }

    }
}

