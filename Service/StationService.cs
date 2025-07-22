using CarwashMonitor.Model;
using CarwashMonitor.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CarwashMonitor.Service
{
    public class StationService : IStationService
    {
        private readonly CarwashDbContext _context; 

        public StationService(CarwashDbContext context) 
        { 
            _context = context;
        }

        public async Task<int> CreateStationAsync(Station station)
        {
            _context.Add(station);
            return await _context.SaveChangesAsync();
        }

        public async Task<Station?> GetStationAsync(Guid stationId)
        {
            return await _context.Stations.FirstOrDefaultAsync(station => station.Id == stationId);
        }

        public async Task<List<Station>> GetStationsAsync()
        {
            return await _context.Stations.ToListAsync();
        }
    }
}
