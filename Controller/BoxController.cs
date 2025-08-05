using CarwashMonitor.Dtos;
using CarwashMonitor.Models;
using CarwashMonitor.Service.Boxes;
using Microsoft.AspNetCore.Mvc;

namespace CarwashMonitor.Controller
{
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

        private IBoxService BoxService { get; set; }

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
        public async Task<ActionResult<Box>> GetBoxAsync(Guid boxId)
        {
            var response = await BoxService.GetBoxAsync(boxId);
            return Ok(response);
        }

        [HttpGet]
        [Route("/box/{boxId}/info")]
        public async Task<ActionResult<BoxInfoDto>> GetBoxInfo(Guid boxId)
        {
            var response = await BoxService.GetBoxInfoAsync(boxId);
            return Ok(response);
        }

        [HttpPatch]
        [Route("/box/{boxId}/status")]
        public async Task<ActionResult<Box>> UpdateStatus(Guid boxId, [FromBody] BoxStatusDto status)
        {
            var response = await BoxService.UpdateBoxStatusAsync(boxId, status);
            return Ok(response);
        }

        #endregion

        #region REST models

        #endregion
    }
}
