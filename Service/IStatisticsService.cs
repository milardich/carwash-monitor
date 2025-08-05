using CarwashMonitor.Model;

namespace CarwashMonitor.Service
{
    public interface IStatisticsService
    {
        Task<StatisticsHighlightsDto> GetStatisticsHighlightsAsync(DateTime? dateFrom, DateTime? dateTo);
        Task<StatisticsSummaryDto> GetStatisticsSummaryAsync(DateTime? dateFrom, DateTime? dateTo);
    }
}
