using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;
using Recodme.Academy.RestaurantApp.BusinessLayer.BusinessObjects.UserBusinessObjects;
using Recodme.Academy.RestaurantApp.DataLayer.UserRecords;
using Recodme.Academy.RestaurantApp.WebApplication.Models.UserViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using WebApplication.Support;
using WebApplication.Models.HtmlComponents;
using Microsoft.AspNetCore.Authorization;

namespace Recodme.Academy.RestaurantApp.WebApplication.Controllers
{
    [Authorize]
    public class AccountsController : Controller
    {
        private UserManager<RestaurantUser> UserManager { get; set; }
        private SignInManager<RestaurantUser> SignInManager { get; set; }
        private RoleManager<RestaurantRole> RoleManager { get; set; }

        private IActionResult OperationErrorBackToIndex(Exception exception)
        {
            TempData["Alert"] = AlertFactory.GenerateAlert(NotificationType.Danger, exception);
            return RedirectToAction(nameof(Index), "Home");
        }

        private IActionResult OperationSuccess(string message)
        {
            TempData["Alert"] = AlertFactory.GenerateAlert(NotificationType.Success, message);
            return RedirectToAction(nameof(Index), "Home");
        }

        public AccountsController(UserManager<RestaurantUser> uManager, SignInManager<RestaurantUser> sManager, RoleManager<RestaurantRole> rManager)
        {
            UserManager = uManager;
            SignInManager = sManager;
            RoleManager = rManager; 
        }

        [AllowAnonymous]
        [HttpPost("/GenerateToken")]
        public IActionResult GenerateToken(LoginViewModel vm)
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            var accountBo = new AccountBusinessController(UserManager, RoleManager);
            var person = new Person(vm.BirthDate, vm.FirstName, vm.LastName, vm.VatNumber, vm.PhoneNumber);
            var registerOperation = await accountBo.Register(vm.UserName, vm.Email, vm.Password, person, vm.Role);
            if (registerOperation.Success)
                return OperationSuccess("The account was successfuly registered!");
            TempData["Alert"] = AlertFactory.GenerateAlert(NotificationType.Danger, registerOperation.Message);
            return View(vm);
        }


        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            var loginOperation = await SignInManager.PasswordSignInAsync(vm.UserName, vm.Password, false, false);
            if (loginOperation.Succeeded) return OperationSuccess("Welcome User");
            else 
            {
                TempData["Alert"] = AlertFactory.GenerateAlert(NotificationType.Danger, loginOperation.ToString());
                return View(vm);
            }
        }


        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
