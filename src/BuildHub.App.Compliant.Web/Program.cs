using BuildHub.App.Compliant.Shared;
using BuildHub.App.Compliant.Web.Components;
using BuildHub.App.Compliant.Shared.Services;
using BuildHub.App.Compliant.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add device-specific services used by the BuildHub.App.Compliant.Shared project
builder
    .Services
    .AddSingleton<IFormFactor, FormFactor>()
    .AddHttpClient()
    .AddSharedServices()
    ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(BuildHub.App.Compliant.Shared._Imports).Assembly);

app.Run();
