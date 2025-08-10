using CarwashMonitor.Dtos;
using CarwashMonitor.Models;
using Microsoft.EntityFrameworkCore;

namespace CarwashMonitor.Service.Stations;

public class StationService : IStationService
{
    private readonly CarwashDbContext _context;

    public StationService(CarwashDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateStationAsync(StationCreateDto stationDto)
    {
        var station = new Station
        {
            Id = Guid.NewGuid(),
            Name = stationDto.Name
        };
        _context.Add(station);
        return await _context.SaveChangesAsync();
    }

    public async Task<StationDto> GetStationAsync(Guid stationId)
    {
        var station = await _context.Stations
            .Include(s => s.Boxes)
            .ThenInclude(b => b.WashCycles)
            .FirstOrDefaultAsync(station => station.Id == stationId);

        if (station == null) throw new Exception("Station not found.");

        var stationDto = new StationDto
        {
            Id = station.Id,
            Name = station.Name,
            Boxes = station.Boxes.Select(b => new BoxDto
            {
                Id = b.Id,
                Number = b.Number,
                WashCycleCount = b.WashCycles.Count,
                TotalCoinAmount = b.WashCycles.Sum(wc => wc.CoinAmount ?? 0),
                TotalWaterConsumption = b.WashCycles.Sum(wc => wc.WaterConsumption ?? 0),
                TotalWaxConsumption = b.WashCycles.Sum(wc => wc.WaxConsumption ?? 0),
                TotalDetergentConsumption = b.WashCycles.Sum(wc => wc.DetergentConsumption ?? 0),
                Status = b.Status.ToString(),
                WashCycles = b.WashCycles.Select(wc => new WashCycleDto
                {
                    Id = wc.Id,
                    WaterConsumption = wc.WaterConsumption ?? 0,
                    DetergentConsumption = wc.DetergentConsumption ?? 0,
                    WaxConsumption = wc.WaxConsumption ?? 0,
                    CoinAmount = wc.CoinAmount ?? 0,
                    DateCreated = wc.DateCreated
                }).ToList()
            }).ToList()
        };

        return stationDto;
    }

    public async Task<List<StationDto>> GetStationsAsync()
    {
        var result = await _context.Stations
            .Include(s => s.Boxes)
            .ThenInclude(b => b.WashCycles)
            .ToListAsync();

        var stationDtos = result.Select(station => new StationDto
        {
            Id = station.Id,
            Name = station.Name,
            Boxes = station.Boxes.Select(b => new BoxDto
            {
                Id = b.Id,
                Number = b.Number,
                WashCycleCount = b.WashCycles.Count,
                TotalCoinAmount = b.WashCycles.Sum(wc => wc.CoinAmount ?? 0),
                TotalWaterConsumption = b.WashCycles.Sum(wc => wc.WaterConsumption ?? 0),
                TotalWaxConsumption = b.WashCycles.Sum(wc => wc.WaxConsumption ?? 0),
                TotalDetergentConsumption = b.WashCycles.Sum(wc => wc.DetergentConsumption ?? 0),
                Status = b.Status.ToString(),
                WashCycles = b.WashCycles.Select(wc => new WashCycleDto
                {
                    Id = wc.Id,
                    WaterConsumption = wc.WaterConsumption ?? 0,
                    DetergentConsumption = wc.DetergentConsumption ?? 0,
                    WaxConsumption = wc.WaxConsumption ?? 0,
                    CoinAmount = wc.CoinAmount ?? 0,
                    DateCreated = wc.DateCreated
                }).ToList()
            }).ToList()
        }).ToList();

        return stationDtos;
    }
}