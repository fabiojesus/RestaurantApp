using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Recodme.Academy.RestaurantApp.BusinessLayer.OperationResults;
using Recodme.Academy.RestaurantApp.DataAccessLayer.Contexts;
using Recodme.Academy.RestaurantApp.DataLayer.UserRecords;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Recodme.Academy.RestaurantApp.BusinessLayer.BusinessObjects.UserBusinessObjects
{
    public class AccountBusinessController
    {
        private UserManager<RestaurantUser> UserManager { get; set; }
        private RoleManager<RestaurantRole> RoleManager { get; set; }
        private readonly PersonBusinessObject _pbo = new PersonBusinessObject();

        public AccountBusinessController(UserManager<RestaurantUser> uManager, RoleManager<RestaurantRole> rManager)
        {
            UserManager = uManager;
            RoleManager = rManager;
        }


        TransactionOptions transactionOptions = new TransactionOptions
        {
            IsolationLevel = IsolationLevel.ReadCommitted,
            Timeout = TimeSpan.FromSeconds(30)
        };

        public async Task<OperationResult> Register(string userName, string email, string password, Person profile, string role)
        {
            if (await UserManager.FindByEmailAsync(email) != null)
                return new OperationResult() { Success = false, Message = $"User {email} already exists" };
            if (await UserManager.FindByNameAsync(userName) != null)
                return new OperationResult() { Success = false, Message = $"User {userName} already exists" };
            using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                var createPersonOperation = await _pbo.CreateAsync(profile);
                if (!createPersonOperation.Success)
                {
                    transactionScope.Dispose();
                    return createPersonOperation;
                }
                var user = new RestaurantUser()
                {
                    Email = email,
                    UserName = userName,
                    PersonId = profile.Id
                };
                var result = await UserManager.CreateAsync(user, password);
                if (!result.Succeeded)
                {
                    transactionScope.Dispose();
                    return new OperationResult() { Success = false, Message = result.ToString() };
                }
                var roleData = await RoleManager.FindByNameAsync(role);
                if(roleData == null || roleData.Name == "Admin")
                {
                    transactionScope.Dispose();
                    return new OperationResult() { Success = false, Message = $"Role {role} does not exist" };
                }
                var roleOpt = await UserManager.AddToRoleAsync(user, role);
                if (!roleOpt.Succeeded)
                {
                    transactionScope.Dispose();
                    return new OperationResult() { Success = false, Message = roleOpt.ToString() };
                }
                transactionScope.Complete();
                return new OperationResult() { Success = true};
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<Person>> GetProfile(IdentityUser<int> user)
        {
            if(user is RestaurantUser)
            {
                var restUser = (RestaurantUser)user;
                try
                {

                    using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                    var personOperation = await _pbo.ReadAsync(restUser.PersonId);
                    transactionScope.Complete();
                    return personOperation;
                }
                catch (Exception e)
                {
                    return new OperationResult<Person>() { Success = false, Exception = e };
                }
            }
            return new OperationResult<Person>() { Success = false, Message = "The user is not a RestaurantUser" };
        }

        public async Task<OperationResult<bool>> IsClient(Person person)
        {
            var users = await UserManager.GetUsersInRoleAsync("Client");
            var user = users.FirstOrDefault(x => x.PersonId == person.Id);
            if (user == null) return new OperationResult<bool>() { Success = true, Result = false, Message = "User is not a client" };
            else return new OperationResult<bool>() { Success = true, Result = false, Message = "User is a client" };
        }

        public async Task<OperationResult<bool>> IsStaff(Person person)
        {
            var users = await UserManager.GetUsersInRoleAsync("Staff");
            var user = users.FirstOrDefault(x => x.PersonId == person.Id);
            if (user == null) return new OperationResult<bool>() { Success = true, Result = false, Message = "User is not a staff member" };
            else return new OperationResult<bool>() { Success = true, Result = false, Message = "User is a staff member" };
        }

    }
}
