using CarwashMonitor.Dtos;

namespace CarwashMonitor.Service.WashCycles;

public interface IWashCycleService
{
    Task<int> CreateWashCycleAsync(Guid boxId, WashCycleDto washCycle);
    Task<WashCycleDto?> GetWashCycleAsync(Guid washCycleId);
    Task<List<WashCycleDto>> GetAllWashCyclesAsync(Guid boxId, DateTime? dateFrom, DateTime? dateTo);
}