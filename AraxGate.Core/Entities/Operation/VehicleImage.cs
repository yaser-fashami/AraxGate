namespace AraxGate.Core.Domain.Entities.Operation;

public class VehicleImage : BaseEntity<long>
{
    public byte[]? ImageData { get; set; }
    public string? ImagePath { get; set; }
    public DateTime CreatedAt { get; set; }
    public GateEntrance? GateIn { get; set; }
    public GateEntrance? GateOut { get; set; }


}
