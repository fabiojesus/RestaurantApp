using Microsoft.EntityFrameworkCore;
using Recodme.Academy.RestaurantApp.DataAccessLayer.Contexts;
using Recodme.Academy.RestaurantApp.DataLayer.RestaurantRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.Academy.RestaurantApp.DataAccessLayer.DataAccessObjects.RestaurantDataAcccessObjects
{
    public class RestaurantDataAccessObject
    {
        
        public List<Restaurant> List()
        {
            using var ctx = new RestaurantContext();
            return ctx.Restaurants.ToList();
        }

        public async Task<List<Restaurant>> ListAsync()
        {
            using var ctx = new RestaurantContext();
            return await ctx.Restaurants.ToListAsync();
        }

        public void Create(Restaurant restaurant)
        {
            using var ctx = new RestaurantContext();
            ctx.Restaurants.Add(restaurant);
            ctx.SaveChanges();
        }

        public async Task CreateAsync(Restaurant restaurant)
        {
            using var ctx = new RestaurantContext();
            await ctx.Restaurants.AddAsync(restaurant);
            await ctx.SaveChangesAsync();
        }

        public Restaurant Read(Guid id)
        {
            using var ctx = new RestaurantContext();
            return ctx.Restaurants.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Restaurant> ReadAsync(Guid id)
        {
            using var ctx = new RestaurantContext();
            return await ctx.Restaurants.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(Restaurant restaurant)
        {
            using var ctx = new RestaurantContext();
            ctx.Entry(restaurant).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public async Task UpdateAsync(Restaurant restaurant)
        {
            using var ctx = new RestaurantContext();
            ctx.Entry(restaurant).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
        }

        public void Delete(Restaurant restaurant)
        {
            restaurant.IsDeleted = true;
            Update(restaurant);
        }

        public async Task DeleteAsync(Restaurant restaurant)
        {
            restaurant.IsDeleted = true;
            await UpdateAsync(restaurant);
        }

        public void Delete(Guid id)
        {
            var restaurant = Read(id);
            if (restaurant == null) return;
            Delete(restaurant);
        }

        public async Task DeleteAsync(Guid id)
        {
            var restaurant = await ReadAsync(id);
            await DeleteAsync(restaurant);
        }
    }
}
