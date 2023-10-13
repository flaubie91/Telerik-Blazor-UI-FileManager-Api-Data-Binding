using ClientUi;
using SharedLibrary.Features;
using SharedLibrary.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// API
// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7202/") });

// CSS Frameworks and Libraries
builder.Services.AddTelerikBlazor();

// Servces
builder.Services.AddScoped<FileBrowserManager>();

await builder.Build().RunAsync();
