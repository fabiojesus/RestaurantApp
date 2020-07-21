using Microsoft.EntityFrameworkCore;
using Recodme.Academy.RestaurantApp.DataAccessLayer.Contexts;
using Recodme.Academy.RestaurantApp.DataLayer.RestaurantRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.Academy.RestaurantApp.DataAccessLayer.DataAccessObjects.RestaurantDataAcccessObjects
{
    public class TitleDataAccessObject
    {
        public List<Title> List()
        {
            using var ctx = new RestaurantContext();
            return ctx.Titles.ToList();
        }

        public async Task<List<Title>> ListAsync()
        {
            using var ctx = new RestaurantContext();
            return await ctx.Titles.ToListAsync();
        }

        public void Create(Title title)
        {
            using var ctx = new RestaurantContext();
            ctx.Titles.Add(title);
            ctx.SaveChanges();
        }

        public async Task CreateAsync(Title title)
        {
            using var ctx = new RestaurantContext();
            await ctx.Titles.AddAsync(title);
            await ctx.SaveChangesAsync();
        }

        public Title Read(Guid id)
        {
            using var ctx = new RestaurantContext();
            return ctx.Titles.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Title> ReadAsync(Guid id)
        {
            using var ctx = new RestaurantContext();
            return await ctx.Titles.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(Title title)
        {
            using var ctx = new RestaurantContext();
            ctx.Entry(title).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public async Task UpdateAsync(Title title)
        {
            using var ctx = new RestaurantContext();
            ctx.Entry(title).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
        }

        public void Delete(Title title)
        {
            title.IsDeleted = true;
            Update(title);
        }

        public async Task DeleteAsync(Title title)
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
