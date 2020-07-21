using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.Academy.RestaurantApp.BusinessLayer.BusinessObjects.RestaurantBusinessObjects;
using Recodme.Academy.RestaurantApp.BusinessLayer.BusinessObjects.UserBusinessObjects;
using Recodme.Academy.RestaurantApp.DataAccessLayer.Seeders;
using Recodme.Academy.RestaurantApp.DataLayer.RestaurantRecords;
using Recodme.Academy.RestaurantApp.DataLayer.UserRecords;
using System;
using System.Linq;
using System.Text;

namespace RestaurantTests
{

    [TestClass]
    public class ClientRecordTests
    {
        [TestMethod]
        public void TestCreateAndListClientRecord()
        {
            RestaurantSeeder.Seed();
            var sbo = new ClientRecordBusinessObject();
            var rbo = new RestaurantBusinessObject();
            var pbo = new PersonBusinessObject();

            var r = rbo.List().Result.First();
            var p = pbo.List().Result.First();


            var dr = new ClientRecord(p.Id, r.Id, DateTime.Now);
            var resCreate = sbo.Create(dr);
            var resGet = sbo.Read(dr.Id);
            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestCreateAndListClientRecordAsync()
        {
            RestaurantSeeder.Seed();
            var sbo = new ClientRecordBusinessObject();
            var rbo = new RestaurantBusinessObject();
            var pbo = new PersonBusinessObject();

            var r = rbo.ListAsync().Result.Result.First();
            var p = pbo.ListAsync().Result.Result.First();


            var dr = new ClientRecord(p.Id, r.Id, DateTime.Now);
            var resCreate = sbo.CreateAsync(dr).Result;
            var resGet = sbo.ReadAsync(dr.Id).Result;
            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListClientRecord()
        {
            RestaurantSeeder.Seed();
            var bo = new ClientRecordBusinessObject();
            var resList = bo.List();
            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestListClientRecordAsync()
        {
            RestaurantSeeder.Seed();
            var bo = new ClientRecordBusinessObject();
            var resList = bo.ListAsync().Result;
            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestUpdateClientRecord()
        {
            RestaurantSeeder.Seed();
            var bo = new ClientRecordBusinessObject();
            var resList = bo.List();
            var item = resList.Result.FirstOrDefault();
            var now = DateTime.Now;
            item.RegisterDate = now;
            var resUpdate = bo.Update(item);
            resList = bo.ListNonDeleted();
            Assert.IsTrue(resList.Success && resUpdate.Success && resList.Result.First().RegisterDate == now);
        }

        [TestMethod]
        public void TestUpdateClientRecordAsync()
        {
            RestaurantSeeder.Seed();
            var bo = new ClientRecordBusinessObject();
            var resList = bo.ListAsync().Result;
            var item = resList.Result.FirstOrDefault();
            var now = DateTime.Now;
            item.RegisterDate = now;
            var resUpdate = bo.UpdateAsync(item).Result;
            resList = bo.ListNonDeletedAsync().Result;
            Assert.IsTrue(resList.Success && resUpdate.Success && resList.Result.First().RegisterDate == now);
        }

        [TestMethod]
        public void TestDeleteClientRecord()
        {
            RestaurantSeeder.Seed();
            var bo = new ClientRecordBusinessObject();
            var resList = bo.List();
            var resDelete = bo.Delete(resList.Result.First().Id);
            resList = bo.ListNonDeleted();
            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.Count == 0);
        }

        [TestMethod]
        public void TestDeleteClientRecordAsync()
        {
            RestaurantSeeder.Seed();
            var bo = new ClientRecordBusinessObject();
            var resList = bo.ListAsync().Result;
            var resDelete = bo.Delete(resList.Result.First().Id);
            resList = bo.ListNonDeletedAsync().Result;
            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.Count == 0);
        }
    }
}
