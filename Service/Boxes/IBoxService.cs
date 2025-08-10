using CarwashMonitor.Dtos;

namespace CarwashMonitor.Service.Boxes;

public interface IBoxService
{
    Task<int> CreateBoxAsync(Guid stationId);
    Task<BoxDto?> GetBoxAsync(Guid boxId);
    Task<BoxDto?> UpdateBoxStatusAsync(Guid boxId, BoxStatusDto status);
}