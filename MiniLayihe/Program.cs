using MiniLayihe.Data;
using Microsoft.SqlServer;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.AspNetCore.Identity;
using System;
using MiniLayihe.Entities;
using MiniLayihe.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<AppUser, IdentityRole>()
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddSingleton<FileService>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("User", policy => policy.RequireRole("User"));
});


// Add services to the container.
builder.Services.AddControllersWithViews();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "User", "Admin" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}
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

app.UseAuthentication();
app.UseAuthorization();

#pragma warning disable ASP0014 // Suggest using top level route registrations
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=AdminHome}/{action=Index}/{id?}"
    );

    // Your other endpoints
});
#pragma warning restore ASP0014 // Suggest using top level route registrations

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "areas",
    pattern: "{area=exists}/{controller=AdminHome}/{action=index}/{id?}");

app.Run();

