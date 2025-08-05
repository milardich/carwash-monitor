namespace CarwashMonitor.Dtos
{
    public class BoxInfoDto
    {
        public int? WashCycleCount { get; set; }
        public int? TotalCoinAmount  { get; set; }
        public float? TotalWaterConsumption { get; set; }
        public float? TotalWaxConsumption { get; set; }
        public float? TotalDetergentConsumption { get; set; }
        public string? BoxStatus { get; set; }
    }
}
