using HomeTaskkMVC4.DAL;
using HomeTaskkMVC4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HomeTaskkMVC4
{
    public  static class RegisterService
    {
        public static void Register(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllersWithViews();
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            }
            );
         
            services.AddSession(option =>
            {
                option.IdleTimeout=TimeSpan.FromMinutes(1);
            });

            services.AddIdentity<AppUser, IdentityRole>(IdentityOptions =>
            {
                IdentityOptions.Password.RequireNonAlphanumeric = true;
                IdentityOptions.Password.RequiredLength = 8;
                IdentityOptions.Password.RequireDigit = true;
                IdentityOptions.Password.RequireLowercase = true;
                IdentityOptions.Password.RequireUppercase = true;

                IdentityOptions.User.RequireUniqueEmail = true;

                IdentityOptions.Lockout.MaxFailedAccessAttempts = 3;
                IdentityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                IdentityOptions.Lockout.AllowedForNewUsers = true;

            })
                .AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();

        }
    }
}
