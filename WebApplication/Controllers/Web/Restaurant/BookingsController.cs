using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using Recodme.Academy.RestaurantApp.BusinessLayer.BusinessObjects.RestaurantBusinessObjects;
using Recodme.Academy.RestaurantApp.BusinessLayer.BusinessObjects.UserBusinessObjects;
using Recodme.Academy.RestaurantApp.DataLayer.UserRecords;
using Recodme.Academy.RestaurantApp.WebApplication.Models.RestaurantViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models.HtmlComponents;
using WebApplication.Support;

namespace Recodme.Academy.RestaurantApp.WebApplication.Controllers.RestaurantControllers.Web.RestaurantControllers
{
    

    [Authorize]
    [Route("[controller]")]
    public class BookingsController : Controller
    {
        private readonly BookingBusinessObject _bo = new BookingBusinessObject();
        private readonly ClientRecordBusinessObject _crbo = new ClientRecordBusinessObject();
        private readonly RestaurantBusinessObject _rbo = new RestaurantBusinessObject();
        private readonly StaffRecordBusinessObject _srbo = new StaffRecordBusinessObject();
        private readonly UserManager<RestaurantUser> _uManager;

        public BookingsController(UserManager<RestaurantUser> uManager)
        {
            _uManager = uManager;
        }

        private IActionResult RecordNotFound()
        {
            TempData["Alert"] = AlertFactory.GenerateAlert(NotificationType.Information, "The record was not found");
            return RedirectToAction(nameof(Index));
        }

        private IActionResult OperationErrorBackToIndex(Exception exception)
        {
            TempData["Alert"] = AlertFactory.GenerateAlert(NotificationType.Danger, exception);
            return RedirectToAction(nameof(Index));
        }

        private IActionResult OperationErrorBackToIndex(string error)
        {
            TempData["Alert"] = AlertFactory.GenerateAlert(NotificationType.Danger, error);
            return RedirectToAction(nameof(Index));
        }

        private IActionResult OperationSuccess(string message)
        {
            TempData["Alert"] = AlertFactory.GenerateAlert(NotificationType.Success, message);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listOperation = await _bo.ListNonDeletedAsync();
            if (!listOperation.Success) return View("Error", new ErrorViewModel() { RequestId = listOperation.Exception.Message });
            var lst = new List<BookingViewModel>();
            foreach (var item in listOperation.Result)
            {
                lst.Add(BookingViewModel.Parse(item));
            }
            ViewData["Header"] = "Bookings";

            return View(lst);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var getOperation = await _bo.ReadAsync((Guid)id);
            if (!getOperation.Success) return View("Error", getOperation.Exception.Message);
            if (getOperation.Result == null) return NotFound();
            var vm = BookingViewModel.Parse(getOperation.Result);
            ViewData["Header"] = "Booking";
            return View(vm);
        }

        [HttpGet("/new")]
        public async Task<IActionResult> New()
        {
            var name = User.Identity.Name;
            var user = await _uManager.FindByNameAsync(name);
            return View();
        }


        [Authorize(Roles = "Client")]
        [HttpGet("/ClientBooking")]
        public async Task<IActionResult> ClientBooking()
        {

            var user = await _uManager.FindByNameAsync(User.Identity.Name);
            if (user == null) OperationErrorBackToIndex("Not a client");

            var getClientRecords = await _crbo.FilterAsync(x => x.PersonId == user.PersonId);
            if (!getClientRecords.Success) OperationErrorBackToIndex(getClientRecords.Exception);

            var bookings = await _bo.FilterAsync(x => getClientRecords.Result.Select(y => y.Id).Contains(x.ClientId));

            var restaurants = await _rbo.FilterAsync(x => getClientRecords.Result.Select(y => y.RestaurantId).Contains(x.Id));

            ViewData["Header"] = "Bookings";

            return View();
        }

        [Authorize(Roles = "Staff")]
        [HttpGet("/RestaurantBooking/{id}")]
        public async Task<IActionResult> StaffBooking(Guid? id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookingViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var model = vm.ToModel();
                var createOperation = await _bo.CreateAsync(model);
                if (!createOperation.Success) return View("Error", new ErrorViewModel() { RequestId = createOperation.Exception.Message });
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        [HttpGet("/edit/{id}")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var getOperation = await _bo.ReadAsync((Guid)id);
            if (!getOperation.Success) return View("Error", new ErrorViewModel() { RequestId = getOperation.Exception.Message });
            if (getOperation.Result == null) return NotFound();
            var vm = BookingViewModel.Parse(getOperation.Result);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BookingViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var getOperation = await _bo.ReadAsync(id);
                if (!getOperation.Success) return View("Error", new ErrorViewModel() { RequestId = getOperation.Exception.Message });
                if (getOperation.Result == null) return NotFound();
                var result = getOperation.Result;
                if (!vm.CompareToModel(result))
                {
                    result = vm.ToModel(result);
                    var updateOperation = await _bo.UpdateAsync(result);
                    if (!updateOperation.Success) return View("Error", new ErrorViewModel() { RequestId = updateOperation.Exception.Message });
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var deleteOperation = await _bo.DeleteAsync((Guid)id);
            if (!deleteOperation.Success) return View("Error", new ErrorViewModel() { RequestId = deleteOperation.Exception.Message });
            return RedirectToAction(nameof(Index));
        }
    }
}
