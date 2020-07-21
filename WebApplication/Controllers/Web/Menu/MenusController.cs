using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;
using Recodme.Academy.RestaurantApp.BusinessLayer.BusinessObjects.MenuBusinessObjects;
using Recodme.Academy.RestaurantApp.BusinessLayer.BusinessObjects.RestaurantBusinessObjects;
using Recodme.Academy.RestaurantApp.WebApplication.Models.MenuViewModels;
using Recodme.Academy.RestaurantApp.WebApplication.Models.RestaurantViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models.HtmlComponents;
using WebApplication.Support;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Recodme.Academy.RestaurantApp.WebApplication.Controllers.RestaurantControllers.Web.MenuControllers
{
    

    [Route("[controller]")]
    public class MenusController : Controller
    {
        private readonly MenuBusinessObject _bo = new MenuBusinessObject();
        private readonly MealBusinessObject _mbo = new MealBusinessObject();
        private readonly RestaurantBusinessObject _rbo = new RestaurantBusinessObject();

        private string GetDeleteRef()
        {
            return this.ControllerContext.RouteData.Values["controller"] + "/" + nameof(Delete);
        }

        private List<BreadCrumb> GetCrumbs()
        {
            return new List<BreadCrumb>()
                { new BreadCrumb(){Icon ="fa-home", Action="Index", Controller="Home", Text="Home"},
                  new BreadCrumb(){Icon = "fa-user-cog", Action="Administration", Controller="Home", Text = "Administration"},
                  new BreadCrumb(){Icon = "fa-hat-chef", Action="Index", Controller="Meals", Text = "Meals"}
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


        private async Task<List<MealViewModel>> GetMealViewModels(List<Guid> ids)
        {
            var filterOperation = await _mbo.FilterAsync(x => ids.Contains(x.Id));
            var drList = new List<MealViewModel>();
            foreach (var item in filterOperation.Result)
            {
                drList.Add(MealViewModel.Parse(item));
            }
            return drList;
        }

        private async Task<MealViewModel> GetMealViewModel(Guid id)
        {
            var getOperation = await _mbo.ReadAsync(id);
            return MealViewModel.Parse(getOperation.Result);
        }

        private async Task<List<RestaurantViewModel>> GetRestaurantViewModels(List<Guid> ids)
        {
            var filterOperation = await _rbo.FilterAsync(x => ids.Contains(x.Id));
            var drList = new List<RestaurantViewModel>();
            foreach (var item in filterOperation.Result)
            {
                drList.Add(RestaurantViewModel.Parse(item));
            }
            return drList;
        }

        private async Task<RestaurantViewModel> GetRestaurantViewModel(Guid id)
        {
            var getOperation = await _rbo.ReadAsync(id);
            return RestaurantViewModel.Parse(getOperation.Result);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listOperation = await _bo.ListNonDeletedAsync();
            if (!listOperation.Success) return OperationErrorBackToIndex(listOperation.Exception);
            var rIds = listOperation.Result.Select(x => x.RestaurantId).Distinct().ToList();
            var mIds = listOperation.Result.Select(x => x.MealId).Distinct().ToList();
            var lst = new List<MenuViewModel>();
            foreach (var item in listOperation.Result)
            {
                lst.Add(MenuViewModel.Parse(item));
            }

            ViewData["Title"] = "Meals";
            ViewData["BreadCrumbs"] = GetCrumbs();
            ViewData["DeleteHref"] = GetDeleteRef();
            ViewData["Meals"] = await GetMealViewModels(mIds);
            ViewData["Restaurants"] = await GetRestaurantViewModels(rIds);

            return View(lst);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return RecordNotFound();
            var getOperation = await _bo.ReadAsync((Guid)id);

            if (!getOperation.Success) return OperationErrorBackToIndex(getOperation.Exception);
            if (getOperation.Result == null) return RecordNotFound();

            var getRestOperation = await _rbo.ReadAsync(getOperation.Result.RestaurantId);
            if (!getRestOperation.Success) return OperationErrorBackToIndex(getRestOperation.Exception);
            if (getRestOperation.Result == null) return RecordNotFound();

            var getMealOperation = await _mbo.ReadAsync(getOperation.Result.MealId);
            if (!getMealOperation.Success) return OperationErrorBackToIndex(getMealOperation.Exception);
            if (getMealOperation.Result == null) return RecordNotFound();

            var vm = MenuViewModel.Parse(getOperation.Result);
            ViewData["Title"] = "Dish";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "Details", Controller = "Menus", Icon = "fa-search", Text = "Detail" });
            ViewData["Restaurant"] = RestaurantViewModel.Parse(getRestOperation.Result);
            ViewData["Meal"] = MealViewModel.Parse(getMealOperation.Result);
            ViewData["BreadCrumbs"] = crumbs;
            return View(vm);
        }

        [HttpGet("new")]
        public async Task<IActionResult> New()
        {
            var listMealOperation = await _mbo.ListNonDeletedAsync();
            if (!listMealOperation.Success) return OperationErrorBackToIndex(listMealOperation.Exception);

            var listRestOperation = await _rbo.ListNonDeletedAsync();
            if (!listRestOperation.Success) return OperationErrorBackToIndex(listRestOperation.Exception);

            var mealList = new List<SelectListItem>();
            foreach (var item in listMealOperation.Result)
            {
                mealList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Name });
            }

            var restList = new List<SelectListItem>();
            foreach (var item in listRestOperation.Result)
            {
                restList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Name });
            }

            ViewBag.Meals = mealList;
            ViewBag.Restaurants = restList;
            ViewData["Title"] = "New Menu";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "New", Controller = "Menus", Icon = "fa-plus", Text = "New" });
            ViewData["BreadCrumbs"] = crumbs;
            return View();
        }

        [HttpPost("new")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MenuViewModel vm)
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

            var vm = MenuViewModel.Parse(getOperation.Result);
            
            var listRestOperation = await _rbo.ListNonDeletedAsync();
            if (!listRestOperation.Success) return OperationErrorBackToIndex(listRestOperation.Exception);

            var listMealOperation = await _mbo.ListNonDeletedAsync();
            if (!listMealOperation.Success) return OperationErrorBackToIndex(listMealOperation.Exception);

            var mealList = new List<SelectListItem>();
            foreach (var item in listMealOperation.Result)
            {
                mealList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Name });
            }

            var restList = new List<SelectListItem>();
            foreach (var item in listRestOperation.Result)
            {
                restList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Name });
            }

            ViewBag.Meals = mealList;
            ViewBag.Restaurants = restList;

            ViewData["Title"] = "Edit Menu";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "Edit", Controller = "Menus", Icon = "fa-edit", Text = "Edit" });
            ViewData["BreadCrumbs"] = crumbs;
            return View(vm);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, MenuViewModel vm)
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
            if (!deleteOperation.Success) return OperationErrorBackToIndex(deleteOperation.Exception);
            else return OperationSuccess("The record was successfuly deleted");
        }
    }
}
