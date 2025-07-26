using CarwashMonitor.Model;

namespace CarwashMonitor.Service
{
    public interface IWashCycleService
    {
        Task<int> CreateWashCycleAsync(Guid boxId, WashCycle washCycle);
        Task<WashCycle?> GetWashCycleAsync(Guid washCycleId);
        Task<List<WashCycle>> GetAllWashCyclesAsync(Guid boxId, DateTime? dateFrom, DateTime? dateTo);
    }
}
