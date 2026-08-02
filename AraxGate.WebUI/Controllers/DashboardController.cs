using AraxGate.Application.Dashboard;
using AraxGate.Core.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AraxGate.WebUI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : Controller
{
    private readonly IDashboardService _dashBoardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashBoardService = dashboardService;
    }


    [HttpGet("DounutChart")]
    public async Task<IActionResult> DounutChart(int year, int month, int day)
    {
        var persianCalendar = new PersianCalendar();

        DateTime date = persianCalendar.ToDateTime(
            year,
            month,
            day,
            0, 0, 0, 0);

        var data = await _dashBoardService.DounutChartDataAsync(date);

        return Ok(data);
    }

    [HttpGet("DounutTotalCount")]
    public async Task<IActionResult> DounutTotalCount([FromQuery]FromToDateRequest date)
    {
        var persianCalendar = new PersianCalendar();

        DateTime fromDate = persianCalendar.ToDateTime(
            date.FromYear,
            date.FromMonth,
            date.FromDay,
            0, 0, 0, 0);
        DateTime toDate = persianCalendar.ToDateTime(
            date.ToYear,
            date.ToMonth,
            date.ToDay,
            0, 0, 0, 0);


        var data = await _dashBoardService.DounutTotalCountAsync(fromDate, toDate);

        return Ok(data);
    }

    [HttpGet("WeekEnterance")]
    public async Task<IActionResult> WeekEnteranceData([FromQuery] FromToDateRequest date)
    {
        var persianCalendar = new PersianCalendar();

        DateTime fromDate = persianCalendar.ToDateTime(
            date.FromYear,
            date.FromMonth,
            date.FromDay,
            0, 0, 0, 0);

        var data = await _dashBoardService.WeekEnteranceData(fromDate);

        return Ok(data);
    }
}
