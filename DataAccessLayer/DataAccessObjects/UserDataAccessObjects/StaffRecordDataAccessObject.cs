using Microsoft.EntityFrameworkCore;
using Recodme.Academy.RestaurantApp.DataAccessLayer.Contexts;
using Recodme.Academy.RestaurantApp.DataLayer.UserRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.Academy.RestaurantApp.DataAccessLayer.DataAccessObjects.UserDataAccessObjects
{
    public class StaffRecordDataAccessObject
    {
       
        public List<StaffRecord> List()
        {
            using var ctx= new RestaurantContext();
            return ctx.StaffRecords.ToList();
        }

        public async Task<List<StaffRecord>> ListAsync()
        {
            using var ctx= new RestaurantContext();
            return await ctx.StaffRecords.ToListAsync();
        }

        public void Create(StaffRecord record)
        {
            using var ctx= new RestaurantContext();
            ctx.StaffRecords.Add(record);
            ctx.SaveChanges();
        }

        public async Task CreateAsync(StaffRecord record)
        {
            using var ctx= new RestaurantContext();
            await ctx.StaffRecords.AddAsync(record);
            await ctx.SaveChangesAsync();
        }

        public StaffRecord Read(Guid id)
        {
            using var ctx= new RestaurantContext();
            return ctx.StaffRecords.FirstOrDefault(x => x.Id == id);
        }

        public async Task<StaffRecord> ReadAsync(Guid id)
        {
            using var ctx= new RestaurantContext();
            return await ctx.StaffRecords.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(StaffRecord record)
        {
            using var ctx= new RestaurantContext();
            ctx.Entry(record).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public async Task UpdateAsync(StaffRecord record)
        {
            using var ctx= new RestaurantContext();
            ctx.Entry(record).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
        }

        public void Delete(StaffRecord record)
        {
            record.IsDeleted = true;
            Update(record);
        }

        public async Task DeleteAsync(StaffRecord record)
        {
            record.IsDeleted = true;
            await UpdateAsync(record);
        }

        public void Delete(Guid id)
        {
            var record = Read(id);
            if (record == null) return;
            Delete(record);
        }

        public async Task DeleteAsync(Guid id)
        {
            var record = await ReadAsync(id);
            await DeleteAsync(record);
        }
    }
}
