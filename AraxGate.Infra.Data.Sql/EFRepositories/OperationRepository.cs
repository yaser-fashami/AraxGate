using Microsoft.EntityFrameworkCore;
using AraxGate.Core.Domain.Entities.Operation;
using AraxGate.Core.Domain.Interfaces;
using AraxGate.Utilities;
using Microsoft.AspNetCore.Http;
using AraxGate.Utilities.Exceptions;
using AraxGate.Core.Domain.Entities.Basic;
using AraxGate.Infrastructure;

namespace AraxGate.Infra.Data.Sql.EFRepositories;
public class OperationRepository : IOperationRepository
{
    private readonly AraxGateDbContext _araxGateDBcontext;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public OperationRepository(AraxGateDbContext araxGateDBcontext, IHttpContextAccessor httpContextAccessor)
    {
        _araxGateDBcontext = araxGateDBcontext;
        _httpContextAccessor = httpContextAccessor;
    }

    #region OilTankGate
    public async Task<uint> GetBaskoolOperationAsync(string baskool)
    {
        BaskoolType baskoolType = new();
        switch (baskool)
        {
            case "a1":
                baskoolType = BaskoolType.Gate_A1;
                break;
            case "a2":
                baskoolType = BaskoolType.Gate_A2;
                break;
            case "b1":
                baskoolType = BaskoolType.Gate_B1;
                break;
            case "b2":
                baskoolType = BaskoolType.Gate_B2;
                break;
            default:
                break;
        }
        var result = await _araxGateDBcontext.BaskoolOperations.Where(c => c.BaskoolType == baskoolType).OrderBy(c => c.Id).Select(c => c.Weight).LastOrDefaultAsync();
        return uint.Parse(result.ToString());
    }
    public async Task<SqlException> SaveGateEnteranceAsync(GateEntrance gateEntrance, byte[]? gateInImage)
    {
        SqlException result = new SqlException();
        var getTrucksInTheYardNow = await GetTrucksInTheYardNowAsync();
        if (getTrucksInTheYardNow.Exists(c=>c.TruckNo == gateEntrance.TruckNo))
        {
            result.State = false;
            result.Message = "This vehicle has already entered the area and has not exited. Please enter its exit first and then try to enter again.";
        }
        else
        {
            try
            {
                gateEntrance = (GateEntrance)Util.TrimAllStringFields(gateEntrance);
                var removeBaskoolOperation = await _araxGateDBcontext.BaskoolOperations.Where(c=>c.BaskoolType == gateEntrance.Baskool).ToArrayAsync();
                if (gateInImage != null)
                {
                    VehicleImage image = new()
                    {
                        ImageData = gateInImage,
                    };
                    await _araxGateDBcontext.VehicleImages.AddAsync(image);
                    gateEntrance.GateInFrontPlateVehicleImage = image;
                }
                await _araxGateDBcontext.GateEntrances.AddAsync(gateEntrance);
                _araxGateDBcontext.BaskoolOperations.RemoveRange(removeBaskoolOperation);
                await _araxGateDBcontext.SaveChangesAsync();
                result.State = true;
            }
            catch (Exception ex)
            {
                result.State = false;
                result.Message = ex.Message;
            }
        }

        return result;
    }
    public async Task<List<GateEntrance>> GetTrucksInTheYardAsync()
    {
		return await _araxGateDBcontext.GateEntrances.Where(c => c.GateOutDate == null || c.GateOutDate >= DateTime.Now.AddDays(-1)).ToListAsync();
	}

    public async Task<List<GateEntrance>> GetTrucksEnterencedByDateAsync(DateTime date)
    {
		return await _araxGateDBcontext.GateEntrances.Where(c => c.GateInDate >= date && c.GateInDate < date.AddDays(1)).ToListAsync();
	}

    public async Task<SqlException> GateExitAsync(GateEntrance gateEntrance, byte[]? gateOutFrontPlatePic)
    {
        var result = new SqlException() { State = false };

        GateEntrance updateGateEntrance = await _araxGateDBcontext.GateEntrances.FindAsync(gateEntrance.Id);
        updateGateEntrance.DriverName = gateEntrance.DriverName;
        updateGateEntrance.GateOutDate = gateEntrance.GateOutDate;
        updateGateEntrance.GateOutOperatorById = gateEntrance.GateOutOperatorById;
        updateGateEntrance.BaskoolOut = gateEntrance.BaskoolOut;
        updateGateEntrance.GateOutWeight = gateEntrance.GateOutWeight;

        try
        {
            var removeBaskoolOperation = await _araxGateDBcontext.BaskoolOperations.Where(c => c.BaskoolType == gateEntrance.BaskoolOut).ToArrayAsync();
            if (gateOutFrontPlatePic != null)
            {
                var image = new VehicleImage() { ImageData = gateOutFrontPlatePic };
                await _araxGateDBcontext.VehicleImages.AddAsync(image);
                updateGateEntrance.GateOutFrontPlateVehicleImage = image;
            }
            _araxGateDBcontext.GateEntrances.Update(updateGateEntrance);
            _araxGateDBcontext.BaskoolOperations.RemoveRange(removeBaskoolOperation);
            await _araxGateDBcontext.SaveChangesAsync();
            result.State = true;
        }
        catch (Exception ex)
        {
            result.Message = ex.Message;
            result.State = false;
        }

        return result;
    }
    public GateEntrance GetGateEntrance(long id)
    {
        var result =  _araxGateDBcontext.GateEntrances
            .Include(c => c.CommodityType)
            .Include(c => c.OilTankType)
            .Include(c => c.Consignee)
            .Include(c => c.TruckType)
            .Include(c => c.GateInOperator)
            .Include(c => c.GateOutOperator)
            .Where(c => c.Id == id)
            .First();

        result.GateInOperator =  _araxGateDBcontext.Users.Where(c => c.Id == result.GateInOperatorById).First();
        result.GateOutOperator =  _araxGateDBcontext.Users.Where(c => c.Id == result.GateOutOperatorById).First();
        return result;
    }
    #endregion

    #region Private Methods
    private async Task<List<GateEntrance>> GetTrucksInTheYardNowAsync()
    {
        return await _araxGateDBcontext.GateEntrances.Where(c => c.GateOutDate == null).ToListAsync();
    }


    #endregion

}
