namespace CarwashMonitor.Dtos;

public class WashCycleDto
{
    public Guid Id { get; set; }
    public float? WaterConsumption { get; set; }
    public float? DetergentConsumption { get; set; }
    public float? WaxConsumption { get; set; }
    public int? CoinAmount { get; set; }
    public DateTime? DateCreated { get; set; }
}