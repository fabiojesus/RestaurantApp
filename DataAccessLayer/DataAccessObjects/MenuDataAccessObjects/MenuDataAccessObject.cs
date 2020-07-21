using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Recodme.Academy.RestaurantApp.DataLayer.MenuRecords;
using Recodme.Academy.RestaurantApp.DataAccessLayer.Contexts;

namespace Recodme.Academy.RestaurantApp.DataAccessLayer.DataAccessObjects.MenuDataAcccessObjects
{
    public class MenuDataAccessObject
    {
        public List<Menu> List()
        {
            using var ctx = new RestaurantContext();
            return ctx.Menus.ToList();
        }

        public async Task<List<Menu>> ListAsync()
        {
            using var ctx = new RestaurantContext();
            return await ctx.Menus.ToListAsync();
        }

        public void Create(Menu menu)
        {
            using var ctx = new RestaurantContext();
            ctx.Menus.Add(menu);
            ctx.SaveChanges();
        }

        public async Task CreateAsync(Menu menu)
        {
            using var ctx = new RestaurantContext();
            await ctx.Menus.AddAsync(menu);
            await ctx.SaveChangesAsync();
        }

        public Menu Read(Guid id)
        {
            using var ctx = new RestaurantContext();
            return ctx.Menus.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Menu> ReadAsync(Guid id)
        {
            using var ctx = new RestaurantContext();
            return await ctx.Menus.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(Menu menu)
        {
            using var ctx = new RestaurantContext();
            ctx.Entry(menu).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public async Task UpdateAsync(Menu menu)
        {
            using var ctx = new RestaurantContext();
            ctx.Entry(menu).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
        }

        public void Delete(Menu menu)
        {
            menu.IsDeleted = true;
            Update(menu);
        }

        public async Task DeleteAsync(Menu menu)
        {
            menu.IsDeleted = true;
            await UpdateAsync(menu);
        }

        public void Delete(Guid id)
        {
            var menu = Read(id);
            if (menu == null) return;
            Delete(menu);
        }

        public async Task DeleteAsync(Guid id)
        {
            var menu = await ReadAsync(id);
            await DeleteAsync(menu);
        }
    }
}
