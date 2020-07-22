using Microsoft.AspNetCore.Identity;
using Recodme.Academy.RestaurantApp.DataAccessLayer.Contexts;
using Recodme.Academy.RestaurantApp.DataLayer.MenuRecords;
using Recodme.Academy.RestaurantApp.DataLayer.RestaurantRecords;
using Recodme.Academy.RestaurantApp.DataLayer.UserRecords;
using System;
using System.Collections.Generic;

namespace Recodme.Academy.RestaurantApp.DataAccessLayer.Seeders
{
    public static class RestaurantSeeder
    {
        public static void Seed()
        {
            using var _ctx = new RestaurantContext();
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();

            var dr = new List<DietaryRestriction>()
            {
                new DietaryRestriction("Vegan"),
                new DietaryRestriction("Vegetarian"),
                new DietaryRestriction("Pescatarian"),
                new DietaryRestriction("Omni")
            };

            var c = new List<Course>()
            {
                new Course("Entry"),
                new Course("Main dish"),
                new Course("Dessert"),
                new Course("Drink")
            };

            var m = new List<Meal>()
            {
                new Meal("Breakfast", "8:00", "12:00"),
                new Meal("Lunch", "12:00", "14:00"),
                new Meal("Tea", "16:00", "17:00"),
                new Meal("Dinner", "18:00", "21:00")
            };

            var p1 = new Person(DateTime.Now, "A", "B", 1203, 1203);
            
            var r1 = new Restaurant("asd", "owewq", "123", "1232", "23ed", 4);
            
            var cr1 = new ClientRecord(p1.Id, r1.Id, DateTime.Now);
            
            var t1 = new Title("123", "4134", "woe");
            
            var sr1 = new StaffRecord(p1.Id, r1.Id, DateTime.Now, DateTime.Now);
            
            var st1 = new StaffTitle(DateTime.Now, DateTime.Now, sr1.Id, t1.Id);
            
            var b1 = new Booking(DateTime.Now, cr1.Id);

            var me = new List<Menu>()
            {
                new Menu(DateTime.Now, r1.Id, m[0].Id),
                new Menu(DateTime.Now, r1.Id, m[1].Id),
                new Menu(DateTime.Now, r1.Id, m[2].Id),
                new Menu(DateTime.Now, r1.Id, m[3].Id)
            };

            var dsh = new List<Dish>
            {
                new Dish("Roasted Esparagus", dr[0].Id),
                new Dish("Veggie Lasagna", dr[0].Id),
                new Dish("Brownie Vegan", dr[0].Id),
                new Dish("Poached Egg", dr[1].Id),
                new Dish("Small Fish from the garden", dr[1].Id),
                new Dish("Green Broth Sorbet", dr[1].Id),
                new Dish("Toast and Fish Eggs", dr[2].Id),
                new Dish("CodFish at Joseph Small Barrel", dr[2].Id),
                new Dish("Sardine Panacotta", dr[2].Id),
                new Dish("Beef Tartar", dr[3].Id),
                new Dish("Female Gardner", dr[3].Id),
                new Dish("Camel's Drool", dr[3].Id)
            };

            var se = new List<Serving>()
            {
                new Serving(me[0].Id, dsh[0].Id, c[0].Id),
                new Serving(me[0].Id, dsh[1].Id, c[1].Id),
                new Serving(me[0].Id, dsh[2].Id, c[2].Id),
                new Serving(me[0].Id, dsh[3].Id, c[0].Id),
                new Serving(me[0].Id, dsh[4].Id, c[1].Id),
                new Serving(me[0].Id, dsh[5].Id, c[2].Id),
                new Serving(me[0].Id, dsh[6].Id, c[0].Id),
                new Serving(me[0].Id, dsh[7].Id, c[1].Id),
                new Serving(me[0].Id, dsh[8].Id, c[2].Id),
                new Serving(me[0].Id, dsh[9].Id, c[0].Id),
                new Serving(me[0].Id, dsh[10].Id, c[1].Id),
                new Serving(me[0].Id, dsh[11].Id, c[2].Id)
        };

            _ctx.DietaryRestrictions.AddRange(dr);
            _ctx.ClientRecords.AddRange(cr1);
            _ctx.StaffRecords.AddRange(sr1);
            _ctx.StaffTitles.AddRange(st1);
            _ctx.Restaurants.AddRange(r1);
            _ctx.Servings.AddRange(se);
            _ctx.Bookings.AddRange(b1);
            _ctx.Dishes.AddRange(dsh);
            _ctx.Courses.AddRange(c);
            _ctx.Titles.AddRange(t1);
            _ctx.People.AddRange(p1);
            _ctx.Meals.AddRange(m);
            _ctx.Menus.AddRange(me);
            _ctx.SaveChanges();
        }
    }
}
