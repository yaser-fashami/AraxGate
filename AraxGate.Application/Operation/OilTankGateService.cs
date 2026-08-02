using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using AraxGate.Core.Domain.Entities;
using AraxGate.Core.Domain.Entities.Basic;
using AraxGate.Core.Domain.Entities.Operation;
using AraxGate.Core.Domain.Interfaces;
using AraxGate.Utilities.Exceptions;
using AraxGate.Utilities;
using AraxGate.Infrastructure;
using System.Text;

namespace AraxGate.Application.Operation;

public class OilTankGateService : IOilTankGateService
{
    private readonly AraxGateDbContext _araxGateDbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IOperationRepository _operationRepository;


    public OilTankGateService(AraxGateDbContext araxGateDbContext, IHttpContextAccessor httpContextAccessor, IOperationRepository operationRepository)
    {
        _araxGateDbContext = araxGateDbContext;
        _httpContextAccessor = httpContextAccessor;
        _operationRepository = operationRepository;
    }

    public BLMessage GateInOperation(GateEntrance gateEntrance, byte[]? gateInImage)
    {
        BLMessage result = new BLMessage();
        gateEntrance.GateInDate = DateTime.Now;
        gateEntrance.GateInOperatorById = _httpContextAccessor.HttpContext.User.Identity?.GetCurrentUserId();
        gateEntrance.GateEntranceNo = GenerateGateEntranceNumber();
        if (gateEntrance.PlateType != PlateType.Iran)
        {
            gateEntrance.TruckNo = "other";
        }
        var res = _operationRepository.SaveGateEnteranceAsync(gateEntrance, gateInImage).Result;
        result.Message = res.Message;
        result.State = res.State;
        return result;
    }

    public BLMessage GateOutOperation(long id, string driverName, string baskool, uint weightOut, byte[] gateOutFrontPlatePic, string? description)
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

        GateEntrance gateEntrance = new()
        {
            Id = id,
            DriverName = driverName,
            GateOutDate = DateTime.Now,
            GateOutOperatorById = _httpContextAccessor.HttpContext.User.Identity?.GetCurrentUserId(),
            BaskoolOut = baskoolType,
            GateOutWeight = weightOut,
            Description = description
        };

        return _operationRepository.GateExitAsync(gateEntrance, gateOutFrontPlatePic).Result;
    }

    #region private Methods
    private string GenerateGateEntranceNumber()
    {
        DateTime date = DateTime.Now;
        var shamsiDate = date.MiladiToPersianDate();
        var gateEntranceNo = new StringBuilder();
        gateEntranceNo.Append(shamsiDate.year);
        gateEntranceNo.Append(shamsiDate.month.ToString().Length == 1 ? "0" + shamsiDate.month : shamsiDate.month);
        var lastGateEntrance = _araxGateDbContext.GateEntrances.OrderBy(c => c.Id).LastOrDefaultAsync().Result;
        var lastGateEntranceNo = lastGateEntrance != null ? (int.Parse(lastGateEntrance.GateEntranceNo.Substring(6, lastGateEntrance.GateEntranceNo.Length - 6)) + 1).ToString() : "00001";
        switch (lastGateEntranceNo.Length)
        {
            case 1:
                lastGateEntranceNo = "0000" + lastGateEntranceNo;
                break;
            case 2:
                lastGateEntranceNo = "000" + lastGateEntranceNo;
                break;
            case 3:
                lastGateEntranceNo = "00" + lastGateEntranceNo;
                break;
            case 4:
                lastGateEntranceNo = "0" + lastGateEntranceNo;
                break;
            case 5:
                break;
            default:
                break;
        }
        if (shamsiDate.month > lastGateEntrance?.GateInDate.MiladiToPersianDate().month)
        {
            lastGateEntranceNo = "00001";
        }
        gateEntranceNo.Append(lastGateEntranceNo);

        return gateEntranceNo.ToString();
    }

    #endregion

}
