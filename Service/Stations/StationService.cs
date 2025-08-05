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

        public async Task<StationGetDto> GetStationAsync(Guid stationId)
        {
            var station = await _context.Stations
                .Include(s => s.Boxes)
                .FirstOrDefaultAsync(station => station.Id == stationId);

            if (station == null)
            {
                throw new Exception("Station not found.");
            }

            var stationDto = new StationGetDto
            {
                Id = station.Id,
                Name = station.Name,
                BoxInfos = station.Boxes.Select(box => new BoxInfoDto
                {
                    Id = box.Id,
                    Number = box.Number,
                    Status = box.Status.ToString()
                }).ToList()
            };

            return stationDto;
        }

        public async Task<List<StationGetDto>> GetStationsAsync()
        {
            var result = await _context.Stations
                .Include(s => s.Boxes)
                .ToListAsync();

            var stationDtos = result.Select(station => new StationGetDto
            {
                Id = station.Id,
                Name = station.Name,
                BoxInfos = station.Boxes.Select(box => new BoxInfoDto
                {
                    Id = box.Id,
                    Number = box.Number,
                    Status = box.Status.ToString(),
                }).ToList()
            }).ToList();

            return stationDtos;
        }
    }
}
