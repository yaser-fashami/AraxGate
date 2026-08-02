using AraxGate.Core.Domain.Entities.Operation;

namespace AraxGate.Core.Domain.Entities.Basic;
public class BaskoolSetting : BaseEntity<ushort>
{
    public string BaskoolMACAddress { get; set; }
    public BaskoolType BaskoolType { get; set; }
    public BaskoolRole BaskoolRole { get; set; }
}

public enum BaskoolType
{
    Gate_A1 = 0,
    Gate_A2 = 2,
    Gate_B1 = 1,
    Gate_B2 = 3,
}
public enum BaskoolRole
{
    Enter,
    Exit,
    EnterExit,
}
