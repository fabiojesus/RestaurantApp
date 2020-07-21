using Microsoft.EntityFrameworkCore;
using Recodme.Academy.RestaurantApp.DataAccessLayer.Contexts;
using Recodme.Academy.RestaurantApp.DataLayer.RestaurantRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.Academy.RestaurantApp.DataAccessLayer.DataAccessObjects.RestaurantDataAcccessObjects
{
    public class BookingDataAccessObject
    {
        
        public List<Booking> List()
        {
            using var ctx = new RestaurantContext();
            return ctx.Bookings.ToList();
        }

        public async Task<List<Booking>> ListAsync()
        {
            using var ctx = new RestaurantContext();
            return await ctx.Bookings.ToListAsync();
        }

        public void Create(Booking restaurant)
        {
            using var ctx = new RestaurantContext();
            ctx.Bookings.Add(restaurant);
            ctx.SaveChanges();
        }

        public async Task CreateAsync(Booking restaurant)
        {
            using var ctx = new RestaurantContext();
            await ctx.Bookings.AddAsync(restaurant);
            await ctx.SaveChangesAsync();
        }

        public Booking Read(Guid id)
        {
            using var ctx = new RestaurantContext();
            return ctx.Bookings.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Booking> ReadAsync(Guid id)
        {
            using var ctx = new RestaurantContext();
            return await ctx.Bookings.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(Booking restaurant)
        {
            using var ctx = new RestaurantContext();
            ctx.Entry(restaurant).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public async Task UpdateAsync(Booking restaurant)
        {
            using var ctx = new RestaurantContext();
            ctx.Entry(restaurant).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
        }

        public void Delete(Booking restaurant)
        {
            restaurant.IsDeleted = true;
            Update(restaurant);
        }

        public async Task DeleteAsync(Booking restaurant)
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
