namespace CarwashMonitor.Dtos;

public class StationResourceSummaryDto
{
    public ResourceConsumptionDto Water { get; set; } = new();
    public ResourceConsumptionDto Wax { get; set; } = new();
    public ResourceConsumptionDto Detergent { get; set; } = new();
}