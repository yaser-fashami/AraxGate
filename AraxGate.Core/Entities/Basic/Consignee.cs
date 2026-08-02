using AraxGate.Core.Domain.Entities.Operation;
using System.ComponentModel;

namespace AraxGate.Core.Domain.Entities.Basic;

public class Consignee : BaseAuditableEntity<uint>
{
    public string ConsigneeName { get; set; }
    public string ConsigneeNameEng { get; set; }

    public string? TelNo { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Description { get; set; }
    public string? PostalCode { get; set; }
    public string? NationalCode { get; set; }
    public string? EconomicCode { get; set; }

    public ConsigneeType ConsigneeType { get; set; }

    public virtual ICollection<GateEntrance>? GateEntrances { get; set; }
}

public enum ConsigneeType
{
    [Description("حقیقی")]
    IndividualPersonality,
    [Description("حقوقی")]
    LegalPersonality
}
