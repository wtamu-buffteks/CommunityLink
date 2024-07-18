using CommunityLink.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<CommunityLinkDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("CommunityLinkContext"))
);

builder.Services.AddHttpContextAccessor();

builder.Services.AddDistributedMemoryCache(); //enables the cache

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10); //if the user does nothing for 10 minutes
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add Identity services with role support
// RequireConfirmedAccount = false gets rid of redundancy that asks user to confirm account
builder
    .Services.AddDefaultIdentity<CommunityUser>(options =>
        options.SignIn.RequireConfirmedAccount = false
    )
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<CommunityLinkDbContext>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<CommunityUser>>();

    await SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapRazorPages();

app.Run();
