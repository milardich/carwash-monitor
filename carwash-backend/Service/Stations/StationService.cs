using AutoMapper;
using CarwashMonitor.Dtos;
using CarwashMonitor.Models;
using Microsoft.EntityFrameworkCore;

namespace CarwashMonitor.Service.Stations;

public class StationService : IStationService
{
    private readonly CarwashDbContext _context;
    private readonly IMapper _mapper;

    public StationService(CarwashDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
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
        var now = DateTime.UtcNow;
        var start = now.Date; // today at 00:00
        var end = start.AddDays(1); // tomorrow at 00:00

        var stationEntity = await _context.Stations
            .Include(s => s.Boxes.OrderBy(b => b.Number))
            .ThenInclude(b => b.WashCycles.Where(wc => wc.DateCreated >= start && wc.DateCreated < end))
            .FirstOrDefaultAsync(station => station.Id == stationId);

        if (stationEntity == null) throw new Exception("Station not found.");

        return _mapper.Map<StationDto>(stationEntity);
    }

    public async Task<List<StationDto>> GetStationsAsync()
    {
        var now = DateTime.UtcNow;
        var start = now.Date; // today at 00:00
        var end = start.AddDays(1); // tomorrow at 00:00

        var stationEntities = await _context.Stations
            .Include(s => s.Boxes.OrderBy(b => b.Number))
            .ThenInclude(b => b.WashCycles.Where(wc => wc.DateCreated >= start && wc.DateCreated < end))
            .ToListAsync();

        return _mapper.Map<List<StationDto>>(stationEntities);
    }
}