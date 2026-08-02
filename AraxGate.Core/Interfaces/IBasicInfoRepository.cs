using AraxGate.Core.Domain.Dtos;
using AraxGate.Core.Domain.Entities.Basic;
using AraxGate.Utilities.Exceptions;
using AraxGate.Utilities.Pagination;

namespace AraxGate.Core.Domain.Interfaces;

public interface IBasicInfoRepository
{
    Task<PagedData<Currency>> GetPaginationCurrenciesAsync(int pageNumber = 1, int pageSize = 10);
    Task<SqlException> CreateCurrencyAsync(Currency newCurrency);

    #region Consignee
    Task<PagedData<Consignee>> GetPaginationConsigneesAsync(int pageNumber = 1, int pageSize = 10, string filter = "");
    Task<SqlException> CreateConsigneeAsync(Consignee consignee);
    Task<Consignee> GetConsigneeById(ulong id);
    Task<SqlException> UpdateConsigneeAsync(Consignee consignee);
    #endregion

    #region CommodityType
    Task<PagedData<CommodityType>> GetPaginationCommodityTypeAsync(int pageNumber = 1, int pageSize = 10, string filter = "");
    Task<SqlException> CreateCommodityTypeAsync(CommodityType commodityType);

	#endregion

	#region OilTankType
	Task<PagedData<OilTankType>> GetPaginationOilTankTypeAsync(int pageNumber = 1, int pageSize = 10, string filter = "");
	Task<SqlException> CreateOilTankTypeAsync(OilTankType oilTankType);
    #endregion

    #region TruckType
    Task<PagedData<TruckType>> GetPaginationTruckTypeAsync(int pageNumber = 1, int pageSize = 10, string filter = "");
    Task<SqlException> CreateTruckTypeAsync(TruckType truckType);

    #endregion

    Task<OilTankGateEnteranceDto> GetOilTankGateEnteranceDataAsync();
}

