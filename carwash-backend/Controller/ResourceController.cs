using CarwashMonitor.Dtos;
using CarwashMonitor.Service.Resources;
using Microsoft.AspNetCore.Mvc;

namespace CarwashMonitor.Controller;

[ApiController]
public class ResourceController : ControllerBase
{
    private readonly IResourceService ResourceService;

    public ResourceController(IResourceService resourceService)
    {
        ResourceService = resourceService;
    }

    [HttpGet]
    [Route("/station/{stationId}/resources")]
    public async Task<ActionResult<StationResourceSummaryDto>> GetStationResourceSummary(Guid stationId)
    {
        var result = await ResourceService.GetResourceSummaryForStationAsync(stationId);
        return Ok(result);
    }
}