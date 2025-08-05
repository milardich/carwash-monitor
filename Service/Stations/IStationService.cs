using CarwashMonitor.Dtos;
using CarwashMonitor.Models;

namespace CarwashMonitor.Service.Stations
{
    public interface IStationService
    {
        Task<int> CreateStationAsync(StationCreateDto station);
        Task<StationGetDto> GetStationAsync(Guid stationId);
        Task<List<StationGetDto>> GetStationsAsync();
    }
}
