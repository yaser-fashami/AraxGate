using AraxGate.Core.Domain.Entities.Basic;

namespace AraxGate.Core.Domain.Entities.Operation;
public class GateEntrance : BaseEntity<long>
{
    public DateTime GateInDate { get; set; }

    public DateTime? GateOutDate { get; set; }
    public string GateInOperatorById { get; set; }
    public User GateInOperator { get; set; }

    public string? GateOutOperatorById { get; set; }
    public User? GateOutOperator { get; set; }

    public string TruckNo { get; set; }
    public string? TruckNoletter { get; set; }

    public PlateType PlateType { get; set; }

    public string GateEntranceNo { get; set; }

    public BaskoolType Baskool { get; set; }

    public float GateInWeight { get; set; }

    public float? GateOutWeight { get; set; }
    public string CustomPermissionNo { get; set; }

    public ushort TruckTypeId { get; set; }
    public TruckType TruckType { get; set; }

    public uint ConsigneeId { get; set; }
    public Consignee Consignee { get; set; }

    public ushort CommodityTypeId { get; set; }
    public CommodityType CommodityType { get; set; }

    public ushort OilTankTypeId { get; set; }
    public OilTankType OilTankType { get; set; }

    public string? DriverName { get; set; }
    public BaskoolType? BaskoolOut { get; set; }
    public long? GateInFrontPlateVehicleImageId { get; set; }
    public VehicleImage? GateInFrontPlateVehicleImage { get; set; }

    public long? GateOutFrontPlateVehicleImageId { get; set; }
    public VehicleImage? GateOutFrontPlateVehicleImage { get; set; }

    public string? Description { get; set; }

}

public enum PlateType
{
    Iran,
    Afghan,
    Iraq
}

