using Microsoft.EntityFrameworkCore;
using Recodme.Academy.RestaurantApp.DataAccessLayer.Contexts;
using Recodme.Academy.RestaurantApp.DataLayer.UserRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.Academy.RestaurantApp.DataAccessLayer.DataAccessObjects.UserDataAccessObjects
{
    public class PersonDataAccessObject
    {
        public List<Person> List()
        {
            using var ctx = new RestaurantContext();
            return ctx.People.ToList();
        }

        public async Task<List<Person>> ListAsync()
        {
            using var ctx = new RestaurantContext();
            return await ctx.People.ToListAsync();
        }

        public void Create(Person person)
        {
            using var ctx = new RestaurantContext();
            ctx.People.Add(person);
            ctx.SaveChanges();
        }

        public async Task CreateAsync(Person person)
        {
            using var ctx = new RestaurantContext();
            await ctx.People.AddAsync(person);
            await ctx.SaveChangesAsync();
        }

        public Person Read(Guid id)
        {
            using var ctx = new RestaurantContext();
            return ctx.People.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Person> ReadAsync(Guid id)
        {
            using var ctx = new RestaurantContext();
            return await ctx.People.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(Person person)
        {
            using var ctx = new RestaurantContext();
            ctx.Entry(person).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public async Task UpdateAsync(Person person)
        {
            using var ctx = new RestaurantContext();
            ctx.Entry(person).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
        }

        public void Delete(Person person)
        {
            person.IsDeleted = true;
            Update(person);
        }

        public async Task DeleteAsync(Person person)
        {
            person.IsDeleted = true;
            await UpdateAsync(person);
        }

        public void Delete(Guid id)
        {
            var person = Read(id);
            if (person == null) return;
            Delete(person);
        }

        public async Task DeleteAsync(Guid id)
        {
            var person = await ReadAsync(id);
            await DeleteAsync(person);
        }

    }
}
