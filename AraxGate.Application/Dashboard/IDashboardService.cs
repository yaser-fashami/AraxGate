using AraxGate.Core.Domain.Dtos;
using AraxGate.Utilities.Exceptions;
using System.Collections;

namespace AraxGate.Application.Dashboard;

public interface IDashboardService
{
    Task<DounutChartDto> DounutChartDataAsync(DateTime date);
    Task<DounutChartDto> DounutTotalCountAsync(DateTime from, DateTime to);
    Task<Array> WeekEnteranceData(DateTime startOfWeek);
}
