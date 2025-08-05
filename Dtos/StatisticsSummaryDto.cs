namespace CarwashMonitor.Dtos
{
    public class StatisticsSummaryDto
    {
        public List<StationStatisticsDto>? allStationStatistics = [];
        public float TotalWaterCost;
        public float TotalDetergentCost;
        public float TotalWaxCost;
        public float TotalRevenue;
    }
}
