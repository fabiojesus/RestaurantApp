using System;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Recodme.Academy.RestaurantApp.BusinessLayer.BusinessObjects.UserBusinessObjects;
using Recodme.Academy.RestaurantApp.DataAccessLayer.Contexts;
using Recodme.Academy.RestaurantApp.DataLayer.UserRecords;

namespace WebApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddSession();
            services.AddMemoryCache();
            //Politicas de cookies
            services.Configure<CookiePolicyOptions>(
                options =>
                {
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });

            //Definição da identidade
            services.AddIdentity<RestaurantUser, RestaurantRole>(
                options =>
                {
                    options.User.RequireUniqueEmail = true;
                }).AddEntityFrameworkStores<RestaurantContext>();

            //Definição da base de dados para autenticação e autorização
            services.AddDbContext<RestaurantContext>(
                options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("Default"));
                });

            //Swagger Generator
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo() { Title = "My API", Version = "v1" });
            });

            //CORS irresponsável
            services.AddCors(x => x.AddPolicy("Default", (builder) => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

            //Adicionar mais umas pastas de Views
            services.Configure<RazorViewEngineOptions>(o =>
            {
                o.ViewLocationFormats.Clear();
                o.ViewLocationFormats.Add("/Views/{1}/{0}" + RazorViewEngine.ViewExtension);
                o.ViewLocationFormats.Add("/Views/Shared/{0}" + RazorViewEngine.ViewExtension);
                o.ViewLocationFormats.Add("/Views/MenuViews/{1}/{0}" + RazorViewEngine.ViewExtension);
                o.ViewLocationFormats.Add("/Views/RestaurantViews/{1}/{0}" + RazorViewEngine.ViewExtension);
                o.ViewLocationFormats.Add("/Views/UserViews/{1}/{0}" + RazorViewEngine.ViewExtension);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<RestaurantUser> userManager, RoleManager<RestaurantRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            SetupRolesAndUsers(userManager, roleManager);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "api",
                    pattern: "api/{controller=Home}/{action=Index}/{id?}");
            });
        }

        public  void SetupRolesAndUsers(UserManager<RestaurantUser> userManager, RoleManager<RestaurantRole> roleManager)
        {
            if (roleManager.FindByNameAsync("Client").Result == null) roleManager.CreateAsync(new RestaurantRole() { Name = "Client" }).Wait();
            if (roleManager.FindByNameAsync("Staff").Result == null) roleManager.CreateAsync(new RestaurantRole() { Name = "Staff" }).Wait();
            if (roleManager.FindByNameAsync("Admin").Result == null) roleManager.CreateAsync(new RestaurantRole() { Name = "Admin" }).Wait();
            if(userManager.FindByNameAsync("admin").Result == null)
            {
                var person = new Person(DateTime.Now, "Administrator", "", 0000000, 0);
                var abo = new AccountBusinessController(userManager, roleManager);
                var res = abo.Register("admin", "admin@restLen.com", "Admin123!#", person, "Admin").Result;
                var roleRes = userManager.AddToRoleAsync(userManager.FindByNameAsync("admin").Result, "Admin");
            }
            
        }
    }
}
