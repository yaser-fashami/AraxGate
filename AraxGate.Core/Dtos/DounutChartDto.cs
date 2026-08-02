namespace AraxGate.Core.Domain.Dtos;

public class DounutChartDto
{
    public int EnteranceCount { get; set; }
    public int ExitedCount { get; set; }
    public int NotExitedLately { get; set; }
    public int TotalInWeek { get; set; }
}
