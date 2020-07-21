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

        public void Create(Booking booking)
        {
            using var ctx = new RestaurantContext();
            ctx.Bookings.Add(booking);
            ctx.SaveChanges();
        }

        public async Task CreateAsync(Booking booking)
        {
            using var ctx = new RestaurantContext();
            await ctx.Bookings.AddAsync(booking);
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

        public void Update(Booking booking)
        {
            using var ctx = new RestaurantContext();
            ctx.Entry(booking).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public async Task UpdateAsync(Booking booking)
        {
            using var ctx = new RestaurantContext();
            ctx.Entry(booking).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
        }

        public void Delete(Booking booking)
        {
            booking.IsDeleted = true;
            Update(booking);
        }

        public async Task DeleteAsync(Booking booking)
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
