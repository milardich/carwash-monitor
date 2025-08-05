namespace CarwashMonitor.Dtos
{
    public class StationStatisticsDto
    {
        public string? StationName { get; set; }
        public float WaterCost { get; set; }
        public float DetergentCost { get; set; }
        public float WaxCost { get; set; }
        public float Revenue { get; set; }
    }
}
