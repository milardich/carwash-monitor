using CarwashMonitor.Constants;
using CarwashMonitor.Dtos;
using CarwashMonitor.Models;
using Microsoft.EntityFrameworkCore;

namespace CarwashMonitor.Service.Statistics;

public class StatisticsService : IStatisticsService
{
    private readonly CarwashDbContext _context;

    public StatisticsService(CarwashDbContext context)
    {
        _context = context;
    }

    public async Task<StatisticsHighlightsDto?> GetStatisticsHighlightsAsync()
    {
        var now = DateTime.UtcNow;
        var startOfMonth = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);
        var startOfNextMonth = startOfMonth.AddMonths(1);

        var filteredWashCycles = _context.WashCycles
            .Include(wc => wc.Box)
            .ThenInclude(b => b!.Station)
            .Where(wc => wc.DateCreated >= startOfMonth && wc.DateCreated < startOfNextMonth);

        var revenueByStation = await filteredWashCycles
            .GroupBy(wc => wc.Box!.Station)
            .Select(g => new
            {
                StationName = g.Key!.Name,
                Revenue = g.Sum(wc => wc.CoinAmount ?? 0)
            })
            .ToListAsync();

        var bestStation = revenueByStation
            .OrderByDescending(x => x.Revenue)
            .FirstOrDefault();

        var totalRevenue = await filteredWashCycles.SumAsync(wc => wc.CoinAmount ?? 0);
        var totalWashCount = await filteredWashCycles.CountAsync();
        var totalWaterConsumption = await filteredWashCycles.SumAsync(wc => wc.WaterConsumption ?? 0);
        var totalWaxConsumption = await filteredWashCycles.SumAsync(wc => wc.WaxConsumption ?? 0);
        var totalDetergentConsumption = await filteredWashCycles.SumAsync(wc => wc.DetergentConsumption ?? 0);

        // TODO: 
        const float waterCostPerUnit = 0.02f;
        const float waxCostPerUnit = 0.05f;
        const float detergentCostPerUnit = 0.03f;

        return new StatisticsHighlightsDto
        {
            TotalRevenue = totalRevenue,
            BestStationName = bestStation?.StationName,
            BestStationRevenue = bestStation?.Revenue ?? 0,
            TotalWashCount = totalWashCount,
            TotalWaterConsumption = totalWaterConsumption,
            TotalWaxConsumption = totalWaxConsumption,
            TotalDetergentConsumption = totalDetergentConsumption,
            TotalWaterCost = totalWaterConsumption * waterCostPerUnit,
            TotalWaxCost = totalWaxConsumption * waxCostPerUnit,
            TotalDetergentCost = totalDetergentConsumption * detergentCostPerUnit
        };
    }

    public async Task<StatisticsSummaryDto?> GetStatisticsSummaryAsync()
    {
        var now = DateTime.UtcNow;
        var startOfMonth = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);
        var startOfNextMonth = startOfMonth.AddMonths(1);

        var waterPrice = WashingResources.WaterPrice;
        var waxPrice = WashingResources.WaxPrice;
        var detergentPrice = WashingResources.DetergentPrice;

        var query = _context.Stations
            .Select(station => new StationStatisticsDto
            {
                StationName = station.Name,
                Revenue = station.Boxes
                    .SelectMany(b => b!.WashCycles)
                    .Where(wc => wc!.DateCreated >= startOfMonth && wc.DateCreated < startOfNextMonth)
                    .Sum(wc => wc!.CoinAmount ?? 0),

                WaterCost = station.Boxes
                    .SelectMany(b => b!.WashCycles)
                    .Where(wc => wc!.DateCreated >= startOfMonth && wc.DateCreated < startOfNextMonth)
                    .Sum(wc => (wc!.WaterConsumption ?? 0) * waterPrice),

                WaxCost = station.Boxes
                    .SelectMany(b => b!.WashCycles)
                    .Where(wc => wc!.DateCreated >= startOfMonth && wc.DateCreated < startOfNextMonth)
                    .Sum(wc => (wc!.WaxConsumption ?? 0) * waxPrice),

                DetergentCost = station.Boxes
                    .SelectMany(b => b!.WashCycles)
                    .Where(wc => wc!.DateCreated >= startOfMonth && wc.DateCreated < startOfNextMonth)
                    .Sum(wc => (wc!.DetergentConsumption ?? 0) * detergentPrice)
            });

        var stationStats = await query.ToListAsync();

        var summary = new StatisticsSummaryDto
        {
            AllStationStatistics = stationStats,
            TotalRevenue = stationStats.Sum(s => s.Revenue),
            TotalWaterCost = stationStats.Sum(s => s.WaterCost),
            TotalWaxCost = stationStats.Sum(s => s.WaxCost),
            TotalDetergentCost = stationStats.Sum(s => s.DetergentCost)
        };

        return summary;
    }
}