using SinaOTOS.Core.Domain.Entities.Basic;

namespace SinaOTOS.WebUI.Models.ViewModels;

public class CreateLoadingDischargeViewModel
{
    public string Method { get; set; }
    public int VesselStoppageId { get; set; }
    public List<LoadingDischargeTariffDetails> LoadingDischargeTariffDetails { get; set; }
}
