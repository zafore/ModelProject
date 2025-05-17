using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Data.Models;
using Services.Data.Repository;
using Services.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Localization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ModelDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Modelcon")));

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<ModelDBContext>()
    .AddDefaultTokenProviders();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
	.AddCookie(options =>
	{
		options.LoginPath = new PathString("/Account/login");
		options.AccessDeniedPath = new PathString("/Account/AccessDenied");
		options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
	
	});

builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Run();
