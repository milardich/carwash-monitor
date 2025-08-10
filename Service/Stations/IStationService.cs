using CarwashMonitor.Dtos;

namespace CarwashMonitor.Service.Stations;

public interface IStationService
{
    Task<int> CreateStationAsync(StationCreateDto station);
    Task<StationDto> GetStationAsync(Guid stationId);
    Task<List<StationDto>> GetStationsAsync();
}