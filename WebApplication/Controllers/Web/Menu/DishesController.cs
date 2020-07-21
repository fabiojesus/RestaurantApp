using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PresentationLayer.Models;
using Recodme.Academy.RestaurantApp.BusinessLayer.BusinessObjects.MenuBusinessObjects;
using Recodme.Academy.RestaurantApp.DataLayer.MenuRecords;
using Recodme.Academy.RestaurantApp.WebApplication.Models.MenuViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models.HtmlComponents;
using WebApplication.Support;

namespace Recodme.Academy.RestaurantApp.WebApplication.Controllers.RestaurantControllers.Web.MenuControllers
{
    

    [Route("[controller]")]
    public class DishesController : Controller
    {
        private readonly DishBusinessObject _bo = new DishBusinessObject();
        private readonly DietaryRestrictionBusinessObject _dro = new DietaryRestrictionBusinessObject();


        private string GetDeleteRef()
        {
            return this.ControllerContext.RouteData.Values["controller"] + "/" + nameof(Delete);
        }

        private List<BreadCrumb> GetCrumbs()
        {
            return new List<BreadCrumb>()
                { new BreadCrumb(){Icon ="fa-home", Action="Index", Controller="Home", Text="Home"},
                  new BreadCrumb(){Icon = "fa-user-cog", Action="Administration", Controller="Home", Text = "Administration"},
                  new BreadCrumb(){Icon = "fa-shish-kebab", Action="Index", Controller="Dishes", Text = "Dishes"}
                };
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

        private IActionResult OperationSuccess(string message)
        {
            TempData["Alert"] = AlertFactory.GenerateAlert(NotificationType.Success, message);
            return RedirectToAction(nameof(Index));
        }


        private async Task<List<DietaryRestrictionViewModel>> GetDietaryRestrictionViewModels(List<Guid> ids)
        {
            var filterOperation = await _dro.FilterAsync(x => ids.Contains(x.Id));
            var drList = new List<DietaryRestrictionViewModel>();
            foreach (var item in filterOperation.Result)
            {
                drList.Add(DietaryRestrictionViewModel.Parse(item));
            }
            return drList;
        }

        private async Task<DietaryRestrictionViewModel> GetDietaryRestrictionViewModel(Guid id)
        {
            var getOperation = await _dro.ReadAsync(id);
            return DietaryRestrictionViewModel.Parse(getOperation.Result);
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listOperation = await _bo.ListNonDeletedAsync();
            if (!listOperation.Success) return OperationErrorBackToIndex(listOperation.Exception);

            var lst = new List<DishViewModel>();
            foreach (var item in listOperation.Result)
            {
                lst.Add(DishViewModel.Parse(item));
            }

            var drList = await GetDietaryRestrictionViewModels(listOperation.Result.Select(x => x.DietaryRestrictionId).Distinct().ToList());
            ViewData["DietaryRestrictions"] = drList;
            ViewData["Title"] = "Dishes";
            ViewData["BreadCrumbs"] = GetCrumbs();
            ViewData["DeleteHref"] = GetDeleteRef();

            return View(lst);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return RecordNotFound();
            var getOperation = await _bo.ReadAsync((Guid)id);

            if (!getOperation.Success) return OperationErrorBackToIndex(getOperation.Exception);
            if (getOperation.Result == null) return RecordNotFound();

            var getDrOperation = await _dro.ReadAsync(getOperation.Result.DietaryRestrictionId);
            if (!getDrOperation.Success) return OperationErrorBackToIndex(getDrOperation.Exception);
            if (getDrOperation.Result == null) return RecordNotFound();

            var vm = DishViewModel.Parse(getOperation.Result);
            ViewData["Title"] = "Dish";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "Details", Controller = "Dishes", Icon = "fa-search", Text = "Detail" });
            ViewData["DietaryRestriction"] = DietaryRestrictionViewModel.Parse(getDrOperation.Result);
            ViewData["BreadCrumbs"] = crumbs;
            return View(vm);
        }

        [HttpGet("new")]
        public async Task<IActionResult> New()
        {
            var listDrOperation = await _dro.ListNonDeletedAsync();
            if (!listDrOperation.Success) return OperationErrorBackToIndex(listDrOperation.Exception);

            var drList = new List<SelectListItem>();
            foreach (var item in listDrOperation.Result)
            {
                drList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Name });
            }
            ViewBag.DietaryRestrictions = drList;
            ViewData["Title"] = "New Dish";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "New", Controller = "Dishes", Icon = "fa-plus", Text = "New" });
            ViewData["BreadCrumbs"] = crumbs;
            return View();
        }


        [HttpPost("new")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(DishViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var model = vm.ToModel();
                var createOperation = await _bo.CreateAsync(model);
                if (!createOperation.Success) return OperationErrorBackToIndex(createOperation.Exception);
                else return OperationSuccess("The record was successfuly created");
            }
            return View(vm);
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return RecordNotFound();

            var getOperation = await _bo.ReadAsync((Guid)id);
            if (!getOperation.Success) return OperationErrorBackToIndex(getOperation.Exception);
            if (getOperation.Result == null) return RecordNotFound();

            var vm = DishViewModel.Parse(getOperation.Result);
            var listDrOperation = await _dro.ListNonDeletedAsync();
            if (!listDrOperation.Success) return OperationErrorBackToIndex(listDrOperation.Exception);

            var drList = new List<SelectListItem>();
            foreach (var item in listDrOperation.Result)
            {
                var listItem = new SelectListItem() { Value = item.Id.ToString(), Text = item.Name };
                if (item.Id == vm.DietaryRestrictionId) listItem.Selected = true;
                drList.Add(listItem);
            }
            ViewBag.DietaryRestrictions = drList;
            ViewData["Title"] = "Edit Course";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "Edit", Controller = "Dishes", Icon = "fa-edit", Text = "Edit" });
            ViewData["BreadCrumbs"] = crumbs;
            return View(vm);
        }


        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, DishViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var getOperation = await _bo.ReadAsync(id);
                if (!getOperation.Success) return OperationErrorBackToIndex(getOperation.Exception);
                if (getOperation.Result == null) return RecordNotFound();
                var result = getOperation.Result;
                if (!vm.CompareToModel(result))
                {
                    result = vm.ToModel(result);
                    var updateOperation = await _bo.UpdateAsync(result);
                    if (!updateOperation.Success)
                    {
                        TempData["Alert"] = AlertFactory.GenerateAlert(NotificationType.Danger, updateOperation.Exception);
                        return View(vm);
                    }
                    else return OperationSuccess("The record was successfuly updated");
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return RecordNotFound();
            var deleteOperation = await _bo.DeleteAsync((Guid)id);
            if(!deleteOperation.Success) return OperationErrorBackToIndex(deleteOperation.Exception);
            else return OperationSuccess("The record was successfuly deleted");
        }
    }
}
