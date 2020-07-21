using Microsoft.EntityFrameworkCore;
using Recodme.Academy.RestaurantApp.DataAccessLayer.Contexts;
using Recodme.Academy.RestaurantApp.DataLayer.MenuRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.Academy.RestaurantApp.DataAccessLayer.DataAccessObjects.MenuDataAcccessObjects
{
    public class MealDataAccessObject
    {
        public List<Meal> List()
        {
            using var ctx = new RestaurantContext();
            return ctx.Meals.ToList();
        }

        public async Task<List<Meal>> ListAsync()
        {
            using var ctx = new RestaurantContext();
            return await ctx.Meals.ToListAsync();
        }

        public void Create(Meal meal)
        {
            using var ctx = new RestaurantContext();
            ctx.Meals.Add(meal);
            ctx.SaveChanges();
        }

        public async Task CreateAsync(Meal meal)
        {
            using var ctx = new RestaurantContext();
            await ctx.Meals.AddAsync(meal);
            await ctx.SaveChangesAsync();
        }

        public Meal Read(Guid id)
        {
            using var ctx = new RestaurantContext();
            return ctx.Meals.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Meal> ReadAsync(Guid id)
        {
            using var ctx = new RestaurantContext();
            return await ctx.Meals.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(Meal meal)
        {
            using var ctx = new RestaurantContext();
            ctx.Entry(meal).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public async Task UpdateAsync(Meal meal)
        {
            using var ctx = new RestaurantContext();
            ctx.Entry(meal).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
        }

        public void Delete(Meal meal)
        {
            meal.IsDeleted = true;
            Update(meal);
        }

        public async Task DeleteAsync(Meal meal)
        {
            meal.IsDeleted = true;
            await UpdateAsync(meal);
        }

        public void Delete(Guid id)
        {
            var meal = Read(id);
            if (meal == null) return;
            Delete(meal);
        }

        public async Task DeleteAsync(Guid id)
        {
            var meal = await ReadAsync(id);
            await DeleteAsync(meal);
        }
    }
}
