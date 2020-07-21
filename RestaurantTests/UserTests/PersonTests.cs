using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.Academy.RestaurantApp.BusinessLayer.BusinessObjects.UserBusinessObjects;
using Recodme.Academy.RestaurantApp.DataAccessLayer.Seeders;
using Recodme.Academy.RestaurantApp.DataLayer.UserRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantTests
{

    [TestClass]
    public class PersonTests
    {
        [TestMethod]
        public void TestCreateAndListPerson()
        {
            RestaurantSeeder.Seed();
            var bo = new PersonBusinessObject();
            var dr = new Person(DateTime.Now, "A", "B", 1203, 1203);
            var resCreate = bo.Create(dr);
            var resGet = bo.Read(dr.Id);
            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListPerson()
        {
            RestaurantSeeder.Seed();
            var bo = new PersonBusinessObject();
            var resList = bo.List();
            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestUpdatePerson()
        {
            RestaurantSeeder.Seed();
            var bo = new PersonBusinessObject();
            var resList = bo.List();
            var item = resList.Result.FirstOrDefault();
            item.FirstName = "another";
            var resUpdate = bo.Update(item);
            resList = bo.ListNonDeleted();
            Assert.IsTrue(resList.Success && resUpdate.Success && resList.Result.First().FirstName == "another");
        }

        [TestMethod]
        public void TestDeletePerson()
        {
            RestaurantSeeder.Seed();
            var bo = new PersonBusinessObject();
            var resList = bo.List();
            var resDelete = bo.Delete(resList.Result.First().Id);
            resList = bo.ListNonDeleted();
            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.Count == 0);
        }


        [TestMethod]
        public void TestCreateAndListPersonAsync()
        {
            RestaurantSeeder.Seed();
            var bo = new PersonBusinessObject();
            var dr = new Person(DateTime.Now, "A", "B", 1203, 1203);
            var resCreate = bo.CreateAsync(dr).Result;
            var resGet = bo.ReadAsync(dr.Id).Result;
            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListPersonAsync()
        {
            RestaurantSeeder.Seed();
            var bo = new PersonBusinessObject();
            var resList = bo.ListAsync().Result;
            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestUpdatePersonAsync()
        {
            RestaurantSeeder.Seed();
            var bo = new PersonBusinessObject();
            var resList = bo.ListAsync().Result;
            var item = resList.Result.FirstOrDefault();
            item.FirstName = "another";
            var resUpdate = bo.UpdateAsync(item).Result;
            resList = bo.ListNonDeletedAsync().Result;
            Assert.IsTrue(resList.Success && resUpdate.Success && resList.Result.First().FirstName == "another");
        }

        [TestMethod]
        public void TestDeletePersonAsync()
        {
            RestaurantSeeder.Seed();
            var bo = new PersonBusinessObject();
            var resList = bo.ListAsync().Result;
            var resDelete = bo.DeleteAsync(resList.Result.First().Id).Result;
            resList = bo.ListNonDeletedAsync().Result;
            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.Count == 0);
        }
    }
}
