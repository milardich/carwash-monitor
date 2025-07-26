using CarwashMonitor.Model;
using CarwashMonitor.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CarwashDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("carwash"))
);

builder.Services.AddScoped<IStationService, StationService>();
builder.Services.AddScoped<IBoxService, BoxService>();
builder.Services.AddScoped<IWashCycleService, WashCycleService>();

builder.Services.AddControllers();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
}
app.UseHttpsRedirection();
app.MapControllers();
app.Run();