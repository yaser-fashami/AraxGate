using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using AraxGate.Core.Domain.Dtos;
using AraxGate.Core.Domain.Entities.Basic;
using AraxGate.Core.Domain.Interfaces;
using AraxGate.Utilities.Exceptions;
using AraxGate.Utilities.Pagination;
using AraxGate.Utilities;
using AraxGate.Infrastructure;

namespace AraxGate.Infra.Data.Sql.EFRepositories;

public class BasicInfoRepository : IBasicInfoRepository
{
	private readonly AraxGateDbContext _araxGateDBContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public BasicInfoRepository(AraxGateDbContext AraxGateDbContext, IHttpContextAccessor httpContextAccessor)
	{
		_araxGateDBContext = AraxGateDbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    #region	Currency
    public async Task<SqlException> CreateCurrencyAsync(Currency newCurrency)
	{
		SqlException result = new SqlException() { State = false };
		Currency currency = new()
		{
			ForeignDollerRate = newCurrency.ForeignDollerRate,
			PersianDollerRate = newCurrency.PersianDollerRate,
			Date = newCurrency.Date.ShamsiToMiladi(),
            CreatedById = _httpContextAccessor.HttpContext.User.Identity?.GetCurrentUserId(),
            CreateDate = DateTime.Now

        };
		try
		{
			await _araxGateDBContext.AddAsync(currency);
			await _araxGateDBContext.SaveChangesAsync();
			result.State = true;
		}
		catch (Exception ex)
		{
			result.State = false;
			result.Message = ex.Message;
		}

		return result;
	}

	public async Task<PagedData<Currency>> GetPaginationCurrenciesAsync(int pageNumber = 1, int pageSize = 10)
	{
		var currnecies = _araxGateDBContext.Currencies.OrderByDescending(c => c.Date).AsNoTracking();

		PagedData<Currency> result = new()
		{
			PageInfo = new()
			{
				PageNumber = pageNumber,
				PageSize = pageSize,
				TotalCount = await currnecies.CountAsync()
			},
			Data = await currnecies.ToPagination(pageNumber, pageSize)
								.ToListAsync()
		};

		return result;
	}

	#endregion

    #region Consignee
    public async Task<PagedData<Consignee>> GetPaginationConsigneesAsync(int pageNumber = 1, int pageSize = 10, string filter = "")
    {
        bool noFilter = string.IsNullOrWhiteSpace(filter);

        var consignees = _araxGateDBContext.Consignees.AsNoTracking()
                                .Where(c => noFilter
                                    || c.ConsigneeName.Contains(filter)
                                    || c.ConsigneeNameEng.Contains(filter)
                                    || c.City.Contains(filter)
                                    || c.NationalCode.Contains(filter)
                                    || c.EconomicCode.Contains(filter));

        PagedData<Consignee> result = new PagedData<Consignee>()
        {
            PageInfo = new()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = await consignees.CountAsync()
            },
            Data = await consignees.OrderBy(c => c.ConsigneeName).ToPagination(pageNumber, pageSize).ToListAsync()
        };

        return result;
    }

    public async Task<SqlException> CreateConsigneeAsync(Consignee consignee)
    {
        var result = new SqlException();
        consignee = (Consignee)Util.TrimAllStringFields(consignee);
        consignee.CreatedById = _httpContextAccessor.HttpContext.User.Identity?.GetCurrentUserId();
        consignee.CreateDate = DateTime.Now;
        try
        {
            await _araxGateDBContext.AddAsync(consignee);
            await _araxGateDBContext.SaveChangesAsync();
			result.State = true;
		}
        catch (Exception ex)
        {
            result.State = false;
            result.Message = ex.Message;
        }

        return result;
    }
	public async Task<Consignee> GetConsigneeById(ulong id)
	{
        return await _araxGateDBContext.Consignees.AsNoTracking().SingleAsync(c => c.Id == id);
	}

	public async Task<SqlException> UpdateConsigneeAsync(Consignee consignee)
	{
		var result = new SqlException();

        var newConsignee = await _araxGateDBContext.Consignees.SingleOrDefaultAsync(c => c.Id == consignee.Id);
        consignee = (Consignee)Util.TrimAllStringFields(consignee);
        if (newConsignee != null)
        {
            newConsignee.Address = consignee.Address;
            newConsignee.City = consignee.City;
            newConsignee.Description = consignee.Description;
            newConsignee.EconomicCode = consignee.EconomicCode;
            newConsignee.NationalCode = consignee.NationalCode;
            newConsignee.Email = consignee.Email;
            newConsignee.PostalCode = consignee.PostalCode;
            newConsignee.TelNo = consignee.TelNo;
            newConsignee.ModifiedById = _httpContextAccessor.HttpContext.User.Identity?.GetCurrentUserId();
            newConsignee.ModifiedDate = DateTime.Now;
		}

        try
        {
            _araxGateDBContext.Update(newConsignee);
			await _araxGateDBContext.SaveChangesAsync();
			result.State = true;

		}
		catch (Exception ex)
        {
			result.State = false;
			result.Message = ex.Message;
		}
		return result;
	}
	#endregion

