using CarwashMonitor.Dtos;
using CarwashMonitor.Models;
using CarwashMonitor.Service.Boxes;
using CarwashMonitor.Service.Stations;
using Microsoft.AspNetCore.Mvc;

namespace CarwashMonitor.Controller;

[ApiController]
public class StationController : ControllerBase
{
    #region Constructor

    public StationController(
        IStationService stationService,
        IBoxService boxService)
    {
        StationService = stationService;
        BoxService = boxService;
    }

    #endregion


    #region Properties

    private IStationService StationService { get; }
    private IBoxService BoxService { get; }

    #endregion


    #region Methods

    [HttpPost]
    [Route("/station")]
    public async Task<ActionResult<int>> CreateStationAsync(StationCreateDto stationDto)
    {
        var response = await StationService.CreateStationAsync(stationDto);
        return Ok(response);
    }

    [HttpGet]
    [Route("/station")]
    public async Task<ActionResult<List<StationGetDto>>> GetStationsAsync()
    {
        var response = await StationService.GetStationsAsync();
        return Ok(response);
    }

    [HttpGet]
    [Route("/station/{stationId}")]
    public async Task<ActionResult<StationGetDto>> GetStationAsync(Guid stationId)
    {
        var response = await StationService.GetStationAsync(stationId);
        return Ok(response);
    }

    [HttpGet]
    [Route("/station/{stationId}/boxes")]
    public async Task<ActionResult<List<Box>>> GetStationsBoxesAsync(Guid stationId)
    {
        var response = await BoxService.GetBoxesAsync(stationId);
        return Ok(response);
    }

    #endregion
}