using System;
using Bibliopole.DataAccess.Repository.IRepository;
using Bibliopole.Models;
using Bibliopole.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace Bibliopole.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public DbInitializer(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
        }


        public void Initialize()
        {
            //Migrations if not applied
            try
            {
                if(_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {

            }
            // create role if not created

            if (!(_roleManager.RoleExistsAsync(staticDetails.Role_Admin).GetAwaiter().GetResult()))
            {
                _roleManager.CreateAsync(new IdentityRole(staticDetails.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(staticDetails.Role_Employee)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(staticDetails.Role_User_Indi)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(staticDetails.Role_User_Comp)).GetAwaiter().GetResult();

                // we will create admin if roles not created

                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "admin@bibliopole.com",
                    Email = "admin@bibliopole.com",
                    Name = "Admin",
                    PhoneNumber = "1234567890",
                    Street = "Admin St.",
                    State = "Admin",
                    PostalCode = "123456",
                    City = "Admin"
                }, "Admin@123").GetAwaiter().GetResult();

                ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@bibliopole.com");

                _userManager.AddToRoleAsync(user, staticDetails.Role_Admin).GetAwaiter().GetResult();
            }
            return;
        }
    }
}

