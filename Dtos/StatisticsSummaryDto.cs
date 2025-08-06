namespace CarwashMonitor.Dtos;

public class StatisticsSummaryDto
{
    public List<StationStatisticsDto>? allStationStatistics = [];
    public float TotalDetergentCost;
    public float TotalRevenue;
    public float TotalWaterCost;
    public float TotalWaxCost;
}