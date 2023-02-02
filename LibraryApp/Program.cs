using System.Reflection.Metadata;
using LibraryApp.Data;
using Microsoft.AspNetCore.Components.Authorization;
using LibraryApp.Identity;
using LibraryApp.Data.Migrations;
using LibraryApp.Data.Models;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Radzen;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));

services.AddSingleton<IMongoDbSettings>(serviceProvider =>
    serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);

services.AddSingleton(typeof(IMongoRepository<>), typeof(MongoRepository<>));

services.AddDatabaseDeveloperPageExceptionFilter();
services.AddAuthenticationCore();
services.AddRazorPages();
services.AddServerSideBlazor();
services.AddScoped<DialogService>();
services.AddScoped<NotificationService>();
services.AddScoped<TooltipService>();
services.AddScoped<ContextMenuService>();
services.AddScoped<ProtectedSessionStorage>();
services.AddScoped<AuthenticationStateProvider, LibAuthenticationStateProvider>();
services.AddSingleton<LibraryUserProvider>();
services.AddSingleton<Migrator>();
services.AddSingleton<PasswordHasher<LibraryUser>>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

var timer = new PeriodicTimer(TimeSpan.FromMinutes(10));

//while (await timer.WaitForNextTickAsync())
//{
//Method from notifier to be executed every 10 minutes
//}

app.Run();