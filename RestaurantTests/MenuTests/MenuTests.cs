using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.Academy.RestaurantApp.BusinessLayer.BusinessObjects.MenuBusinessObjects;
using Recodme.Academy.RestaurantApp.BusinessLayer.BusinessObjects.RestaurantBusinessObjects;
using Recodme.Academy.RestaurantApp.DataAccessLayer.Seeders;
using Recodme.Academy.RestaurantApp.DataLayer.MenuRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantTests
{

    [TestClass]
    public class MenuTests
    {
        [TestMethod]
        public void TestCreateAndListMenu()
        {
            RestaurantSeeder.Seed();
            var mbo = new MealBusinessObject();
            var rbo = new RestaurantBusinessObject();
            var mebo = new MenuBusinessObject();

            var m = mbo.List().Result.First();
            var r = rbo.List().Result.First();

            var dr = new Menu(DateTime.Now, r.Id, m.Id);
            var resCreate = mebo.Create(dr);
            var resGet = mebo.Read(dr.Id);
            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListMenu()
        {
            RestaurantSeeder.Seed();
            var bo = new MenuBusinessObject();
            var resList = bo.List();
            Assert.IsTrue(resList.Success && resList.Result.Count > 0);
        }

        [TestMethod]
        public void TestUpdateMenu()
        {
            RestaurantSeeder.Seed();
            var bo = new MenuBusinessObject();
            var resList = bo.List();
            var item = resList.Result.FirstOrDefault();
            var now = DateTime.Now;
            item.Date = now;
            var resUpdate = bo.Update(item);
            resList = bo.ListNonDeleted();
            Assert.IsTrue(resList.Success && resUpdate.Success && resList.Result.First().Date == now);
        }

        [TestMethod]
        public void TestDeleteMenu()
        {
            RestaurantSeeder.Seed();
            var bo = new MenuBusinessObject();
            var resList = bo.List();
            var total = resList.Result.Count;
            var resDelete = bo.Delete(resList.Result.First().Id);
            resList = bo.ListNonDeleted();
            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.Count == total- 1);
        }


        [TestMethod]
        public void TestCreateAndListMenuAsync()
        {
            RestaurantSeeder.Seed();
            var mbo = new MealBusinessObject();
            var rbo = new RestaurantBusinessObject();
            var mebo = new MenuBusinessObject();

            var m = mbo.ListAsync().Result.Result.First();
            var r = rbo.ListAsync().Result.Result.First();

            var dr = new Menu(DateTime.Now, r.Id, m.Id);
            var resCreate = mebo.CreateAsync(dr).Result;
            var resGet = mebo.ReadAsync(dr.Id).Result;
            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListMenuAsync()
        {
            RestaurantSeeder.Seed();
            var bo = new MenuBusinessObject();
            var resList = bo.ListAsync().Result;
            Assert.IsTrue(resList.Success && resList.Result.Count > 0);
        }

        [TestMethod]
        public void TestUpdateMenuAsync()
        {
            RestaurantSeeder.Seed();
            var bo = new MenuBusinessObject();
            var resList = bo.ListAsync().Result;
            var item = resList.Result.FirstOrDefault();
            var now = DateTime.Now;
            item.Date = now;
            var resUpdate = bo.UpdateAsync(item).Result;
            resList = bo.ListNonDeletedAsync().Result;
            Assert.IsTrue(resList.Success && resUpdate.Success && resList.Result.First().Date == now);
        }

        [TestMethod]
        public void TestDeleteMenuAsync()
        {
            RestaurantSeeder.Seed();
            var bo = new MenuBusinessObject();
            var resList = bo.ListAsync().Result;
            var resDelete = bo.DeleteAsync(resList.Result.First().Id).Result;
            var total = resList.Result.Count;
            resList = bo.ListNonDeletedAsync().Result;
            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.Count == total - 1);
        }
    }
}
