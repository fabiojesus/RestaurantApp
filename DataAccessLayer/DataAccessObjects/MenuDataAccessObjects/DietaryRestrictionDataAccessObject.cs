using Microsoft.EntityFrameworkCore;
using Recodme.Academy.RestaurantApp.DataAccessLayer.Contexts;
using Recodme.Academy.RestaurantApp.DataLayer.MenuRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.Academy.RestaurantApp.DataAccessLayer.DataAccessObjects.MenuDataAcccessObjects
{
    public class DietaryRestrictionDataAccessObject
    {

        public List<DietaryRestriction> List()
        {
            using var ctx = new RestaurantContext();
            return ctx.DietaryRestrictions.ToList();
        }

        public async Task<List<DietaryRestriction>> ListAsync()
        {
            using var ctx = new RestaurantContext();
            return await ctx.DietaryRestrictions.ToListAsync();
        }

        public void Create(DietaryRestriction dietaryRestriction)
        {
            using var ctx = new RestaurantContext();
            ctx.DietaryRestrictions.Add(dietaryRestriction);
            ctx.SaveChanges();
        }

        public async Task CreateAsync(DietaryRestriction dietaryRestriction)
        {
            using var ctx = new RestaurantContext();
            await ctx.DietaryRestrictions.AddAsync(dietaryRestriction);
            await ctx.SaveChangesAsync();
        }

        public DietaryRestriction Read(Guid id)
        {
            using var ctx = new RestaurantContext();
            return ctx.DietaryRestrictions.FirstOrDefault(x => x.Id == id);
        }

        public async Task<DietaryRestriction> ReadAsync(Guid id)
        {
            using var ctx = new RestaurantContext();
            return await ctx.DietaryRestrictions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(DietaryRestriction dietaryRestriction)
        {
            using var ctx = new RestaurantContext();
            ctx.Entry(dietaryRestriction).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public async Task UpdateAsync(DietaryRestriction dietaryRestriction)
        {
            using var ctx = new RestaurantContext();
            ctx.Entry(dietaryRestriction).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
        }

        public void Delete(DietaryRestriction dietaryRestriction)
        {
            dietaryRestriction.IsDeleted = true;
            Update(dietaryRestriction);
        }

        public async Task DeleteAsync(DietaryRestriction dietaryRestriction)
        {
            dietaryRestriction.IsDeleted = true;
            await UpdateAsync(dietaryRestriction);
        }

        public void Delete(Guid id)
        {
            var dietaryRestriction = Read(id);
            if (dietaryRestriction == null) return;
            Delete(dietaryRestriction);
        }

        public async Task DeleteAsync(Guid id)
        {
            var dietaryRestriction = await ReadAsync(id);
            await DeleteAsync(dietaryRestriction);
        }
    }
}
