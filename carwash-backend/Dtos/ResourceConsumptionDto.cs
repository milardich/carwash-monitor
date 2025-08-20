namespace CarwashMonitor.Dtos;

public class ResourceConsumptionDto
{
    public List<string> Labels { get; set; } = [];
    public List<float> Data { get; set; } = [];
}