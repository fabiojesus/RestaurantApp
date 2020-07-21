﻿using Microsoft.AspNetCore.Mvc;
using Recodme.Academy.RestaurantApp.BusinessLayer.BusinessObjects.MenuBusinessObjects;
using Recodme.Academy.RestaurantApp.WebApplication.Models.MenuViewModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Recodme.Academy.RestaurantApp.WebApplication.Controllers.RestaurantControllers.Api.MenuControllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        private readonly MenuBusinessObject _bo = new MenuBusinessObject();

        [NonAction]
        public IActionResult InternalServerError(Exception exception)
        {
            var result = new ObjectResult(exception.Message)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
            return result;
        }

        [NonAction]
        public IActionResult NotModified()
        {
            var result = new ObjectResult(null)
            {
                StatusCode = (int)HttpStatusCode.NotModified
            };
            return result;
        }

        [HttpGet]
        public async Task<IActionResult> ListAsync()
        {
            var listResult = await _bo.ListNonDeletedAsync();
            if (!listResult.Success) return InternalServerError(listResult.Exception);
            var list = listResult.Result;
            var lst = new List<MenuViewModel>();
            foreach (var item in list)
            {
                var vm = MenuViewModel.Parse(item);
                lst.Add(vm);
            }
            return Ok(lst);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] MenuViewModel vm)
        {
            var newItem = vm.ToModel();
            var result = await _bo.CreateAsync(newItem);
            if (!result.Success) return InternalServerError(result.Exception);
            return Created(Request.Path.Value, null);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var getResult = await _bo.ReadAsync(id);
            if (!getResult.Success) return InternalServerError(getResult.Exception);
            var item = getResult.Result;
            if (item == null) return NotFound();
            var vm = MenuViewModel.Parse(item);
            return Ok(vm);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] MenuViewModel vm)
        {
            var getResult = await _bo.ReadAsync(vm.Id);
            if (!getResult.Success) return InternalServerError(getResult.Exception);
            var item = getResult.Result;
            if (item == null) return NotFound();
            if (vm.CompareToModel(item)) return NotModified();
            item = vm.ToModel(item);
            var updateResult = await _bo.UpdateAsync(item);
            if (!updateResult.Success) return InternalServerError(updateResult.Exception);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var result = await _bo.DeleteAsync(id);
            if (!result.Success) return InternalServerError(result.Exception);
            return Ok();
        }
    }
}
