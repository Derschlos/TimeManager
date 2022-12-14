using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TimeManagerMVC.Areas.Identity.Data;
using TimeManagerMVC.Data;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("TimeManagerUserContextConnection") ?? throw new InvalidOperationException("Connection string 'TimeManagerUserContextConnection' not found.");

builder.Services.AddDbContext<TimeManagerUserContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<TimeManagerUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<TimeManagerUserContext>();

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
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
