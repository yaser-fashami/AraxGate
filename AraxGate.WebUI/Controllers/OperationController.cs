using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AraxGate.Core.Domain.Entities.Operation;
using AraxGate.Core.Domain.Interfaces;
using AraxGate.Application.Operation;
using SmartBreadcrumbs.Attributes;
using System.Security.Claims;
using System.Text.Json;
using AraxGate.Core.Domain.Entities.Basic;
using AraxGate.Utilities.Exceptions;

namespace AraxGate.WebUI.Controllers;

[Authorize]
public class OperationController : Controller
{
    private readonly IBasicInfoRepository _basicInfoRepository;
    private readonly IOperationRepository _operationRepository;
    private readonly IOilTankGateService _oilTankGateService;
    private readonly HttpClient _httpClient;

    public OperationController(IOperationRepository operationRepository, IBasicInfoRepository basicRepository, IOilTankGateService oilTankGateService, HttpClient httpClient)
    {
        _operationRepository = operationRepository;
        _basicInfoRepository = basicRepository;
        _oilTankGateService = oilTankGateService;
        _httpClient = httpClient;
    }

    #region OilTankGate

    [HttpGet]
    [Authorize(Roles = "admin, gateoperator")]
    //[Breadcrumb("OilTankGateOperation", FromAction = "List", FromController = typeof(DashboardController))]
    public IActionResult OilTankGateOperation()
    {
		var roles = User.Claims
		                    .Where(c => c.Type == ClaimTypes.Role)
		                    .Select(c => c.Value)
		                    .ToList();
        if (roles.Contains("admin"))
        {
            ViewData["role"] = "admin";
        }
        
        //var model = _operationRepository.GetTrucksInTheYardAsync().Result;

        ViewData["Title"] = "OilTank Gate Operation";
        ViewData["ActiveLink"] = "oilTankGateOperation";
        //return View(model);
        return View();
    }

    public async Task<IActionResult> TruckEnterenced(int year, int month, int day)
    {
        var date = new DateTime(year, month, day);
        var model = await _operationRepository.GetTrucksEnterencedByDateAsync(date);
        return PartialView("_EnterancesTruck", model);
    }

    public JsonResult GetBaskoolData(string baskool)
    {
        return Json(_operationRepository.GetBaskoolOperationAsync(baskool).Result);
    }

    public JsonResult GetOilTankGateEnterranceData()
    {
        return Json(_basicInfoRepository.GetOilTankGateEnteranceDataAsync().Result);
    }

    [HttpPost]
    public BLMessage SaveGateEntrance
    (
        string baskool, 
        uint weight, 
        uint consigneeId, 
        ushort tankTypeId, 
        ushort commodityId, 
        ushort trucktypeId, 
        string plateType, 
        string twoNo, 
        string alphabet, 
        string threeNo, 
        string provience,
        string otherPlateNo,
        string customPermission,
        byte[]? gateInFrontPlatePic,
        string? description
    )
    {
        BaskoolType baskoolType = new BaskoolType();
        PlateType plateTypeEnum = new PlateType();
        switch (baskool)
        {
            case "Baskool A1":
                baskoolType = BaskoolType.Gate_A1;
                break;
            case "Baskool A2":
                baskoolType = BaskoolType.Gate_A2;
                break;
            case "Baskool B1":
                baskoolType = BaskoolType.Gate_B1;
                break;
            case "Baskool B2":
                baskoolType = BaskoolType.Gate_B2;
                break;
        }
        switch (plateType)
        {
            case "iran":
                plateTypeEnum = PlateType.Iran;
                break;
            case "iraq":
                plateTypeEnum = PlateType.Iraq;
                break;
            case "afghan":
                plateTypeEnum = PlateType.Afghan;
                break;
        }

        GateEntrance gateEntrance = new()
        {
            Baskool = baskoolType,
            GateInWeight = weight,
            ConsigneeId = consigneeId,
            OilTankTypeId = tankTypeId,
            CommodityTypeId = commodityId,
            TruckTypeId = trucktypeId,
            PlateType = plateTypeEnum,
            TruckNo = (twoNo + alphabet + threeNo + "-" + provience),
            TruckNoletter = otherPlateNo,
            CustomPermissionNo = customPermission,
            Description = description
        };
        
        var result = _oilTankGateService.GateInOperation(gateEntrance, gateInFrontPlatePic);
        return result;
    }

    [HttpPost]
    public bool GateOut(long id, string driverName, string baskool, uint weightOut, byte[]? gateOutFrontPlatePic, string? description)
    {
        return _oilTankGateService.GateOutOperation(id, driverName, baskool, weightOut, gateOutFrontPlatePic, description).State;
    }

    public IActionResult GetPrint(long id)
    {
        var model = _operationRepository.GetGateEntrance(id);
        return View("PrintGateOut", model);
    }

    public async Task<Tuple<string,string,string,string,string>> GetPlateNoFromCam(string cameraId)
    {
        HttpResponseMessage response;
        _httpClient.Timeout = TimeSpan.FromSeconds(100);

        try
        {
            response = await _httpClient.GetAsync("http://172.19.60.20:9002/ocr_camera?cam_id=" + cameraId);
            response.EnsureSuccessStatusCode();

        }
        catch (Exception ex)
        {

            throw;
        }
        var content = await response.Content.ReadAsStringAsync();
        string first_part = string.Empty;
        string persian_letter = string.Empty;
        string second_part = string.Empty;
        string city_code = string.Empty;
        string crop_base64 = string.Empty;

        JsonDocument document = JsonDocument.Parse(content);

        foreach (var item in document.RootElement.GetProperty("result").GetProperty("plates").EnumerateArray())
        {
            first_part = item.GetProperty("plate_text").GetProperty("first_part").GetString();
            persian_letter = item.GetProperty("plate_text").GetProperty("persian_letter").GetString();
            second_part = item.GetProperty("plate_text").GetProperty("second_part").GetString();
            city_code = item.GetProperty("plate_text").GetProperty("city_code").GetString();
            crop_base64 = item.GetProperty("crop_base64").GetString();
        }

        Tuple<string, string, string, string, string> result = new(first_part, persian_letter, second_part, city_code, crop_base64);
     
        return result;
    }
    #endregion

}
