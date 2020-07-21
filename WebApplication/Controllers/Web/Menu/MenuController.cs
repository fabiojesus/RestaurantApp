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

namespace Recodme.Academy.RestaurantApp.WebApplication.Controllers.RestaurantControllers.Web.MenuControllers
{
    

    [Route("[controller]")]
    public class MenusController : Controller
    {
        private readonly MenuBusinessObject _bo = new MenuBusinessObject();
        private readonly MealBusinessObject _mbo = new MealBusinessObject();
        private readonly RestaurantBusinessObject _rbo = new RestaurantBusinessObject();


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
            if (!listOperation.Success) return View("Error", new ErrorViewModel() { RequestId = listOperation.Exception.Message });
            var rIds = listOperation.Result.Select(x => x.RestaurantId).Distinct().ToList();
            var mIds = listOperation.Result.Select(x => x.MealId).Distinct().ToList();
            var lst = new List<MenuViewModel>();
            foreach (var item in listOperation.Result)
            {
                lst.Add(MenuViewModel.Parse(item));
            }
            ViewData["Title"] = "Menus";
            ViewData["BreadCrumbs"] = new List<string>() { "Home", "Menus" };
            ViewData["Meals"] = await GetMealViewModels(mIds);
            ViewData["Restaurants"] = await GetRestaurantViewModels(rIds);
            return View(lst);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var getOperation = await _bo.ReadAsync((Guid)id);
            if (!getOperation.Success) return View("Error", getOperation.Exception.Message);
            if (getOperation.Result == null) return NotFound();
            var vm = MenuViewModel.Parse(getOperation.Result);
            ViewData["Header"] = "Menu";
            return View(vm);
        }

        [HttpGet("/new")]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MenuViewModel vm)
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
            var vm = MenuViewModel.Parse(getOperation.Result);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, MenuViewModel vm)
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
