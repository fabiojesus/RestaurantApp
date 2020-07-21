using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.Academy.RestaurantApp.BusinessLayer.BusinessObjects.RestaurantBusinessObjects;
using Recodme.Academy.RestaurantApp.DataAccessLayer.Seeders;
using Recodme.Academy.RestaurantApp.DataLayer.RestaurantRecords;
using System.Linq;

namespace RestaurantTests
{

    [TestClass]
    public class RestaurantTests
    {
        [TestMethod]
        public void TestCreateAndListRestaurant()
        {
            RestaurantSeeder.Seed();
            var dbo = new RestaurantBusinessObject();
            var dr = new Restaurant("asdasd", "owewq", "123", "1232", "23ed", 4);
            var resCreate = dbo.Create(dr);
            var resGet = dbo.Read(dr.Id);
            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListRestaurant()
        {
            RestaurantSeeder.Seed();
            var bo = new RestaurantBusinessObject();
            var resList = bo.List();
            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestUpdateRestaurant()
        {
            RestaurantSeeder.Seed();
            var bo = new RestaurantBusinessObject();
            var resList = bo.List();
            var item = resList.Result.FirstOrDefault();
            item.Name = "another";
            var resUpdate = bo.Update(item);
            resList = bo.ListNonDeleted();
            Assert.IsTrue(resList.Success && resUpdate.Success && resList.Result.First().Name == "another");
        }

        [TestMethod]
        public void TestDeleteRestaurant()
        {
            RestaurantSeeder.Seed();
            var bo = new RestaurantBusinessObject();
            var resList = bo.List();
            var resDelete = bo.Delete(resList.Result.First().Id);
            resList = bo.ListNonDeleted();
            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.Count == 0);
        }


        [TestMethod]
        public void TestCreateAndListRestaurantAsync()
        {
            RestaurantSeeder.Seed();
            var dbo = new RestaurantBusinessObject();
            var dr = new Restaurant("asdasd", "owewq", "123", "1232", "23ed", 4);
            var resCreate = dbo.CreateAsync(dr).Result;
            var resGet = dbo.ReadAsync(dr.Id).Result;
            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListRestaurantAsync()
        {
            RestaurantSeeder.Seed();
            var bo = new RestaurantBusinessObject();
            var resList = bo.ListAsync().Result;
            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestUpdateRestaurantAsync()
        {
            RestaurantSeeder.Seed();
            var bo = new RestaurantBusinessObject();
            var resList = bo.ListAsync().Result;
            var item = resList.Result.FirstOrDefault();
            item.Name = "another";
            var resUpdate = bo.UpdateAsync(item).Result;
            resList = bo.ListNonDeletedAsync().Result;
            Assert.IsTrue(resList.Success && resUpdate.Success && resList.Result.First().Name == "another");
        }

        [TestMethod]
        public void TestDeleteRestaurantAsync()
        {
            RestaurantSeeder.Seed();
            var bo = new RestaurantBusinessObject();
            var resList = bo.ListAsync().Result;
            var resDelete = bo.DeleteAsync(resList.Result.First().Id).Result;
            resList = bo.ListNonDeletedAsync().Result;
            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.Count == 0);
        }
    }
}
