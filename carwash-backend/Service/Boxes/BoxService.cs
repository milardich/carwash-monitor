using AutoMapper;
using CarwashMonitor.Dtos;
using CarwashMonitor.Enums;
using CarwashMonitor.Models;
using Microsoft.EntityFrameworkCore;

namespace CarwashMonitor.Service.Boxes;

public class BoxService : IBoxService
{
    private readonly CarwashDbContext _context;
    private readonly IMapper _mapper;

    public BoxService(CarwashDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
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
            Status = BoxStatus.INACTIVE,
            CoinTrayLimit = 1000, 
            CoinTrayAmount = 0,
        };

        _context.Boxes.Add(newBox);
        return await _context.SaveChangesAsync();
    }

    public async Task<BoxDto?> GetBoxAsync(Guid boxId)
    {
        var now = DateTime.UtcNow;
        var start = now.Date; // today at 00:00
        var end = start.AddDays(1); // tomorrow at 00:00

        var boxEntity = await _context.Boxes
            .Include(b => b.WashCycles)
            .Where(box => box.Id == boxId).FirstOrDefaultAsync();

        boxEntity.WashCycles = boxEntity.WashCycles
            .Where(wc =>
                wc.DateCreated >= start &&
                wc.DateCreated < end)
            .ToList();

        return _mapper.Map<BoxDto>(boxEntity);
    }

    public async Task<BoxDto?> UpdateBoxStatusAsync(Guid boxId, BoxStatusDto status)
    {
        var boxEntity = await _context.Boxes.FirstOrDefaultAsync(b => b.Id == boxId);

        if (boxEntity == null)
            return null;

        if (!Enum.TryParse<BoxStatus>(status.Status, out var newStatus))
            throw new ArgumentException("Invalid status value.");

        boxEntity.Status = newStatus;
        await _context.SaveChangesAsync();

        return _mapper.Map<BoxDto>(boxEntity);
    }

    public async Task<BoxDto?> EmptyTray(Guid boxId)
    {
        var boxEntity = await _context.Boxes.FirstOrDefaultAsync(b => b.Id == boxId);

        if (boxEntity == null)
            return null;

        boxEntity.CoinTrayAmount = 0;
        await _context.SaveChangesAsync();

        return _mapper.Map<BoxDto?>(boxEntity);
    }

    public async Task<int> DeleteBoxAsync(Guid boxId)
    {
        var boxEntity = await _context.Boxes
            .Include(b => b.WashCycles)
            .FirstOrDefaultAsync(b => b.Id == boxId);

        if (boxEntity == null)
            throw new Exception("Box not found.");

        if (boxEntity.Status != BoxStatus.INACTIVE && boxEntity.Status != BoxStatus.MAINTENANCE)
            throw new InvalidOperationException("Only boxes in INACTIVE or MAINTENANCE status can be deleted.");

        _context.WashCycles.RemoveRange(boxEntity.WashCycles);
        _context.Boxes.Remove(boxEntity);
        return await _context.SaveChangesAsync();
    }

}