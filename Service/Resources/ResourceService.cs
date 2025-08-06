using CarwashMonitor.Dtos;
using CarwashMonitor.Models;
using Microsoft.EntityFrameworkCore;

namespace CarwashMonitor.Service.Resources;

public class ResourceService : IResourceService
{
    private readonly CarwashDbContext _context;

    public ResourceService(CarwashDbContext context)
    {
        _context = context;
    }

    public async Task<StationResourceSummaryDto> GetResourceSummaryForStationAsync(Guid stationId)
    {
        var now = DateTime.UtcNow;
        var start = now.Date; // today at 00:00
        var end = start.AddDays(1); // tomorrow at 00:00

        var washCycles = await _context.WashCycles
            .Where(wc =>
                wc.Box!.StationId == stationId &&
                wc.DateCreated >= start &&
                wc.DateCreated < end)
            .ToListAsync();

        var grouped = washCycles
            .GroupBy(wc => wc.DateCreated.Hour)
            .ToDictionary(g => g.Key, g => g.ToList());

        var water = new List<float>();
        var wax = new List<float>();
        var detergent = new List<float>();
        var labels = new List<string>();

        for (var hour = 0; hour < 24; hour++)
        {
            labels.Add($"{hour:00}:00");

            var group = grouped.ContainsKey(hour) ? grouped[hour] : new List<WashCycle>();

            water.Add(group.Sum(wc => wc.WaterConsumption ?? 0));
            wax.Add(group.Sum(wc => wc.WaxConsumption ?? 0));
            detergent.Add(group.Sum(wc => wc.DetergentConsumption ?? 0));
        }

        return new StationResourceSummaryDto
        {
            Water = new ResourceConsumptionDto { Labels = labels, Data = water },
            Wax = new ResourceConsumptionDto { Labels = labels, Data = wax },
            Detergent = new ResourceConsumptionDto { Labels = labels, Data = detergent }
        };
    }
}