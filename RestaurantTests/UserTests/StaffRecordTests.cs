using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.Academy.RestaurantApp.BusinessLayer.BusinessObjects.RestaurantBusinessObjects;
using Recodme.Academy.RestaurantApp.BusinessLayer.BusinessObjects.UserBusinessObjects;
using Recodme.Academy.RestaurantApp.DataAccessLayer.Seeders;
using Recodme.Academy.RestaurantApp.DataLayer.UserRecords;
using System;
using System.Linq;

namespace RestaurantTests
{

    [TestClass]
    public class StaffRecordTests
    {
        [TestMethod]
        public void TestCreateAndListStaffRecord()
        {
            RestaurantSeeder.Seed();
            var sbo = new StaffRecordBusinessObject();
            var rbo = new RestaurantBusinessObject();
            var pbo = new PersonBusinessObject();

            var r = rbo.List().Result.First();
            var p = pbo.List().Result.First();


            var dr = new StaffRecord(p.Id, r.Id, DateTime.Now, DateTime.Now);
            var resCreate = sbo.Create(dr);
            var resGet = sbo.Read(dr.Id);
            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListStaffRecord()
        {
            RestaurantSeeder.Seed();
            var bo = new StaffRecordBusinessObject();
            var resList = bo.List();
            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestUpdateStaffRecord()
        {
            RestaurantSeeder.Seed();
            var bo = new StaffRecordBusinessObject();
            var resList = bo.List();
            var item = resList.Result.FirstOrDefault();
            var now = DateTime.Now;
            item.BeginDate = now;
            var resUpdate = bo.Update(item);
            resList = bo.ListNonDeleted();
            Assert.IsTrue(resList.Success && resUpdate.Success && resList.Result.First().BeginDate == now);
        }

        [TestMethod]
        public void TestDeleteStaffRecord()
        {
            RestaurantSeeder.Seed();
            var bo = new StaffRecordBusinessObject();
            var resList = bo.List();
            var resDelete = bo.Delete(resList.Result.First().Id);
            resList = bo.ListNonDeleted();
            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.Count == 0);
        }

        [TestMethod]
        public void TestCreateAndListStaffRecordAsync()
        {
            RestaurantSeeder.Seed();
            var sbo = new StaffRecordBusinessObject();
            var rbo = new RestaurantBusinessObject();
            var pbo = new PersonBusinessObject();

            var r = rbo.ListAsync().Result.Result.First();
            var p = pbo.ListAsync().Result.Result.First();


            var dr = new StaffRecord(p.Id, r.Id, DateTime.Now, DateTime.Now);
            var resCreate = sbo.CreateAsync(dr).Result;
            var resGet = sbo.ReadAsync(dr.Id).Result;
            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListStaffRecordAsync()
        {
            RestaurantSeeder.Seed();
            var bo = new StaffRecordBusinessObject();
            var resList = bo.ListAsync().Result;
            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestUpdateStaffRecordAsync()
        {
            RestaurantSeeder.Seed();
            var bo = new StaffRecordBusinessObject();
            var resList = bo.ListAsync().Result;
            var item = resList.Result.FirstOrDefault();
            var now = DateTime.Now;
            item.BeginDate = now;
            var resUpdate = bo.UpdateAsync(item).Result;
            resList = bo.ListNonDeletedAsync().Result;
            Assert.IsTrue(resList.Success && resUpdate.Success && resList.Result.First().BeginDate == now);
        }

        [TestMethod]
        public void TestDeleteStaffRecordAsync()
        {
            RestaurantSeeder.Seed();
            var bo = new StaffRecordBusinessObject();
            var resList = bo.ListAsync().Result;
            var resDelete = bo.DeleteAsync(resList.Result.First().Id).Result;
            resList = bo.ListNonDeletedAsync().Result;
            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.Count == 0);
        }
    }
}
