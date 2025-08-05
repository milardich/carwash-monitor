using CarwashMonitor.Dtos;
using CarwashMonitor.Enums;
using CarwashMonitor.Models;
using Microsoft.EntityFrameworkCore;

namespace CarwashMonitor.Service.Boxes
{
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

            var newBox = new Box()
            { 
                Id = Guid.NewGuid(),
                Number = boxNumber,
                StationId = stationId,
                Status = BoxStatus.INACTIVE,
            };

            _context.Boxes.Add(newBox);
            return await _context.SaveChangesAsync();
        }

        public async Task<Box?> GetBoxAsync(Guid boxId)
        {
            return await _context.Boxes
                .Include(b => b.WashCycles)
                .FirstOrDefaultAsync(box => box.Id == boxId);
        }

        public async Task<List<Box>?> GetBoxesAsync(Guid stationId)
        {
            var station = await _context.Stations
                .Include(s => s.Boxes)
                .FirstOrDefaultAsync(station => station.Id == stationId);

            if (station == null)
                throw new Exception("Station not found.");

            return station.Boxes;
        }

        public async Task<BoxInfoDto?> GetBoxInfoAsync(Guid boxId)
        {
            var washCycles = await _context.WashCycles.Where(wc => wc.BoxId == boxId).ToListAsync();
            var dto = new BoxInfoDto
            {
                WashCycleCount = washCycles.Count,
                TotalCoinAmount = washCycles.Sum(wc => wc.CoinAmount ?? 0),
                TotalWaterConsumption = washCycles.Sum(wc => wc.WaterConsumption ?? 0),
                TotalWaxConsumption = washCycles.Sum(wc => wc.WaxConsumption ?? 0),
                TotalDetergentConsumption = washCycles.Sum(wc => wc.DetergentConsumption ?? 0),
                Status = await _context.Boxes
                    .Where(b => b.Id == boxId)
                    .Select(b => b.Status.ToString())
                    .FirstOrDefaultAsync()
            };
            return dto;
        }

        public async Task<Box?> UpdateBoxStatusAsync(Guid boxId, BoxStatusDto status)
        {
            var box = await _context.Boxes.FirstOrDefaultAsync(b => b.Id == boxId);

            if (box == null)
                return null;

            if (!Enum.TryParse<BoxStatus>(status.Status, out var newStatus))
                throw new ArgumentException("Invalid status value.");

            box.Status = newStatus;
            await _context.SaveChangesAsync();

            return box;
        }
    }
}
