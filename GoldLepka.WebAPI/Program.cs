using Application;
using DAL.SqlServer.Context;
using GoldLepka.WebAPI.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Swashbuckle.AspNetCore.Annotations;
using System.Globalization; // 👈 1. Əlavə edin

// Rəqəmlərin nöqtə ilə ötürülməsini təmin etmək üçün mədəniyyət parametrlərini Invariant Culture olaraq təyin edirik
CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture; // 👈 2. Əlavə edin
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture; // 👈 3. Əlavə edin

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// ... qalan xidmətlər dəyişməz qalır ...

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.OperationFilter<SwaggerFileOperationFilter>();
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MSSql")));

builder.Services.AddBlServices(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();
// ... qalan app konfiqurasiyası dəyişməz qalır ...

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.WebRootPath, "uploads")),
    RequestPath = "/uploads"
});

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthorization();
app.MapControllers();
app.Run();