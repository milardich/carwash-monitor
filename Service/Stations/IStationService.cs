using CarwashMonitor.Dtos;
using CarwashMonitor.Models;

namespace CarwashMonitor.Service.Stations
{
    public interface IStationService
    {
        Task<int> CreateStationAsync(StationCreateDto station);
        Task<Station?> GetStationAsync(Guid stationId);
        Task<List<Station>?> GetStationsAsync();
    }
}
