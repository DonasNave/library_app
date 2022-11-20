using Microsoft.AspNetCore.Components.Authorization;
using LibraryApp.Identity;
using LibraryApp.Data.Migrations;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var client = new MongoClient(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddSingleton(client);
var m = new Migrator(client.GetDatabase("library"));
m.MigrateAll();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddAuthenticationCore();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<AuthenticationStateProvider, LibAuthenticationStateProvider>();
builder.Services.AddSingleton<LibraryUserProvider>();

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

app.Run();