using CarwashMonitor.Dtos;
using CarwashMonitor.Models;

namespace CarwashMonitor.Service.Boxes;

public interface IBoxService
{
    Task<int> CreateBoxAsync(Guid stationId);
    Task<Box?> GetBoxAsync(Guid boxId);
    Task<List<Box>?> GetBoxesAsync(Guid stationId);
    Task<BoxInfoDto?> GetBoxInfoAsync(Guid boxId);
    Task<Box?> UpdateBoxStatusAsync(Guid boxId, BoxStatusDto status);
}