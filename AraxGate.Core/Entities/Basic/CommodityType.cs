using AraxGate.Core.Domain.Entities.Operation;

namespace AraxGate.Core.Domain.Entities.Basic;

public class CommodityType : BaseEntity<ushort>
{
    public string CommodityTypeName { get; set; }
    public string? Description { get; set; }


    public virtual ICollection<GateEntrance>? GateEntrances { get; set; }
}
