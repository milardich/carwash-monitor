using CarwashMonitor.Dtos;
using CarwashMonitor.Models;
using Microsoft.EntityFrameworkCore;

namespace CarwashMonitor.Service.Stations
{
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
                Name = stationDto.Name,
            };
            _context.Add(station);
            return await _context.SaveChangesAsync();
        }

        public async Task<Station?> GetStationAsync(Guid stationId)
        {
            return await _context.Stations
                .Include(s => s.Boxes)
                .FirstOrDefaultAsync(station => station.Id == stationId);
        }

        public async Task<List<Station>?> GetStationsAsync()
        {
            return await _context.Stations
                .Include (s => s.Boxes)
                .ToListAsync();
        }
    }
}
