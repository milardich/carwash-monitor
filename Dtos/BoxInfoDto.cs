namespace CarwashMonitor.Dtos
{
    public class BoxInfoDto
    {
        public Guid Id { get; set; }
        public int? Number { get; set; }
        public int? WashCycleCount { get; set; }
        public int? TotalCoinAmount  { get; set; }
        public float? TotalWaterConsumption { get; set; }
        public float? TotalWaxConsumption { get; set; }
        public float? TotalDetergentConsumption { get; set; }
        public string? Status { get; set; }
        public List<WashCycleDto> WashCycles { get; set; } = [];
    }
}
