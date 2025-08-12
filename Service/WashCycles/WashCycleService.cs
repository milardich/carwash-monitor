using AutoMapper;
using CarwashMonitor.Dtos;
using CarwashMonitor.Enums;
using CarwashMonitor.Models;
using Microsoft.EntityFrameworkCore;

namespace CarwashMonitor.Service.WashCycles;

public class WashCycleService : IWashCycleService
{
    private readonly CarwashDbContext _context;
    private readonly IMapper _mapper;

    public WashCycleService(
        CarwashDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> CreateWashCycleAsync(Guid boxId, WashCycleDto washCycleDto)
    {
        var box = await _context.Boxes.FirstOrDefaultAsync(b => b.Id == boxId);

        if (box == null
            || box.IsCoinTrayFull()
            || washCycleDto.CoinAmount > box.CoinTrayLimit - box.CoinTrayAmount
            || box.Status != BoxStatus.ACTIVE)
            return 0;

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

        box.CoinTrayAmount += washCycleDto.CoinAmount ?? 0;

        box.Status = box.IsCoinTrayFull() ? BoxStatus.INACTIVE : BoxStatus.ACTIVE;

        return await _context.SaveChangesAsync();
    }

    public async Task<WashCycleDto?> GetWashCycleAsync(Guid washCycleId)
    {
        var washCycleEntity = await _context.WashCycles.FirstOrDefaultAsync(wc => wc.Id == washCycleId);
        return _mapper.Map<WashCycleDto>(washCycleEntity);
    }

    public async Task<List<WashCycleDto>> GetAllWashCyclesAsync(Guid boxId, DateTime? dateFrom, DateTime? dateTo)
    {
        var query = _context.WashCycles.AsQueryable();

        query = query.Where(wc => wc.BoxId == boxId);

        if (dateFrom.HasValue)
            query = query.Where(wc => wc.DateCreated >= dateFrom.Value.ToUniversalTime());

        if (dateTo.HasValue)
            query = query.Where(wc => wc.DateCreated <= dateTo.Value.ToUniversalTime());

        var washCycleEntities = await query.ToListAsync();

        return _mapper.Map<List<WashCycleDto>>(washCycleEntities);
    }
}