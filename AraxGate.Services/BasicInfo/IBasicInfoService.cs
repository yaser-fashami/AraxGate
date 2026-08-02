using Microsoft.AspNetCore.Mvc;
using SinaOTOS.Core.Domain.Entities.Basic;

namespace SinaOTOS.Services.BasicInfo;
public interface IBasicInfoService
{
    Task<List<LoadingDischargeTariffDetails>> GetLastLoadingDischargeTariffDetailAsync();

    JsonResult AddLoadingDischargeTariffDetail(List<LoadingDischargeTariffDetails> loadingDischargeTariffDetails, string description, DateTime effectiveDate);
}
