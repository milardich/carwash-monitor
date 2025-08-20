using CarwashMonitor.Dtos;

namespace CarwashMonitor.Service.Resources;

public interface IResourceService
{
    Task<StationResourceSummaryDto> GetResourceSummaryForStationAsync(Guid stationId);
}