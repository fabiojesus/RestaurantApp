using Microsoft.EntityFrameworkCore;
using Recodme.Academy.RestaurantApp.DataAccessLayer.Contexts;
using Recodme.Academy.RestaurantApp.DataLayer.UserRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.Academy.RestaurantApp.DataAccessLayer.DataAccessObjects.UserDataAccessObjects
{
    public class ClientRecordDataAccessObject
    {
        public List<ClientRecord> List()
        {
            using var ctx = new RestaurantContext();
            return ctx.ClientRecords.ToList();
        }

        public async Task<List<ClientRecord>> ListAsync()
        {
            using var ctx = new RestaurantContext();
            return await ctx.ClientRecords.ToListAsync();
        }

        public void Create(ClientRecord record)
        {
            using var ctx = new RestaurantContext();
            ctx.ClientRecords.Add(record);
            ctx.SaveChanges();
        }

        public async Task CreateAsync(ClientRecord record)
        {
            using var ctx = new RestaurantContext();
            await ctx.ClientRecords.AddAsync(record);
            await ctx.SaveChangesAsync();
        }

        public ClientRecord Read(Guid id)
        {
            using var ctx = new RestaurantContext();
            return ctx.ClientRecords.FirstOrDefault(x => x.Id == id);
        }

        public async Task<ClientRecord> ReadAsync(Guid id)
        {
            using var ctx = new RestaurantContext();
            return await ctx.ClientRecords.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(ClientRecord record)
        {
            using var ctx = new RestaurantContext();
            ctx.Entry(record).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public async Task UpdateAsync(ClientRecord record)
        {
            using var ctx = new RestaurantContext();
            ctx.Entry(record).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
        }

        public void Delete(ClientRecord record)
        {
            record.IsDeleted = true;
            Update(record);
        }

        public async Task DeleteAsync(ClientRecord record)
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
