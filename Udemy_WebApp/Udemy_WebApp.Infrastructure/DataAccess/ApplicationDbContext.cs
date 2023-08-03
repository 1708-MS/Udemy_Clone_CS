using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection;
using Udemy_WebApp.Domain.Models;
using Udemy_WebApp.Infrastructure.Identity;

namespace Udemy_WebApp.Infrastructure.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder ModelBuilder)
        {
            base.OnModelCreating(ModelBuilder);
            ModelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseCategory> CourseCategories { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<CourseVideo> CourseVideos { get; set; }
        //public DbSet<CourseImage> CourseImages { get; set; }
        public DbSet<CourseReview> CourseReviews { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
