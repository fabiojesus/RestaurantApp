using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.Academy.RestaurantApp.BusinessLayer.BusinessObjects.RestaurantBusinessObjects;
using Recodme.Academy.RestaurantApp.BusinessLayer.BusinessObjects.UserBusinessObjects;
using Recodme.Academy.RestaurantApp.DataAccessLayer.Seeders;
using Recodme.Academy.RestaurantApp.DataLayer.RestaurantRecords;
using Recodme.Academy.RestaurantApp.DataLayer.UserRecords;
using System;
using System.Linq;

namespace RestaurantTests
{

    [TestClass]
    public class BookingTests
    {
        [TestMethod]
        public void TestCreateAndListBooking()
        {
            RestaurantSeeder.Seed();
            var cbo = new ClientRecordBusinessObject();
            var bbo = new BookingBusinessObject();

            var c = cbo.List().Result.First();

            var dr = new Booking(DateTime.Now, c.Id);
            var resCreate = bbo.Create(dr);
            var resGet = bbo.Read(dr.Id);
            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestCreateAndListBookingAsync()
        {
            RestaurantSeeder.Seed();
            var cbo = new ClientRecordBusinessObject();
            var bbo = new BookingBusinessObject();

            var c = cbo.ListAsync().Result.Result.First();

            var dr = new Booking(DateTime.Now, c.Id);
            var resCreate = bbo.CreateAsync(dr).Result;
            var resGet = bbo.ReadAsync(dr.Id).Result;
            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListBooking()
        {
            RestaurantSeeder.Seed();
            var bo = new BookingBusinessObject();
            var resList = bo.List();
            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestListBookingAsync()
        {
            RestaurantSeeder.Seed();
            var bo = new BookingBusinessObject();
            var resList = bo.ListAsync().Result;
            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestUpdateBooking()
        {
            RestaurantSeeder.Seed();
            var bo = new BookingBusinessObject();
            var resList = bo.List();
            var item = resList.Result.FirstOrDefault();
            var now = DateTime.Now;
            item.Date = now;
            var resUpdate = bo.Update(item);
            resList = bo.ListNonDeleted();
            Assert.IsTrue(resList.Success && resUpdate.Success && resList.Result.First().Date == now);
        }

        [TestMethod]
        public void TestUpdateBookingAsync()
        {
            RestaurantSeeder.Seed();
            var bo = new BookingBusinessObject();
            var resList = bo.List();
            var item = resList.Result.FirstOrDefault();
            var now = DateTime.Now;
            item.Date = now;
            var resUpdate = bo.Update(item);
            resList = bo.ListNonDeleted();
            Assert.IsTrue(resList.Success && resUpdate.Success && resList.Result.First().Date == now);
        }

        [TestMethod]
        public void TestDeleteBooking()
        {
            RestaurantSeeder.Seed();
            var bo = new BookingBusinessObject();
            var resList = bo.List();
            var resDelete = bo.Delete(resList.Result.First().Id);
            resList = bo.ListNonDeleted();
            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.Count == 0);
        }

        [TestMethod]
        public void TestDeleteBookingAsync()
        {
            RestaurantSeeder.Seed();
            var bo = new BookingBusinessObject();
            var resList = bo.ListAsync().Result;
            var resDelete = bo.DeleteAsync(resList.Result.First().Id).Result;
            resList = bo.ListNonDeletedAsync().Result;
            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.Count == 0);
        }
    }
}
