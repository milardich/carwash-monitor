using CarwashMonitor.Model;
using CarwashMonitor.Models;
using CarwashMonitor.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarwashMonitor.Controllers
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

        #endregion



        #region REST models

        #endregion
    }
}
