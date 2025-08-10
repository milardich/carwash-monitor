using CarwashMonitor.Dtos;
using CarwashMonitor.Enums;
using CarwashMonitor.Models;
using Microsoft.EntityFrameworkCore;

namespace CarwashMonitor.Service.Boxes;

public class BoxService : IBoxService
{
    private readonly CarwashDbContext _context;

    public BoxService(CarwashDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateBoxAsync(Guid stationId)
    {
        var station = await _context.Stations
            .Include(s => s.Boxes)
            .FirstOrDefaultAsync(station => station.Id == stationId);

        if (station == null)
            throw new Exception("Station not found.");

        var boxNumber = station.Boxes.Count() + 1;

        var newBox = new Box
        {
            Id = Guid.NewGuid(),
            Number = boxNumber,
            StationId = stationId,
            Status = BoxStatus.INACTIVE
        };

        _context.Boxes.Add(newBox);
        return await _context.SaveChangesAsync();
    }

    public async Task<BoxDto?> GetBoxAsync(Guid boxId)
    {
        var now = DateTime.UtcNow;
        var start = now.Date; // today at 00:00
        var end = start.AddDays(1); // tomorrow at 00:00

        var box = await _context.Boxes
            .Include(b => b.WashCycles)
            .Where(box => box.Id == boxId).FirstOrDefaultAsync();

        var washCycles = box?.WashCycles
            .Where(wc =>
                wc.DateCreated >= start &&
                wc.DateCreated < end).ToList();

        var dto = new BoxDto
        {
            WashCycleCount = washCycles?.Count() ?? 0,
            TotalCoinAmount = washCycles?.Sum(wc => wc.CoinAmount ?? 0),
            TotalWaterConsumption = washCycles?.Sum(wc => wc.WaterConsumption ?? 0),
            TotalWaxConsumption = washCycles.Sum(wc => wc.WaxConsumption ?? 0),
            TotalDetergentConsumption = washCycles?.Sum(wc => wc.DetergentConsumption ?? 0),
            Status = box.Status.ToString(),
            WashCycles = washCycles.Select(wc => new WashCycleDto
            {
                Id = wc.Id,
                WaterConsumption = wc.WaterConsumption ?? 0,
                DetergentConsumption = wc.DetergentConsumption ?? 0,
                WaxConsumption = wc.WaxConsumption ?? 0,
                CoinAmount = wc.CoinAmount ?? 0,
                DateCreated = wc.DateCreated
            }).ToList()
        };
        return dto;
    }

    public async Task<BoxDto?> UpdateBoxStatusAsync(Guid boxId, BoxStatusDto status)
    {
        var box = await _context.Boxes.FirstOrDefaultAsync(b => b.Id == boxId);

        if (box == null)
            return null;

        if (!Enum.TryParse<BoxStatus>(status.Status, out var newStatus))
            throw new ArgumentException("Invalid status value.");

        box.Status = newStatus;
        await _context.SaveChangesAsync();

        return new BoxDto
        {
            Id = box.Id,
            Number = box.Number,
            Status = box.Status.ToString()
        };
    }
}