using CarwashMonitor.Models;

namespace CarwashMonitor.Service
{
    public interface IStationService
    {
        Task<int> CreateStationAsync(Station station);
        Task<Station?> GetStationAsync(Guid stationId);
        Task<List<Station>> GetStationsAsync();
    }
}
