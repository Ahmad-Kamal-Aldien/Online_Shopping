using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shopping.DataAccessLayer.Data;
using Shopping.DataAccessLayer.Repositorys.IRepository;
using Shopping.DataAccessLayer.Repositorys.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Shopping.Utilities;
using Stripe;
using Shopping.Utilities.StripeInfo;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<ApplicationDBContext>(
op => op.UseSqlServer(builder.Configuration.GetConnectionString("conn")));



//To Make Stripe

builder.Services.Configure<StripeInfo>((builder.Configuration.GetSection("Stripe:SecretKey")));



//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDBContext>();

builder.Services.AddIdentity<IdentityUser,IdentityRole>
    (p=>p.Lockout.DefaultLockoutTimeSpan=TimeSpan.FromMinutes(5))
    .AddDefaultTokenProviders().AddDefaultUI().AddEntityFrameworkStores<ApplicationDBContext>();


builder.Services.AddSingleton<IEmailSender,EmailSender>();
builder.Services.AddRazorPages();


builder.Services.AddMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



////To Make Stripe
StripeConfiguration.ApiKey = builder.Configuration.GetSection("SecretKey").Get<string>();




app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.MapRazorPages();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
//pattern: "{controller=Home}/{action=Index}/{id?}");

//pattern: "{area=Admin}/{controller=product}/{action=add}/{id?}");
//pattern: "{area=Admin}/{controller=Category}/{action=index}/{id?}");

pattern: "{area=Customer}/{controller=Home}/{action=index}/{id?}");














app.Run();
