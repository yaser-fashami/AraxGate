namespace AraxGate.Core.Domain.Dtos;

public record FromToDateRequest
{
    public int FromYear { get; set; }
    public int FromMonth { get; set; }
    public int FromDay { get; set; }

    public int ToYear { get; set; }
    public int ToMonth { get; set; }
    public int ToDay { get; set; }
}
