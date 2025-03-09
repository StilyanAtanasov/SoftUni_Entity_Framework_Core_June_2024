using Eventmi.Core;
using Eventmi.Core.Contracts;
using Eventmi.Data;
using Eventmi.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Eventmi.Web;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services
            .AddDbContext<EventmiDbContext>(c =>
                c.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL")));

        builder.Services
            .AddScoped<IRepository, EfRepository>()
            .AddScoped<IEventService, EventService>()
            .AddScoped<ITownService, TownService>();

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<EventmiDbContext>();
        await db.Database.MigrateAsync();

        await app.RunAsync();
    }
}