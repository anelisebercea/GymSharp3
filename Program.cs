using GymSharp.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using GymSharp.Hubs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
//using GymSharp.GymModel.Data;
using GymSharp.Areas.Identity;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<GymContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<IdentityContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<IdentityContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    //options.SignIn.RequireConfirmedAccount = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.AllowedForNewUsers = false;

    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 1;
});
    //.AddDefaultTokenProviders();


builder.Services.AddAuthorization(opts => {
    opts.AddPolicy("OnlyCertified", policy => {
        policy.RequireClaim("Department", "Fitness");
    });
    opts.AddPolicy("GymManager", policy => {
        policy.RequireRole("Manager");
        policy.RequireClaim("Department", "Administration");
    });
}); /// poate sa faca CRUD  users
/*
builder.Services.AddAuthorization(opts => {
    opts.AddPolicy("TrainingEmployee", policy => {
        policy.RequireRole("Employee");
        policy.RequireClaim("Department", "Fitness");
    });
});
*/
builder.Services.ConfigureApplicationCookie(opts =>
{
    opts.AccessDeniedPath = "/Identity/Account/AccessDenied";
});


builder.Services.AddSignalR();
builder.Services.AddRazorPages();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    DbInitializer.Initialize(services);
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<ChatHub>("/Chat");
app.MapRazorPages();

app.Run();
