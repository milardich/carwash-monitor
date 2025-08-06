using CarwashMonitor.Models;
using CarwashMonitor.Service.Boxes;
using CarwashMonitor.Service.Stations;
using CarwashMonitor.Service.Statistics;
using CarwashMonitor.Service.WashCycles;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Db context
builder.Services.AddDbContext<CarwashDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("carwash"))
);

// services
builder.Services.AddScoped<IStationService, StationService>();
builder.Services.AddScoped<IBoxService, BoxService>();
builder.Services.AddScoped<IWashCycleService, WashCycleService>();
builder.Services.AddScoped<IStatisticsService, StatisticsService>();

// Add CORS support
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// controllers
builder.Services.AddControllers();

// swagger / OpenApi config
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Carwash Monitor API",
        Version = "v1",
        Description = "API documentation for the Carwash monitoring system."
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Carwash Monitor API V1");
        options.RoutePrefix = "";
    });
}

app.UseCors();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();