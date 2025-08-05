using CarwashMonitor.Dtos;
using CarwashMonitor.Models;
using Microsoft.EntityFrameworkCore;

namespace CarwashMonitor.Service.WashCycles
{
    public class WashCycleService : IWashCycleService
    {
        private readonly CarwashDbContext _context;

        public WashCycleService(
            CarwashDbContext context) 
        { 
            _context = context;
        }

        public async Task<int> CreateWashCycleAsync(Guid boxId, WashCycleCreateDto washCycleDto)
        {
            var washCycle = new WashCycle
            {
                Id = Guid.NewGuid(),
                BoxId = boxId,
                WaterConsumption = washCycleDto.WaterConsumption,
                WaxConsumption = washCycleDto.WaxConsumption,
                DetergentConsumption = washCycleDto.DetergentConsumption,
                CoinAmount = washCycleDto.CoinAmount,
                DateCreated = DateTime.UtcNow,
            };
            await _context.AddAsync(washCycle);
            return await _context.SaveChangesAsync();
        }

        public async Task<WashCycle?> GetWashCycleAsync(Guid washCycleId)
        {
            return await _context.WashCycles.FirstOrDefaultAsync(wc => wc.Id == washCycleId);
        }

        public async Task<List<WashCycle>> GetAllWashCyclesAsync(Guid boxId, DateTime? dateFrom, DateTime? dateTo)
        {
            var query = _context.WashCycles.AsQueryable();

            query = query.Where(wc => wc.Id == boxId);

            if (dateFrom.HasValue)
                query = query.Where(wc => wc.DateCreated > dateFrom.Value);

            if (dateTo.HasValue) 
                query = query.Where(wc => wc.DateCreated < dateTo.Value);

            return await query.ToListAsync();
        }

    }
}
