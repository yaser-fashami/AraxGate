using AraxGate.Core.Domain.Entities.Basic;

namespace AraxGate.Core.Domain.Entities.Operation;
public class BaskoolOperation : BaseEntity<ulong>
{
    public BaskoolType BaskoolType { get; set; }
    public float Weight { get; set; }
    public DateTime CreateDate { get; set; }
}
