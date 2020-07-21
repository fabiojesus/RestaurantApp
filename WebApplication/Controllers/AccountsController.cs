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

namespace Recodme.Academy.RestaurantApp.WebApplication.Controllers
{
    public class AccountsController : Controller
    {
        private UserManager<RestaurantUser> UserManager { get; set; }
        private SignInManager<RestaurantUser> SignInManager { get; set; }
        private RoleManager<RestaurantRole> RoleManager { get; set; }
        private readonly PersonBusinessObject _bo = new PersonBusinessObject();

        public AccountsController(UserManager<RestaurantUser> uManager, SignInManager<RestaurantUser> sManager, RoleManager<RestaurantRole> rManager)
        {
            UserManager = uManager;
            SignInManager = sManager;
            RoleManager = rManager; 
        }

        [HttpPost("/GenerateToken")]
        public IActionResult GenerateToken(LoginViewModel vm)
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            try
            {
                var user = await UserManager.FindByNameAsync(vm.UserName);
                if(user == null)
                {
                    var person = new Person(vm.BirthDate, vm.FirstName, vm.LastName, vm.VatNumber, vm.PhoneNumber);
                    var personOperation = await _bo.CreateAsync(person);
                    if(!personOperation.Success) return View("Error", new ErrorViewModel() { RequestId = personOperation.Exception.Message});
                    
                    user = new RestaurantUser();
                    user.UserName = vm.UserName;
                    user.Email = vm.Email;
                    user.PersonId = person.Id;
                    var result = await UserManager.CreateAsync(user, vm.Password);
                    
                    var roleSetResult = await UserManager.AddToRoleAsync(user, vm.Role);
                }
                return RedirectToAction("Index", "Home");
            }
            catch(Exception e)
            {
                return View("Error", new ErrorViewModel() { RequestId = e.Message });
            }
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            var loginOperation = await SignInManager.PasswordSignInAsync(vm.UserName, vm.Password, false, false);
            if (loginOperation.Succeeded) return RedirectToAction("Index", "Home");
            else return View("Error", new ErrorViewModel() { RequestId = loginOperation.ToString() });
        }


        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
