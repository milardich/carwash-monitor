using CarwashMonitor.Dtos;
using CarwashMonitor.Service.Boxes;
using Microsoft.AspNetCore.Mvc;

namespace CarwashMonitor.Controller;

[ApiController]
public class BoxController : ControllerBase
{
    #region Constructor

    public BoxController(IBoxService boxService)
    {
        BoxService = boxService;
    }

    #endregion


    #region Properties

    private IBoxService BoxService { get; }

    #endregion


    #region Methods

    [HttpPost]
    [Route("/station/{stationId}/box")]
    public async Task<ActionResult<int>> CreateBoxAsync(Guid stationId)
    {
        var response = await BoxService.CreateBoxAsync(stationId);
        return Ok(response);
    }

    [HttpGet]
    [Route("/box/{boxId}")]
    public async Task<ActionResult<BoxDto>> GetBoxInfo(Guid boxId)
    {
        var response = await BoxService.GetBoxAsync(boxId);
        return Ok(response);
    }

    [HttpPatch]
    [Route("/box/{boxId}/status")]
    public async Task<ActionResult<BoxDto>> UpdateStatus(Guid boxId, [FromBody] BoxStatusDto status)
    {
        var response = await BoxService.UpdateBoxStatusAsync(boxId, status);
        return Ok(response);
    }

    [HttpPatch]
    [Route("/box/{boxId}/empty-tray")]
    public async Task<ActionResult<BoxDto>> EmptyTray(Guid boxId)
    {
        var response = await BoxService.EmptyTray(boxId);
        return Ok(response);
    }

    [HttpDelete]
    [Route("/box/{boxId}")]
    public async Task<ActionResult<int>> DeleteBoxAsync(Guid boxId)
    {
        var response = await BoxService.DeleteBoxAsync(boxId);
        return Ok(response);
    }

    #endregion
}