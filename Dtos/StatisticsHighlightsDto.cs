namespace CarwashMonitor.Dtos
{
    public class StatisticsHighlightsDto
    {
        public float TotalRevenue { get; set; }
        public string? BestStationName { get; set; }
        public float BestStationRevenue { get; set; }
        public int TotalWashCount { get; set; }
        public float TotalWaterConsumption { get; set; }
        public float TotalWaxConsumption { get; set; }
        public float TotalDetergentConsumption { get; set; }
        public float TotalWaterCost { get; set; }
        public float TotalWaxCost { get; set; }
        public float TotalDetergentCost { get; set; }
    }
}
