using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using TimeManagerPortalReact.Data;
using TimeManagerPortalReact.Interfaces;
using TimeManagerPortalReact.Models;
using TimeManagerPortalReact.Reporitories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("TimeManagerUserContextConnection")
    ?? throw new InvalidOperationException("Connection string 'TimeManagerUserContextConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).
    AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentityServer()
    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

builder.Services.AddAuthentication()
    .AddIdentityServerJwt();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

AddScoped();
AddAuthorisationPolicies(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseIdentityServer();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");
app.MapRazorPages();

app.MapFallbackToFile("index.html");

app.Run();


void AddScoped()
{
    // builder.Services.AddScoped<ILogApiRepository, LogApiRepository>();
    // builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IRoleRepository, RoleRepository>();
    // builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
}

void AddAuthorisationPolicies(IServiceCollection services)
{
    //services.AddAuthorization(options =>
    //{
    //    options.AddPolicy("EmployeeOnly", policy => policy.RequireClaim("EmployeeNumber"));
    //});
    services.AddAuthorization(options =>
    {
        options.AddPolicy($"{BaseConstants.Policies.RequireManager}", policy => policy.RequireRole($"{BaseConstants.Roles.Manager}"));
        options.AddPolicy($"Admin", policy => policy.RequireClaim("Admin", "Admin"));
        options.AddPolicy($"{BaseConstants.Policies.RequireAdmin}", policy => policy.RequireRole($"{BaseConstants.Roles.Admin}"));
        options.AddPolicy($"{BaseConstants.Policies.RequireUser}", policy => policy.RequireRole($"{BaseConstants.Roles.User}"));
    });
}