	#region CommodityType
	public async Task<PagedData<CommodityType>> GetPaginationCommodityTypeAsync(int pageNumber = 1, int pageSize = 10, string filter = "")
    {
        bool noFilter = string.IsNullOrWhiteSpace(filter);

        var commodityTypes = _araxGateDBContext.CommodityTypes.AsNoTracking()
                                .Where(c => noFilter
                                    || c.CommodityTypeName.Contains(filter)
                                    || c.Description.Contains(filter));

        PagedData<CommodityType> result = new PagedData<CommodityType>()
        {
            PageInfo = new()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = await commodityTypes.CountAsync()
            },
            Data = await commodityTypes.OrderBy(c => c.CommodityTypeName).ToPagination(pageNumber, pageSize).ToListAsync()
        };

        return result;
    }

    public async Task<SqlException> CreateCommodityTypeAsync(CommodityType commodityType)
    {
        var result = new SqlException();
        commodityType = (CommodityType)Util.TrimAllStringFields(commodityType);
        try
        {
            await _araxGateDBContext.AddAsync(commodityType);
            await _araxGateDBContext.SaveChangesAsync();
            result.State = true;
        }
        catch (Exception ex)
        {
            result.State = false;
            result.Message = ex.Message;
        }

        return result;
    }

	#endregion

	#region OilTankType
	public async Task<PagedData<OilTankType>> GetPaginationOilTankTypeAsync(int pageNumber = 1, int pageSize = 10, string filter = "")
	{
		bool noFilter = string.IsNullOrWhiteSpace(filter);

		var oilTankTypes = _araxGateDBContext.OilTankTypes.AsNoTracking()
								.Where(c => noFilter
									|| c.TankName.Contains(filter)
									|| c.TankGroup.Contains(filter)
									|| c.Description.Contains(filter));

		PagedData<OilTankType> result = new PagedData<OilTankType>()
		{
			PageInfo = new()
			{
				PageNumber = pageNumber,
				PageSize = pageSize,
				TotalCount = await oilTankTypes.CountAsync()
			},
			Data = await oilTankTypes.OrderBy(c => c.TankGroup).ToPagination(pageNumber, pageSize).ToListAsync()
		};

		return result;
	}

	public async Task<SqlException> CreateOilTankTypeAsync(OilTankType oilTankType)
	{
		var result = new SqlException();
		oilTankType = (OilTankType)Util.TrimAllStringFields(oilTankType);
		try
		{
			await _araxGateDBContext.AddAsync(oilTankType);
			await _araxGateDBContext.SaveChangesAsync();
			result.State = true;
		}
		catch (Exception ex)
		{
			result.State = false;
			result.Message = ex.Message;
		}

		return result;
	}

    #endregion

    #region TruckType
    public async Task<PagedData<TruckType>> GetPaginationTruckTypeAsync(int pageNumber = 1, int pageSize = 10, string filter = "")
    {
        bool noFilter = string.IsNullOrWhiteSpace(filter);

        var truckTypes = _araxGateDBContext.TruckTypes.AsNoTracking()
                                .Where(c => noFilter
                                    || c.TruckTypeName.Contains(filter)
                                    || c.Description.Contains(filter));

        PagedData<TruckType> result = new PagedData<TruckType>()
        {
            PageInfo = new()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = await truckTypes.CountAsync()
            },
            Data = await truckTypes.OrderBy(c => c.TruckTypeName).ToPagination(pageNumber, pageSize).ToListAsync()
        };

        return result;
    }

    public async Task<SqlException> CreateTruckTypeAsync(TruckType truckType)
    {
        var result = new SqlException();
        truckType = (TruckType)Util.TrimAllStringFields(truckType);
        try
        {
            await _araxGateDBContext.AddAsync(truckType);
            await _araxGateDBContext.SaveChangesAsync();
            result.State = true;
        }
        catch (Exception ex)
        {
            result.State = false;
            result.Message = ex.Message;
        }

        return result;
    }

    #endregion

    public async Task<OilTankGateEnteranceDto> GetOilTankGateEnteranceDataAsync()
    {
        OilTankGateEnteranceDto result = new();

        result.ConsigneeList = await _araxGateDBContext.Consignees.ToListAsync();
        result.CommodityList = await _araxGateDBContext.CommodityTypes.ToListAsync();
        result.TankTypeList = await _araxGateDBContext.OilTankTypes.ToListAsync();
        result.TruckTypeList = await _araxGateDBContext.TruckTypes.ToListAsync();

        return result;
    }

}

