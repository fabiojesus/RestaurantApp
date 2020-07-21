using Microsoft.EntityFrameworkCore;
using Recodme.Academy.RestaurantApp.DataAccessLayer.Contexts;
using Recodme.Academy.RestaurantApp.DataLayer.MenuRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.Academy.RestaurantApp.DataAccessLayer.DataAccessObjects.MenuDataAcccessObjects
{
    public class DishDataAccessObject
    {
        public List<Dish> List()
        {
            using var ctx = new RestaurantContext();
            return ctx.Dishes.ToList();
        }

        public async Task<List<Dish>> ListAsync()
        {
            using var ctx = new RestaurantContext();
            return await ctx.Dishes.ToListAsync();
        }

        public void Create(Dish dish)
        {
            using var ctx = new RestaurantContext();
            ctx.Dishes.Add(dish);
            ctx.SaveChanges();
        }

        public async Task CreateAsync(Dish dish)
        {
            using var ctx = new RestaurantContext();
            await ctx.Dishes.AddAsync(dish);
            await ctx.SaveChangesAsync();
        }

        public Dish Read(Guid id)
        {
            using var ctx = new RestaurantContext();
            return ctx.Dishes.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Dish> ReadAsync(Guid id)
        {
            using var ctx = new RestaurantContext();
            return await ctx.Dishes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(Dish dish)
        {
            using var ctx = new RestaurantContext();
            ctx.Entry(dish).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public async Task UpdateAsync(Dish dish)
        {
            using var ctx = new RestaurantContext();
            ctx.Entry(dish).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
        }

        public void Delete(Dish dish)
        {
            dish.IsDeleted = true;
            Update(dish);
        }

        public async Task DeleteAsync(Dish dish)
        {
            dish.IsDeleted = true;
            await UpdateAsync(dish);
        }

        public void Delete(Guid id)
        {
            var dish = Read(id);
            if (dish == null) return;
            Delete(dish);
        }

        public async Task DeleteAsync(Guid id)
        {
            var dish = await ReadAsync(id);
            await DeleteAsync(dish);
        }
    }
}
