using Api.Controllers;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Cross-Origin Resource Sharing Policy
// From  Marinko Spasojevic,  https://code-maze.com/blazor-webassembly-httpclient/
builder.Services.AddCors(policy =>
{
    policy.AddPolicy("CorsPolicy", opt => opt
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
        .WithExposedHeaders("*"));
});


// API
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Repository
builder.Services.AddScoped<FileBrowserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Call UseStatusCodePages before request handling middleware
    // app.UseStatusCodePagesWithRedirects("/CustomErrorPages/{0}");
    app.UseStatusCodePages(
        "text/html",
        "<h2>An Error Occured Processing Your Request</h1>" +
        "<h4>Status Code: {0}</h2>");
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
    bool IsDevelopment = true;
}
else
{
    app.UseExceptionHandler("/error");
}

app.UseHsts();
app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();
