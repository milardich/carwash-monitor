using CarwashMonitor.Dtos;
using CarwashMonitor.Models;
using CarwashMonitor.Service.WashCycles;
using Microsoft.AspNetCore.Mvc;

namespace CarwashMonitor.Controller
{
    [ApiController]
    public class WashCycleController : ControllerBase
    {
        public IWashCycleService WashCycleService { get; set; }

        public WashCycleController(
            IWashCycleService washCycleService) 
        { 
            WashCycleService = washCycleService;
        }

        [HttpPost]
        [Route("/box/{boxId}/washcycle")]
        public async Task<ActionResult<int>> CreateWashCycleAsync(Guid boxId, WashCycleCreateDto washCycleDto)
        {
            var result = await WashCycleService.CreateWashCycleAsync(boxId, washCycleDto);
            return Ok(result);
        }

        [HttpGet]
        [Route("/washcycle/{washCycleId}")]
        public async Task<ActionResult<WashCycle>> GetWashCycleAsync(Guid washCycleId)
        {
            var result = await WashCycleService.GetWashCycleAsync(washCycleId);
            return Ok(result);
        }

        [HttpGet]
        [Route("/box/{boxId}/washcycles")]
        public async Task<ActionResult<List<WashCycle>>> GetAllWashCycles(Guid boxId, [FromQuery] DateTime? dateFrom, [FromQuery] DateTime? dateTo)
        {
            var result = await WashCycleService.GetAllWashCyclesAsync(boxId, dateFrom, dateTo);
            return Ok(result);
        }
    }
}
