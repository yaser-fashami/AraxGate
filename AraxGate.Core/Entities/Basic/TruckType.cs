using AraxGate.Core.Domain.Entities.Operation;

namespace AraxGate.Core.Domain.Entities.Basic;

public class TruckType : BaseEntity<ushort>
{
    public string TruckTypeName { get; set; }
    public string? Description { get; set; }

    public virtual ICollection<GateEntrance>? GateEntrances { get; set; }
}
