using AraxGate.Core.Domain.Entities.Operation;
using AraxGate.Utilities.Pagination;

namespace AraxGate.WebUI.Models.ViewModels;

public class OilTankGateViewModel
{
    public PagedData<GateEntrance> Items { get; set; }

}
