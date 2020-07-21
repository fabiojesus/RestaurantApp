using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.Academy.RestaurantApp.BusinessLayer.BusinessObjects.MenuBusinessObjects;
using Recodme.Academy.RestaurantApp.DataAccessLayer.Seeders;
using Recodme.Academy.RestaurantApp.DataLayer.MenuRecords;
using System.Linq;

namespace RestaurantTests
{

    [TestClass]
    public class CourseTests
    {
        [TestMethod]
        public void TestCreateAndListCourse()
        {
            RestaurantSeeder.Seed();
            var bo = new CourseBusinessObject();
            var dr = new Course("Vegetarian");
            var resCreate = bo.Create(dr);
            var resGet = bo.Read(dr.Id);
            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestCreateAndListCourseAsync()
        {
            RestaurantSeeder.Seed();
            var bo = new CourseBusinessObject();
            var dr = new Course("Vegetarian");
            var resCreate = bo.CreateAsync(dr).Result;
            var resGet = bo.ReadAsync(dr.Id).Result;
            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListCourse()
        {
            RestaurantSeeder.Seed();
            var bo = new CourseBusinessObject();
            var resList = bo.List();
            Assert.IsTrue(resList.Success && resList.Result.Count > 0);
        }

        [TestMethod]
        public void TestListCourseAsync()
        {
            RestaurantSeeder.Seed();
            var bo = new CourseBusinessObject();
            var resList = bo.ListAsync().Result;
            Assert.IsTrue(resList.Success && resList.Result.Count > 0);
        }

        [TestMethod]
        public void TestUpdateCourse()
        {
            RestaurantSeeder.Seed();
            var bo = new CourseBusinessObject();
            var resList = bo.List();
            var item = resList.Result.FirstOrDefault();
            item.Name = "another";
            var resUpdate = bo.Update(item);
            resList = bo.ListNonDeleted();
            Assert.IsTrue(resList.Success && resUpdate.Success && resList.Result.First().Name == "another");
        }

        [TestMethod]
        public void TestUpdateCourseAsync()
        {
            RestaurantSeeder.Seed();
            var bo = new CourseBusinessObject();
            var resList = bo.ListAsync().Result;
            var item = resList.Result.FirstOrDefault();
            item.Name = "another";
            var resUpdate = bo.UpdateAsync(item).Result;
            resList = bo.ListNonDeletedAsync().Result;
            Assert.IsTrue(resList.Success && resUpdate.Success && resList.Result.First().Name == "another");
        }

        [TestMethod]
        public void TestDeleteCourse()
        {
            RestaurantSeeder.Seed();
            var bo = new CourseBusinessObject();
            var resList = bo.ListNonDeleted();
            var total = resList.Result.Count;
            var resDelete = bo.Delete(resList.Result.First().Id);
            resList = bo.ListNonDeleted();
            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.Count == total-1);
        }

        [TestMethod]
        public void TestDeleteCourseAsync()
        {
            RestaurantSeeder.Seed();
            var bo = new CourseBusinessObject();
            var resList = bo.ListAsync().Result;
            var total = resList.Result.Count;
            var resDelete = bo.Delete(resList.Result.First().Id);
            resList = bo.ListNonDeletedAsync().Result;
            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.Count == total - 1);
        }
    }
}
