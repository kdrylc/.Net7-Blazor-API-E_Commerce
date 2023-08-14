using E_Commerce_Common;
using E_Commerce_DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Server.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        public DbInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }
        public void Initializer()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Count() > 0)
                {
                    _context.Database.Migrate();
                }
                if (!_roleManager.RoleExistsAsync(Keys.Role_ForAdmin).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(Keys.Role_ForAdmin)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new IdentityRole(Keys.Role_ForCustomer)).GetAwaiter().GetResult();
                }
                else
                {
                    return;
                }
                IdentityUser user = new()
                {
                    UserName = "admin",
                    Email = "admin@admin.com",
                    EmailConfirmed = true
                };
                _userManager.CreateAsync(user, "Admin123*").GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(user, "Admin").GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
