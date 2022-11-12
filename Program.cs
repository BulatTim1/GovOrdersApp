using GovOrdersApp.Data.DB;
using GovOrdersApp.Data.Orders;
using GovOrdersApp.Data.Users;
using GovOrdersApp.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddAuthenticationCore();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddSingleton<FileService>();
builder.Services.AddScoped<AuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => 
    provider.GetRequiredService<AuthStateProvider>());
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

DBConnection.users.Indexes.CreateOne(new CreateIndexModel<AppUser>(Builders<AppUser>.IndexKeys.Ascending(x => x.Login), new CreateIndexOptions { Unique = true }));
DBConnection.users.Indexes.CreateOne(new CreateIndexModel<AppUser>(Builders<AppUser>.IndexKeys.Ascending(x => x.Email), new CreateIndexOptions { Unique = true }));

app.Run();
