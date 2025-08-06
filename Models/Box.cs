using CarwashMonitor.Enums;

namespace CarwashMonitor.Models;

public class Box
{
    public Guid Id { get; set; }
    public int Number { get; set; }
    public BoxStatus Status { get; set; }
    public Station? Station { get; set; }
    public Guid StationId { get; set; }
    public List<WashCycle> WashCycles { get; set; } = [];
}