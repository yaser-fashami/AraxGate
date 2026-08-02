using AraxGate.Core.Domain.Dtos;
using AraxGate.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace AraxGate.Application.Dashboard;

public class DashboardService : IDashboardService
{
    private readonly AraxGateDbContext _araxGateDbContext;

    public DashboardService(AraxGateDbContext araxGateDbContext)
    {
        _araxGateDbContext = araxGateDbContext;
    }

    public async Task<DounutChartDto> DounutChartDataAsync(DateTime date)
    {
        return new DounutChartDto
        {
            EnteranceCount = await _araxGateDbContext.GateEntrances.Where(d => d.GateInDate >= date && d.GateInDate <= date.AddDays(1)).CountAsync(),
            ExitedCount = await _araxGateDbContext.GateEntrances.Where(d => d.GateInDate >= date && d.GateInDate <= date.AddDays(1) && d.GateOutDate != null).CountAsync(),
            NotExitedLately = await _araxGateDbContext.GateEntrances.Where(d => d.GateInDate < date && d.GateOutDate == null).CountAsync()
        };
    }

    public async Task<DounutChartDto> DounutTotalCountAsync(DateTime from, DateTime to)
    {
        return new DounutChartDto
        {
            TotalInWeek = await _araxGateDbContext.GateEntrances.Where(d => d.GateInDate >= from && (d.GateInDate <= to || d.GateOutDate <= to)).CountAsync()
        };
    }

    public async Task<Array> WeekEnteranceData(DateTime startOfWeek)
    {
        DateTime endOfWeek = startOfWeek.AddDays(7);
        var data = await _araxGateDbContext.GateEntrances
                                            .Where(x => x.GateInDate >= startOfWeek &&
                                                        x.GateInDate < endOfWeek)
                                            .GroupBy(x => x.GateInDate.Date)
                                            .Select(g => new
                                            {
                                                Date = g.Key,
                                                Count = g.Count()
                                            })
                                            .OrderBy(x => x.Date)
                                            .ToListAsync();

        return Enumerable.Range(0, 7).Select(i =>
                                    {
                                        var date = startOfWeek.AddDays(i);

                                        return data
                                                .Where(x => x.Date == date.Date)
                                                .Select(x => x.Count)
                                                .FirstOrDefault();
                                    })
                                    .ToArray();
    }
}
