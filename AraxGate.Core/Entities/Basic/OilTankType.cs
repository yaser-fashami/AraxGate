using AraxGate.Core.Domain.Entities.Operation;

namespace AraxGate.Core.Domain.Entities.Basic;

public class OilTankType : BaseEntity<ushort>
{
    public string TankName { get; set; }

    public TankType TankType { get; set; }

    public string TankGroup { get; set; }

    public string? Description { get; set; }


    public virtual ICollection<GateEntrance>? GateEntrances { get; set; }
}

public enum TankType
{
    Light,
    Heavy
}

