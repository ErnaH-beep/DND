global using Shared.Models;
using Blazored.LocalStorage;
using Frontend.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
    
builder.Services.AddBlazorBootstrap();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddAuthorizationCore();
builder.Services.AddRadzenComponents();


// Make HTTP requests to the backend
builder.Services.AddHttpClient("BackendAPI", client => 
{
    client.BaseAddress = new Uri("https://localhost:5001");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.UseDeveloperExceptionPage();  // Enable detailed error pages for development

app.Run();