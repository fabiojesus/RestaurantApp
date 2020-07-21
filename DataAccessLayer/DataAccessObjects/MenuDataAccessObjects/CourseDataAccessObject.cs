using Microsoft.EntityFrameworkCore;
using Recodme.Academy.RestaurantApp.DataAccessLayer.Contexts;
using Recodme.Academy.RestaurantApp.DataLayer.MenuRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.Academy.RestaurantApp.DataAccessLayer.DataAccessObjects.MenuDataAcccessObjects
{
    public class CourseDataAccessObject
    {

        public List<Course> List()
        {
            using var ctx = new RestaurantContext();
            return ctx.Courses.ToList();
        }

        public async Task<List<Course>> ListAsync()
        {
            using var ctx = new RestaurantContext();
            return await ctx.Courses.ToListAsync();
        }

        public void Create(Course course)
        {
            using var ctx = new RestaurantContext();
            ctx.Courses.Add(course);
            ctx.SaveChanges();
        }

        public async Task CreateAsync(Course course)
        {
            using var ctx = new RestaurantContext();
            await ctx.Courses.AddAsync(course);
            await ctx.SaveChangesAsync();
        }

        public Course Read(Guid id)
        {
            using var ctx = new RestaurantContext();
            return ctx.Courses.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Course> ReadAsync(Guid id)
        {
            using var ctx = new RestaurantContext();
            return await ctx.Courses.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(Course course)
        {
            using var ctx = new RestaurantContext();
            ctx.Entry(course).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public async Task UpdateAsync(Course course)
        {
            using var ctx = new RestaurantContext();
            ctx.Entry(course).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
        }

        public void Delete(Course course)
        {
            course.IsDeleted = true;
            Update(course);
        }

        public async Task DeleteAsync(Course course)
        {
            course.IsDeleted = true;
            await UpdateAsync(course);
        }

        public void Delete(Guid id)
        {
            var course = Read(id);
            if (course == null) return;
            Delete(course);
        }

        public async Task DeleteAsync(Guid id)
        {
            var course = await ReadAsync(id);
            await DeleteAsync(course);
        }
    }
}
