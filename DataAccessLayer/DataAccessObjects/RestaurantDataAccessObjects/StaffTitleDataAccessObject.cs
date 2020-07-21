using Microsoft.EntityFrameworkCore;
using Recodme.Academy.RestaurantApp.DataAccessLayer.Contexts;
using Recodme.Academy.RestaurantApp.DataLayer.RestaurantRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.Academy.RestaurantApp.DataAccessLayer.DataAccessObjects.RestaurantDataAcccessObjects
{
    public class StaffTitleDataAccessObject
    {
        public List<StaffTitle> List()
        {
            using var ctx = new RestaurantContext();
            return ctx.StaffTitles.ToList();
        }

        public async Task<List<StaffTitle>> ListAsync()
        {
            using var ctx = new RestaurantContext();
            return await ctx.StaffTitles.ToListAsync();
        }

        public void Create(StaffTitle title)
        {
            using var ctx = new RestaurantContext();
            ctx.StaffTitles.Add(title);
            ctx.SaveChanges();
        }

        public async Task CreateAsync(StaffTitle title)
        {
            using var ctx = new RestaurantContext();
            await ctx.StaffTitles.AddAsync(title);
            await ctx.SaveChangesAsync();
        }

        public StaffTitle Read(Guid id)
        {
            using var ctx = new RestaurantContext();
            return ctx.StaffTitles.FirstOrDefault(x => x.Id == id);
        }

        public async Task<StaffTitle> ReadAsync(Guid id)
        {
            using var ctx = new RestaurantContext();
            return await ctx.StaffTitles.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(StaffTitle title)
        {
            using var ctx = new RestaurantContext();
            ctx.Entry(title).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public async Task UpdateAsync(StaffTitle title)
        {
            using var ctx = new RestaurantContext();
            ctx.Entry(title).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
        }

        public void Delete(StaffTitle title)
        {
            title.IsDeleted = true;
            Update(title);
        }

        public async Task DeleteAsync(StaffTitle title)
        {
            title.IsDeleted = true;
            await UpdateAsync(title);
        }

        public void Delete(Guid id)
        {
            var title = Read(id);
            if (title == null) return;
            Delete(title);
        }

        public async Task DeleteAsync(Guid id)
        {
            var title = await ReadAsync(id);
            await DeleteAsync(title);
        }
    }
}
