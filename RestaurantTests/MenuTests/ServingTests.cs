using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.Academy.RestaurantApp.DataAccessLayer.Seeders;
using System;
using System.Linq;
using Recodme.Academy.RestaurantApp.DataLayer.MenuRecords;
using Recodme.Academy.RestaurantApp.BusinessLayer.BusinessObjects.MenuBusinessObjects;

namespace RestaurantTests
{

    [TestClass]
    public class ServingTests
    {
        [TestMethod]
        public void TestCreateAndListServing()
        {
            RestaurantSeeder.Seed();
            var cbo = new CourseBusinessObject();
            var dbo = new DishBusinessObject();
            var mbo = new MenuBusinessObject();
            var sbo = new ServingBusinessObject();

            var c = cbo.List().Result.First();
            var d = dbo.List().Result.First();
            var m = mbo.List().Result.First();

            var dr = new Serving(m.Id, d.Id, c.Id);
            var resCreate = sbo.Create(dr);
            var resGet = sbo.Read(dr.Id);
            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListServingAsync()
        {
            RestaurantSeeder.Seed();
            var bo = new ServingBusinessObject();
            var resList = bo.List();
            Assert.IsTrue(resList.Success && resList.Result.Count > 0);
        }

        [TestMethod]
        public void TestUpdateServing()
        {
            RestaurantSeeder.Seed();
            var bo = new ServingBusinessObject();
            var mbo = new MenuBusinessObject();
            var resList = bo.List();
            var item = resList.Result.FirstOrDefault();
            var newMenuId = mbo.List().Result.First(x => x.Id != item.MenuId).Id;
            item.MenuId = newMenuId;
            var resUpdate = bo.Update(item);
            resList = bo.ListNonDeleted();
            Assert.IsTrue(resList.Success && resUpdate.Success && resList.Result.First().MenuId == newMenuId);
        }

        [TestMethod]
        public void TestDeleteServing()
        {
            RestaurantSeeder.Seed();
            var bo = new ServingBusinessObject();
            var resList = bo.List();
            var total = resList.Result.Count;
            var resDelete = bo.Delete(resList.Result.First().Id);
            resList = bo.ListNonDeleted();
            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.Count == total-1);
        }

        [TestMethod]
        public void TestCreateAndListServingAsync()
        {
            RestaurantSeeder.Seed();
            var cbo = new CourseBusinessObject();
            var dbo = new DishBusinessObject();
            var mbo = new MenuBusinessObject();
            var sbo = new ServingBusinessObject();

            var c = cbo.ListAsync().Result.Result.First();
            var d = dbo.ListAsync().Result.Result.First();
            var m = mbo.ListAsync().Result.Result.First();

            var dr = new Serving(m.Id, d.Id, c.Id);
            var resCreate = sbo.CreateAsync(dr).Result;
            var resGet = sbo.ReadAsync(dr.Id).Result;
            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListServing()
        {
            RestaurantSeeder.Seed();
            var bo = new ServingBusinessObject();
            var resList = bo.ListAsync().Result;
            var total = resList.Result.Count;
            Assert.IsTrue(resList.Success && resList.Result.Count > 0);
        }

        [TestMethod]
        public void TestUpdateServingAsync()
        {
            RestaurantSeeder.Seed();
            var bo = new ServingBusinessObject();
            var mbo = new MenuBusinessObject();
            var resList = bo.ListAsync().Result;
            var item = resList.Result.FirstOrDefault();
            var newMenuId = mbo.ListAsync().Result.Result.First(x => x.Id != item.MenuId).Id;
            item.MenuId = newMenuId;
            var resUpdate = bo.UpdateAsync(item).Result;
            resList = bo.ListNonDeletedAsync().Result;
            Assert.IsTrue(resList.Success && resUpdate.Success && resList.Result.First().MenuId == newMenuId);
        }

        [TestMethod]
        public void TestDeleteServingAsync()
        {
            RestaurantSeeder.Seed();
            var bo = new ServingBusinessObject();
            var resList = bo.ListAsync().Result;
            var total = resList.Result.Count;
            var resDelete = bo.DeleteAsync(resList.Result.First().Id).Result;
            resList = bo.ListNonDeletedAsync().Result;
            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.Count == total-1);
        }

    }
}
