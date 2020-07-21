using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using Recodme.Academy.RestaurantApp.BusinessLayer.BusinessObjects.MenuBusinessObjects;
using Recodme.Academy.RestaurantApp.WebApplication.Models.MenuViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.Academy.RestaurantApp.WebApplication.Controllers.RestaurantControllers.Web.MenuControllers
{
    

    [Route("[controller]")]
    public class ServingsController : Controller
    {
        private readonly ServingBusinessObject _bo = new ServingBusinessObject();
        private readonly MenuBusinessObject _mbo = new MenuBusinessObject();
        private readonly CourseBusinessObject _cbo = new CourseBusinessObject();
        private readonly DishBusinessObject _dbo = new DishBusinessObject();


        private async Task<List<MenuViewModel>> GetMenuViewModels(List<Guid> ids)
        {
            var filterOperation = await _mbo.FilterAsync(x => ids.Contains(x.Id));
            var drList = new List<MenuViewModel>();
            foreach (var item in filterOperation.Result)
            {
                drList.Add(MenuViewModel.Parse(item));
            }
            return drList;
        }

        private async Task<MenuViewModel> GetMenuViewModel(Guid id)
        {
            var getOperation = await _mbo.ReadAsync(id);
            return MenuViewModel.Parse(getOperation.Result);
        }

        private async Task<List<CourseViewModel>> GetCourseViewModels(List<Guid> ids)
        {
            var filterOperation = await _cbo.FilterAsync(x => ids.Contains(x.Id));
            var drList = new List<CourseViewModel>();
            foreach (var item in filterOperation.Result)
            {
                drList.Add(CourseViewModel.Parse(item));
            }
            return drList;
        }

        private async Task<CourseViewModel> GetCourseViewModel(Guid id)
        {
            var getOperation = await _cbo.ReadAsync(id);
            return CourseViewModel.Parse(getOperation.Result);
        }

        private async Task<List<DishViewModel>> GetDishViewModels(List<Guid> ids)
        {
            var filterOperation = await _dbo.FilterAsync(x => ids.Contains(x.Id));
            var drList = new List<DishViewModel>();
            foreach (var item in filterOperation.Result)
            {
                drList.Add(DishViewModel.Parse(item));
            }
            return drList;
        }

        private async Task<DishViewModel> GetDishViewModel(Guid id)
        {
            var getOperation = await _dbo.ReadAsync(id);
            return DishViewModel.Parse(getOperation.Result);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listOperation = await _bo.ListNonDeletedAsync();
            if (!listOperation.Success) return View("Error", new ErrorViewModel() { RequestId = listOperation.Exception.Message });
            var mIds = listOperation.Result.Select(x => x.MenuId).Distinct().ToList();
            var cIds = listOperation.Result.Select(x => x.CourseId).Distinct().ToList();
            var dIds = listOperation.Result.Select(x => x.DishId).Distinct().ToList();


            var lst = new List<ServingViewModel>();
            foreach (var item in listOperation.Result)
            {
                lst.Add(ServingViewModel.Parse(item));
            }
            ViewData["BreadCrumbs"] = new List<string>() { "Home", "Servings" };
            ViewData["Title"] = "Servings";
            ViewData["Menus"] = await GetMenuViewModels(mIds);
            ViewData["Courses"] = await GetCourseViewModels(cIds);
            ViewData["Dishes"] = await GetDishViewModels(dIds);
            return View(lst);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var getOperation = await _bo.ReadAsync((Guid)id);
            if (!getOperation.Success) return View("Error", getOperation.Exception.Message);
            if (getOperation.Result == null) return NotFound();
            var vm = ServingViewModel.Parse(getOperation.Result);
            ViewData["Header"] = "Serving";
            return View(vm);
        }

        [HttpGet("/new")]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServingViewModel vm)
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
            var vm = ServingViewModel.Parse(getOperation.Result);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ServingViewModel vm)
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
