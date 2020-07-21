using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.Academy.RestaurantApp.BusinessLayer.BusinessObjects.MenuBusinessObjects;
using Recodme.Academy.RestaurantApp.DataAccessLayer.Seeders;
using Recodme.Academy.RestaurantApp.DataLayer.MenuRecords;
using System;
using System.Linq;

namespace RestaurantTests
{

    [TestClass]
    public class DishTests
    {
        [TestMethod]
        public void TestCreateAndListDish()
        {
            RestaurantSeeder.Seed();
            var dbo = new DishBusinessObject();
            var drbo = new DietaryRestrictionBusinessObject();
            var firstDietaryRestriction = drbo.List().Result.First();
            var dr = new Dish("Vegetarian", firstDietaryRestriction.Id);
            var resCreate = dbo.Create(dr);
            var resGet = dbo.Read(dr.Id);
            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListDish()
        {
            RestaurantSeeder.Seed();
            var bo = new DishBusinessObject();
            var resList = bo.List();
            Assert.IsTrue(resList.Success && resList.Result.Count > 0);
        }

        [TestMethod]
        public void TestUpdateDish()
        {
            RestaurantSeeder.Seed();
            var bo = new DishBusinessObject();
            var resList = bo.List();
            var item = resList.Result.FirstOrDefault();
            item.Name = "another";
            var resUpdate = bo.Update(item);
            resList = bo.ListNonDeleted();
            Assert.IsTrue(resList.Success && resUpdate.Success && resList.Result.First().Name == "another");
        }

        [TestMethod]
        public void TestDeleteDish()
        {
            RestaurantSeeder.Seed();
            var bo = new DishBusinessObject();
            var resList = bo.List();
            var total = resList.Result.Count;
            var resDelete = bo.Delete(resList.Result.First().Id);
            resList = bo.ListNonDeleted();
            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.Count == total-1);
        }

        [TestMethod]
        public void TestCreateAndListDishAsync()
        {
            RestaurantSeeder.Seed();
            var dbo = new DishBusinessObject();
            var drbo = new DietaryRestrictionBusinessObject();
            var firstDietaryRestriction = drbo.ListAsync().Result.Result.First();
            var dr = new Dish("Vegetarian", firstDietaryRestriction.Id);
            var resCreate = dbo.CreateAsync(dr).Result;
            var resGet = dbo.ReadAsync(dr.Id).Result;
            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListDishAsync()
        {
            RestaurantSeeder.Seed();
            var bo = new DishBusinessObject();
            var resList = bo.ListAsync().Result;
            Assert.IsTrue(resList.Success && resList.Result.Count > 0);
        }

        [TestMethod]
        public void TestUpdateDishAsync()
        {
            RestaurantSeeder.Seed();
            var bo = new DishBusinessObject();
            var resList = bo.ListAsync().Result;
            var item = resList.Result.FirstOrDefault();
            item.Name = "another";
            var resUpdate = bo.UpdateAsync(item).Result;
            resList = bo.ListNonDeletedAsync().Result;
            Assert.IsTrue(resList.Success && resUpdate.Success && resList.Result.First().Name == "another");
        }

        [TestMethod]
        public void TestDeleteDishAsync()
        {
            RestaurantSeeder.Seed();
            var bo = new DishBusinessObject();
            var resList = bo.ListAsync().Result;
            var resDelete = bo.DeleteAsync(resList.Result.First().Id).Result;
            resList = bo.ListNonDeletedAsync().Result;
            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.Count > 0);
        }
    }
}
