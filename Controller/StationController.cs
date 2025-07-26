using CarwashMonitor.Model;
using CarwashMonitor.Models;
using CarwashMonitor.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarwashMonitor.Controllers
{
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

        private IStationService StationService { get; set; }
        private IBoxService BoxService { get; set; }

        #endregion



        #region Methods

        [HttpPost]
        [Route("/station")]
        public async Task<ActionResult<int>> CreateStationAsync(Station station)
        {
            station.Id = Guid.NewGuid();
            var response = await StationService.CreateStationAsync(station);
            return Ok(response);
        }

        [HttpGet]
        [Route("/station")]
        public async Task<ActionResult<List<Station>>> GetStationsAsync()
        {
            var response = await StationService.GetStationsAsync();
            return Ok(response);
        }

        [HttpGet]
        [Route("/station/{stationId}")]
        public async Task<ActionResult<Station>> GetStationAsync(Guid stationId)
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



        #region REST models

        #endregion
    }
}
