using AraxGate.Core.Domain.Entities.Basic;

namespace AraxGate.Core.Domain.Dtos;
public class OilTankGateEnteranceDto
{
    public List<Consignee> ConsigneeList { get; set; }
    public List<CommodityType> CommodityList { get; set; }
    public List<OilTankType> TankTypeList { get; set; }
    public List<TruckType> TruckTypeList { get; set; }
}
