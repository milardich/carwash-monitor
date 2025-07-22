using CarwashMonitor.Model;

namespace CarwashMonitor.Service
{
    public interface IBoxService
    {
        Task<int> CreateBoxAsync(Guid stationId);
        Task<Box?> GetBoxAsync(Guid boxId);
        Task<List<Box?>> GetBoxesAsync(Guid stationId);
    }
}
