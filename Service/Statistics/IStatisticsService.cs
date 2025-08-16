using CarwashMonitor.Dtos;

namespace CarwashMonitor.Service.Statistics;

public interface IStatisticsService
{
    Task<StatisticsHighlightsDto?> GetStatisticsHighlightsAsync();
    Task<StatisticsSummaryDto?> GetStatisticsSummaryAsync();
}