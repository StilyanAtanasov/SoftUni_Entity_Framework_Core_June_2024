using Blog.Core.Entities;
using Blog.Core.Interfaces.Repository;
using Blog.Core.Interfaces.Services;
using Blog.Core.Services;
using Blog.Data;
using Blog.Data.Common;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static Blog.Common.CookieConstraints;

namespace Blog.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services
                .AddDbContext<BlogDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddControllersWithViews();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/ApplicationUser/Login";
                    options.LogoutPath = "/Home/Index";
                    options.ExpireTimeSpan = TimeSpan.FromDays(AuthCookieDefaultExpireDays);
                    options.SlidingExpiration = true;
                    options.Cookie.HttpOnly = true;
                });

            builder.Services
                .AddScoped<IRepository, EfRepository>()
                .AddScoped<IApplicationUserService, ApplicationUserService>()
                .AddScoped<IArticleService, ArticleService>()
                .AddScoped<IPasswordHasher<ApplicationUser>, PasswordHasher<ApplicationUser>>();

            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}