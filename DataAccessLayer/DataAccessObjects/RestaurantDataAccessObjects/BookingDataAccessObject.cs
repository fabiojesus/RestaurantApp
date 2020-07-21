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

        public void Create(Restaurant booking)
        {
            using var ctx = new RestaurantContext();
            ctx.Restaurants.Add(booking);
            ctx.SaveChanges();
        }

        public async Task CreateAsync(Restaurant booking)
        {
            using var ctx = new RestaurantContext();
            await ctx.Restaurants.AddAsync(booking);
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

        public void Update(Restaurant booking)
        {
            using var ctx = new RestaurantContext();
            ctx.Entry(booking).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public async Task UpdateAsync(Restaurant booking)
        {
            using var ctx = new RestaurantContext();
            ctx.Entry(booking).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
        }

        public void Delete(Restaurant booking)
        {
            booking.IsDeleted = true;
            Update(booking);
        }

        public async Task DeleteAsync(Restaurant booking)
        {
            booking.IsDeleted = true;
            await UpdateAsync(booking);
        }

        public void Delete(Guid id)
        {
            var booking = Read(id);
            if (booking == null) return;
            Delete(booking);
        }

        public async Task DeleteAsync(Guid id)
        {
            var booking = await ReadAsync(id);
            await DeleteAsync(booking);
        }

    }
}
