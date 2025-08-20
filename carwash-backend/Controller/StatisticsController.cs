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
    public async Task<ActionResult<StatisticsHighlightsDto>> GetStatisticsHighlightsAsync()
    {
        var result = await StatisticsService.GetStatisticsHighlightsAsync();
        return Ok(result);
    }

    [HttpGet]
    [Route("/statistics/summary")]
    public async Task<ActionResult<StatisticsSummaryDto>> GetStatisticsSummaryAsync()
    {
        var result = await StatisticsService.GetStatisticsSummaryAsync();
        return Ok(result);
    }
}