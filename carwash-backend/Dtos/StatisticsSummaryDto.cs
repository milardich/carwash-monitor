namespace CarwashMonitor.Dtos;

public class StatisticsSummaryDto
{
    public List<StationStatisticsDto> AllStationStatistics { get; set; } = [];
    public float TotalDetergentCost { get; set; }
    public float TotalRevenue { get; set; }
    public float TotalWaterCost { get; set; }
    public float TotalWaxCost { get; set; }
}