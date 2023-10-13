using SharedLibrary.Features;
using SharedLibrary.Models;

var builder = WebApplication.CreateBuilder(args);

// API
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7202/") });

// Add services to the container.
//builder.Services.AddRazorPages();
builder.Services.AddRazorPages(options => options.RootDirectory = "/wwwroot");
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

// Add Telerik Blazor server side services
builder.Services.AddTelerikBlazor();

// Services
builder.Services.AddSingleton<WeatherForecastService>();

// Repository Services
builder.Services.AddScoped<FileBrowserManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
