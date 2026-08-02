using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SinaOTOS.Core.Domain.Entities.Basic;
using SinaOTOS.Core.Domain.Interfaces;
using SinaOTOS.Infra.Data.Sql.EFRepositories;
using SinaOTOS.Infrastructure;

namespace SinaOTOS.Services.BasicInfo;
public class BasicInfoService : IBasicInfoService
{
    private readonly SinaOTOSDbContext _SinaOTOSDbContext;
    private readonly IBasicInfoRepository _basicInfoRepository;

    public BasicInfoService(SinaOTOSDbContext SinaOTOSDbContext, IBasicInfoRepository basicInfoRepository)
    {
        _SinaOTOSDbContext = SinaOTOSDbContext;
        _basicInfoRepository = basicInfoRepository;
    }

    public async Task<List<LoadingDischargeTariffDetails>> GetLastLoadingDischargeTariffDetailAsync()
    {
        var lastLoadingDischargeTariffId = await _SinaOTOSDbContext.LoadingDischargeTariffs.OrderByDescending(x => x.EffectiveDate).Select(c => c.Id).FirstAsync();

        return await _SinaOTOSDbContext.LoadingDischargeTariffDetails
                            .Include(c => c.LoadingDischargeTariff)
                            .Where(c => c.LoadingDischargeTariffId == lastLoadingDischargeTariffId)
                            .ToListAsync();


    }

    public JsonResult AddLoadingDischargeTariffDetail(List<LoadingDischargeTariffDetails> loadingDischargeTariffDetails, string description, DateTime effectiveDate)
    {
        using var transaction = _SinaOTOSDbContext.Database.BeginTransaction();

        var loadingDischargeTariff = _basicInfoRepository.CreateLoadingDischargeTariffAsync(new LoadingDischargeTariff { Description = description, EffectiveDate = effectiveDate }).Result;
        if (!loadingDischargeTariff.State)
        {
            transaction.Rollback();
            return new JsonResult(loadingDischargeTariff.Message);
        }
        else
        {
            loadingDischargeTariffDetails.ForEach(c => c.LoadingDischargeTariffId = (int)loadingDischargeTariff.sqlResult);

            var result = _basicInfoRepository.CreateLoadingDischargeTariffDetailsAsync(loadingDischargeTariffDetails).Result;
            if (!result.State)
            {
                transaction.Rollback();
                return new JsonResult(result.Message);
            }
        }
        transaction.Commit();
        return new JsonResult(loadingDischargeTariff.sqlResult);

    }
}
