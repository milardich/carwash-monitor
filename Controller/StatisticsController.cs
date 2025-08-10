using CarwashMonitor.Dtos;
using CarwashMonitor.Service.Statistics;
using Microsoft.AspNetCore.Mvc;

namespace CarwashMonitor.Controller;

public class StatisticsController : ControllerBase
{
    private readonly IStatisticsService StatisticsService;

    public StatisticsController(IStatisticsService statisticsService)
    {
        StatisticsService = statisticsService;
    }

    [HttpGet]
    [Route("/statistics/highlights")]
    public async Task<ActionResult<StatisticsHighlightsDto>> GetStatisticsHighlightsAsync(
        [FromQuery] DateTime? dateFrom, [FromQuery] DateTime? dateTo)
    {
        var result = await StatisticsService.GetStatisticsHighlightsAsync(dateFrom, dateTo);
        return Ok(result);
    }

    [HttpGet]
    [Route("/statistics/summary")]
    public async Task<ActionResult<StatisticsSummaryDto>> GetStatisticsSummaryAsync([FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo)
    {
        var result = await StatisticsService.GetStatisticsSummaryAsync(dateFrom, dateTo);
        return Ok(result);
    }
}