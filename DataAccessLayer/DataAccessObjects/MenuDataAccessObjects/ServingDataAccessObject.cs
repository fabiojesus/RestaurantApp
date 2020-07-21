using Microsoft.EntityFrameworkCore;
using Recodme.Academy.RestaurantApp.DataAccessLayer.Contexts;
using Recodme.Academy.RestaurantApp.DataLayer.MenuRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.Academy.RestaurantApp.DataAccessLayer.DataAccessObjects.MenuDataAcccessObjects
{
    public class ServingDataAccessObject
    {
        
        public List<Serving> List()
        {
            using var ctx = new RestaurantContext();
            return ctx.Servings.ToList();
        }

        public async Task<List<Serving>> ListAsync()
        {
            using var ctx = new RestaurantContext();
            return await ctx.Servings.ToListAsync();
        }

        public void Create(Serving serving)
        {
            using var ctx = new RestaurantContext();
            ctx.Servings.Add(serving);
            ctx.SaveChanges();
        }

        public async Task CreateAsync(Serving serving)
        {
            using var ctx = new RestaurantContext();
            await ctx.Servings.AddAsync(serving);
            await ctx.SaveChangesAsync();
        }

        public Serving Read(Guid id)
        {
            using var ctx = new RestaurantContext();
            return ctx.Servings.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Serving> ReadAsync(Guid id)
        {
            using var ctx = new RestaurantContext();
            return await ctx.Servings.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(Serving serving)
        {
            using var ctx = new RestaurantContext();
            ctx.Entry(serving).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public async Task UpdateAsync(Serving serving)
        {
            using var ctx = new RestaurantContext();
            ctx.Entry(serving).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
        }

        public void Delete(Serving serving)
        {
            serving.IsDeleted = true;
            Update(serving);
        }

        public async Task DeleteAsync(Serving serving)
        {
            serving.IsDeleted = true;
            await UpdateAsync(serving);
        }

        public void Delete(Guid id)
        {
            var serving = Read(id);
            if (serving == null) return;
            Delete(serving);
        }

        public async Task DeleteAsync(Guid id)
        {
            var serving = await ReadAsync(id);
            await DeleteAsync(serving);
        }
    }
}
