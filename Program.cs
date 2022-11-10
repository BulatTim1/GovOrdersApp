using AspNetCore.Identity.Mongo;
using GovOrdersApp.Areas.Identity;
using GovOrdersApp.Data;
using GovOrdersApp.Data.DB;
using GovOrdersApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

BsonClassMap.RegisterClassMap<AppUser>();
BsonClassMap.RegisterClassMap<CustomerRole>();
BsonClassMap.RegisterClassMap<AdminRole>();
BsonClassMap.RegisterClassMap<BuilderRole>();
BsonClassMap.RegisterClassMap<DesignerRole>();
BsonClassMap.RegisterClassMap<OrderDocument>();
BsonClassMap.RegisterClassMap<Comment>();
BsonClassMap.RegisterClassMap<Order>();

new UsersContext().AddUser(new AdminRole()
{
    Email = "admin@dot.net",
    Password = "admin",
    FullName = "Admin",
    Login = "admin",
    Phone = "1234567890",
});
new UsersContext().AddUser(new CustomerRole()
{
    Email = "customer@test",
    Password = "customer",
    FullName = "TestCustomer",
    Login = "testcustomer",
    Phone = "",
});

app.Run();
