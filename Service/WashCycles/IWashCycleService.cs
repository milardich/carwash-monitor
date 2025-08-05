using CarwashMonitor.Dtos;
using CarwashMonitor.Models;

namespace CarwashMonitor.Service.WashCycles
{
    public interface IWashCycleService
    {
        Task<int> CreateWashCycleAsync(Guid boxId, WashCycleCreateDto washCycle);
        Task<WashCycle?> GetWashCycleAsync(Guid washCycleId);
        Task<List<WashCycle>> GetAllWashCyclesAsync(Guid boxId, DateTime? dateFrom, DateTime? dateTo);
    }
}
