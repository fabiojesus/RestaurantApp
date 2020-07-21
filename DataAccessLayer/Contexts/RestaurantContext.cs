using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Recodme.Academy.RestaurantApp.DataAccessLayer.Properties;
using Recodme.Academy.RestaurantApp.DataLayer.MenuRecords;
using Recodme.Academy.RestaurantApp.DataLayer.RestaurantRecords;
using Recodme.Academy.RestaurantApp.DataLayer.UserRecords;

namespace Recodme.Academy.RestaurantApp.DataAccessLayer.Contexts
{
    public class RestaurantContext : IdentityDbContext<RestaurantUser, RestaurantRole, int>
    {
        private string _connectionString = string.Empty;

        public RestaurantContext() : base() { }

        public RestaurantContext(string cString)
        {
            _connectionString = cString;
        }

        public RestaurantContext(DbContextOptions options) : base(options) { }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                if (_connectionString == string.Empty)
                {
                    optionsBuilder.UseSqlServer(Resources.ConnectionString);
                }
                else
                {
                    optionsBuilder.UseSqlServer(_connectionString);
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<DietaryRestriction> DietaryRestrictions { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Serving> Servings { get; set; }
        public DbSet<ClientRecord> ClientRecords { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<StaffRecord> StaffRecords { get; set; }
        public DbSet<StaffTitle> StaffTitles { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}
