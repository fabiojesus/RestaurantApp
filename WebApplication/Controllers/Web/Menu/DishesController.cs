using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using Recodme.Academy.RestaurantApp.BusinessLayer.BusinessObjects.MenuBusinessObjects;
using Recodme.Academy.RestaurantApp.DataLayer.MenuRecords;
using Recodme.Academy.RestaurantApp.WebApplication.Models.MenuViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.Academy.RestaurantApp.WebApplication.Controllers.RestaurantControllers.Web.MenuControllers
{
    

    [Route("[controller]")]
    public class DishesController : Controller
    {
        private readonly DishBusinessObject _bo = new DishBusinessObject();
        private readonly DietaryRestrictionBusinessObject _dro = new DietaryRestrictionBusinessObject();


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
            if (!listOperation.Success) return View("Error", new ErrorViewModel() { RequestId = listOperation.Exception.Message });
            var lst = new List<DishViewModel>();
            foreach (var item in listOperation.Result)
            {
                lst.Add(DishViewModel.Parse(item));
            }
            var drList = await GetDietaryRestrictionViewModels(listOperation.Result.Select(x => x.DietaryRestrictionId).Distinct().ToList());
             ViewData["Title"] = "Dishes";
            ViewData["BreadCrumbs"] = new List<string>() { "Home", "Dishes" };
            ViewData["DietaryRestrictions"] = drList;
            return View(lst);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var getOperation = await _bo.ReadAsync((Guid)id);
            if (!getOperation.Success) return View("Error", getOperation.Exception.Message);
            if (getOperation.Result == null) return NotFound();
            var vm = DishViewModel.Parse(getOperation.Result);
            ViewData["Header"] = "Dish";
            return View(vm);
        }

        [HttpGet("/new")]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DishViewModel vm)
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
            var vm = DishViewModel.Parse(getOperation.Result);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, DishViewModel vm)
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
