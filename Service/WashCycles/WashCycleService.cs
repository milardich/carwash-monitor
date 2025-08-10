using CarwashMonitor.Dtos;
using CarwashMonitor.Models;
using Microsoft.EntityFrameworkCore;

namespace CarwashMonitor.Service.WashCycles;

public class WashCycleService : IWashCycleService
{
    private readonly CarwashDbContext _context;

    public WashCycleService(
        CarwashDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateWashCycleAsync(Guid boxId, WashCycleDto washCycleDto)
    {
        var washCycle = new WashCycle
        {
            Id = Guid.NewGuid(),
            BoxId = boxId,
            WaterConsumption = washCycleDto.WaterConsumption,
            WaxConsumption = washCycleDto.WaxConsumption,
            DetergentConsumption = washCycleDto.DetergentConsumption,
            CoinAmount = washCycleDto.CoinAmount,
            DateCreated = DateTime.UtcNow
        };
        await _context.AddAsync(washCycle);
        return await _context.SaveChangesAsync();
    }

    public async Task<WashCycleDto?> GetWashCycleAsync(Guid washCycleId)
    {
        var result = await _context.WashCycles.FirstOrDefaultAsync(wc => wc.Id == washCycleId);
        return new WashCycleDto
        {
            Id = result.Id,
            WaterConsumption = result.WaterConsumption ?? 0,
            DetergentConsumption = result.DetergentConsumption ?? 0,
            WaxConsumption = result.WaxConsumption ?? 0,
            CoinAmount = result.CoinAmount ?? 0,
            DateCreated = result.DateCreated
        };
    }

    public async Task<List<WashCycleDto>> GetAllWashCyclesAsync(Guid boxId, DateTime? dateFrom, DateTime? dateTo)
    {
        var query = _context.WashCycles.AsQueryable();

        query = query.Where(wc => wc.Id == boxId);

        if (dateFrom.HasValue)
            query = query.Where(wc => wc.DateCreated > dateFrom.Value);

        if (dateTo.HasValue)
            query = query.Where(wc => wc.DateCreated < dateTo.Value);

        var result = await query.ToListAsync();

        return result.Select(wc => new WashCycleDto
        {
            Id = wc.Id,
            WaterConsumption = wc.WaterConsumption ?? 0,
            DetergentConsumption = wc.DetergentConsumption ?? 0,
            WaxConsumption = wc.WaxConsumption ?? 0,
            CoinAmount = wc.CoinAmount ?? 0,
            DateCreated = wc.DateCreated
        }).ToList();
    }
}