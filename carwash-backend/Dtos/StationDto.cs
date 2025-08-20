namespace CarwashMonitor.Dtos;

public class StationDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public List<BoxDto> Boxes { get; set; } = [];
}