using CarwashMonitor.Dtos;

namespace CarwashMonitor.Service.Statistics;

public interface IStatisticsService
{
    Task<StatisticsHighlightsDto?> GetStatisticsHighlightsAsync(DateTime? dateFrom, DateTime? dateTo);
    Task<StatisticsSummaryDto?> GetStatisticsSummaryAsync(DateTime? dateFrom, DateTime? dateTo);
}