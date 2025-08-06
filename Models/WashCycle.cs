namespace CarwashMonitor.Models;

public class WashCycle
{
    public Guid Id { get; set; }
    public Box? Box { get; set; }
    public Guid BoxId { get; set; }
    public float? WaterConsumption { get; set; }
    public float? DetergentConsumption { get; set; }
    public float? WaxConsumption { get; set; }
    public DateTime DateCreated { get; set; }
    public int? CoinAmount { get; set; } = 0;
}