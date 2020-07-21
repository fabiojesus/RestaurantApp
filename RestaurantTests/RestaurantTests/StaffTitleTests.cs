using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.Academy.RestaurantApp.BusinessLayer.BusinessObjects.RestaurantBusinessObjects;
using Recodme.Academy.RestaurantApp.BusinessLayer.BusinessObjects.UserBusinessObjects;
using Recodme.Academy.RestaurantApp.DataAccessLayer.Seeders;
using Recodme.Academy.RestaurantApp.DataLayer.RestaurantRecords;
using System;
using System.Linq;

namespace RestaurantTests
{

    [TestClass]
    public class StaffTitleTests
    {
        [TestMethod]
        public void TestCreateAndListStaffTitle()
        {
            RestaurantSeeder.Seed();
            var sbo = new StaffTitleBusinessObject();
            var tbo = new TitleBusinessObject();
            var srbo = new StaffRecordBusinessObject();

            var t = tbo.List().Result.First();
            var sr = srbo.List().Result.First();


            var dr = new StaffTitle(DateTime.Now, DateTime.Now, sr.Id, t.Id);
            var resCreate = sbo.Create(dr);
            var resGet = sbo.Read(dr.Id);
            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListStaffTitle()
        {
            RestaurantSeeder.Seed();
            var bo = new StaffTitleBusinessObject();
            var resList = bo.List();
            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestUpdateStaffTitle()
        {
            RestaurantSeeder.Seed();
            var bo = new StaffTitleBusinessObject();
            var resList = bo.List();
            var item = resList.Result.FirstOrDefault();
            var now = DateTime.Now;
            item.BeginDate = now;
            var resUpdate = bo.Update(item);
            resList = bo.ListNonDeleted();
            Assert.IsTrue(resList.Success && resUpdate.Success && resList.Result.First().BeginDate == now);
        }

        [TestMethod]
        public void TestDeleteStaffTitle()
        {
            RestaurantSeeder.Seed();
            var bo = new StaffTitleBusinessObject();
            var resList = bo.List();
            var resDelete = bo.Delete(resList.Result.First().Id);
            resList = bo.ListNonDeleted();
            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.Count == 0);
        }


        [TestMethod]
        public void TestCreateAndListStaffTitleAsync()
        {
            RestaurantSeeder.Seed();
            var sbo = new StaffTitleBusinessObject();
            var tbo = new TitleBusinessObject();
            var srbo = new StaffRecordBusinessObject();

            var t = tbo.ListAsync().Result.Result.First();
            var sr = srbo.ListAsync().Result.Result.First();


            var dr = new StaffTitle(DateTime.Now, DateTime.Now, sr.Id, t.Id);
            var resCreate = sbo.CreateAsync(dr).Result;
            var resGet = sbo.ReadAsync(dr.Id).Result;
            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListStaffTitleAsync()
        {
            RestaurantSeeder.Seed();
            var bo = new StaffTitleBusinessObject();
            var resList = bo.ListAsync().Result;
            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestUpdateStaffTitleAsync()
        {
            RestaurantSeeder.Seed();
            var bo = new StaffTitleBusinessObject();
            var resList = bo.ListAsync().Result;
            var item = resList.Result.FirstOrDefault();
            var now = DateTime.Now;
            item.BeginDate = now;
            var resUpdate = bo.UpdateAsync(item).Result;
            resList = bo.ListNonDeletedAsync().Result;
            Assert.IsTrue(resList.Success && resUpdate.Success && resList.Result.First().BeginDate == now);
        }

        [TestMethod]
        public void TestDeleteStaffTitleAsync()
        {
            RestaurantSeeder.Seed();
            var bo = new StaffTitleBusinessObject();
            var resList = bo.ListAsync().Result;
            var resDelete = bo.DeleteAsync(resList.Result.First().Id).Result;
            resList = bo.ListNonDeletedAsync().Result;
            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.Count == 0);
        }
    }
}
