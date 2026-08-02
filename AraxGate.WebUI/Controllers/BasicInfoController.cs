using Microsoft.AspNetCore.Mvc;
using AraxGate.Core.Domain.Entities.Basic;
using AraxGate.Core.Domain.Interfaces;
using AraxGate.WebUI.Models;
using AraxGate.WebUI.Models.ViewModels;
using SmartBreadcrumbs.Attributes;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using AraxGate.Utilities.Exceptions;

namespace AraxGate.WebUI.Controllers
{
    [Authorize]
	public class BasicInfoController : Controller
    {
        private readonly ILogger<BasicInfoController> _logger;
        private readonly IBasicInfoRepository _basicInfoRepository;
        private readonly IAppVersionService _appVersionService;

        public BasicInfoController(ILogger<BasicInfoController> logger,
			IBasicInfoRepository basicIOnfoRepository,
			IAppVersionService appVersionService)
		{
			_logger = logger;
			_basicInfoRepository = basicIOnfoRepository;
            _appVersionService = appVersionService;
        }

        #region Currencies
        [Breadcrumb("Currencies", FromAction = "List", FromController = typeof(DashboardController))]
        public IActionResult CurrencyList(int pageNumber = 1, int pageCount = 10)
		{
			var model = _basicInfoRepository.GetPaginationCurrenciesAsync(pageNumber, pageCount).Result;
			model.PageInfo.Title = "Currencies";
			model.PageInfo.PageName = "CurrencyList";
            ViewData["ActiveLink"] = "currency";

			return View(model);
		}

        [Authorize(Roles = "admin")]
        [Breadcrumb("CreateCurrencyRate", FromAction = "CurrencyList", FromController = typeof(BasicInfoController))]
        public IActionResult CreateCurrency()
		{
            ViewData["ActiveLink"] = "currency";
            return View();
		}

		[HttpPost]
        [Authorize(Roles = "admin")]
        [Breadcrumb("CreateCurrencyRate", FromAction = "CurrencyList", FromController = typeof(BasicInfoController))]
        public IActionResult CreateCurrency(Currency newCurrency)
		{
			if (ModelState.IsValid)
			{
				var result = _basicInfoRepository.CreateCurrencyAsync(newCurrency).Result;
				if (result.State)
				{
					return RedirectToAction("CurrencyList");
				}
				else
				{
					ModelState.AddModelError("", result.Message ?? string.Empty);
				}

			}
			ViewData["ActiveLink"] = "currency";
            return View();
		}
        #endregion

        #region Consignee
        [Breadcrumb("Consignees", FromAction = "List", FromController = typeof(DashboardController))]
        public async Task<IActionResult> ConsigneeList(int pageNumber = 1, int pageCount = 10, string filter = "")
        {
            var model = await _basicInfoRepository.GetPaginationConsigneesAsync(pageNumber, pageCount, filter);
            model.PageInfo.Title = "Consignee List";
            model.PageInfo.Filter = filter;
            model.PageInfo.PageName = "ConsigneeList";
            ViewData["ActiveLink"] = "consignee";
            return View(model);
        }

        [Authorize(Roles = "admin, gateoperator")]
        [Breadcrumb("CreateConsignee", FromAction = "ConsigneeList", FromController = typeof(BasicInfoController))]
        public async Task<IActionResult> CreateConsignee()
        {
            ViewData["ActiveLink"] = "consignee";
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin, gateoperator")]
        [Breadcrumb("CreateConsignee", FromAction = "ConsigneeList", FromController = typeof(BasicInfoController))]
        public async Task<IActionResult> CreateConsignee(Consignee input, IFormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
				input.TelNo = EditTels(input.TelNo, formCollection);

				var result = await _basicInfoRepository.CreateConsigneeAsync(input);
                if (result.State)
                {
                    return RedirectToAction("ConsigneeList");
                }
                else
                {
                    ModelState.AddModelError("", result.Message ?? string.Empty);
                }
            }

            ViewData["ActiveLink"] = "consignee";
            return View(input);
        }

		[Authorize(Roles = "admin")]
		[Breadcrumb("EditConsignee", FromAction = "ConsigneeList", FromController = typeof(BasicInfoController))]
		public async Task<IActionResult> EditConsignee(ulong consigneeId)
		{
			ViewData["ActiveLink"] = "consignee";
			var model = await _basicInfoRepository.GetConsigneeById(consigneeId);
			return View(model);
		}

		[HttpPost]
		[Authorize(Roles = "admin")]
		[Breadcrumb("EditConsignee", FromAction = "ConsigneeList", FromController = typeof(BasicInfoController))]
		public async Task<IActionResult> EditConsignee(Consignee consignee, IFormCollection formCollection)
		{
			if (ModelState.IsValid)
			{
				consignee.TelNo = EditTels(consignee.TelNo, formCollection);

				var result = await _basicInfoRepository.UpdateConsigneeAsync(consignee);
				if (result.State)
				{
					return RedirectToAction("ConsigneeList");
				}
				else
				{
					ModelState.AddModelError("", result.Message ?? string.Empty);
				}
			}
			var model = await _basicInfoRepository.GetConsigneeById(consignee.Id);
			return View(model);
		}
		#endregion

		#region CommodityType
		[Breadcrumb("CommodityTypes", FromAction = "List", FromController = typeof(DashboardController))]
        public async Task<IActionResult> CommodityTypeList(int pageNumber = 1, int pageCount = 10, string filter = "")
        {
            var model = await _basicInfoRepository.GetPaginationCommodityTypeAsync(pageNumber, pageCount, filter);
            model.PageInfo.Title = "CommodityType List";
            model.PageInfo.Filter = filter;
            model.PageInfo.PageName = "CommodityTypeList";
            ViewData["ActiveLink"] = "commodityType";
            return View(model);
        }

        [Authorize(Roles = "admin, gateoperator")]
        [Breadcrumb("CreateCommodityType", FromAction = "CommodityTypeList", FromController = typeof(BasicInfoController))]
        public async Task<IActionResult> CreateCommodityType()
        {
            ViewData["ActiveLink"] = "commodityType";
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin, gateoperator")]
        [Breadcrumb("CreateCommodityType", FromAction = "CommodityTypeList", FromController = typeof(BasicInfoController))]
        public async Task<IActionResult> CreateCommodityType(CommodityType input, IFormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                var result = await _basicInfoRepository.CreateCommodityTypeAsync(input);
                if (result.State)
                {
                    return RedirectToAction("CommodityTypeList");
                }
                else
                {
                    ModelState.AddModelError("", result.Message ?? string.Empty);
                }
            }

            ViewData["ActiveLink"] = "commodityType";
            return View(input);
        }
		#endregion

		#region OilTankType
		[Breadcrumb("OilTankTypes", FromAction = "List", FromController = typeof(DashboardController))]
		public async Task<IActionResult> OilTankTypeList(int pageNumber = 1, int pageCount = 10, string filter = "")
		{
			var model = await _basicInfoRepository.GetPaginationOilTankTypeAsync(pageNumber, pageCount, filter);
			model.PageInfo.Title = "OilTankType List";
			model.PageInfo.Filter = filter;
			model.PageInfo.PageName = "OilTankTypeList";
			ViewData["ActiveLink"] = "oilTankType";
			return View(model);
		}

		[Authorize(Roles = "admin, gateoperator")]
		[Breadcrumb("CreateOilTankType", FromAction = "OilTankTypeList", FromController = typeof(BasicInfoController))]
		public async Task<IActionResult> CreateOilTankType()
		{
			ViewData["ActiveLink"] = "oilTankType";
			return View();
		}

		[HttpPost]
		[Authorize(Roles = "admin, gateoperator")]
		[Breadcrumb("CreateOilTankType", FromAction = "OilTankTypeList", FromController = typeof(BasicInfoController))]
		public async Task<IActionResult> CreateOilTankType(OilTankType input, IFormCollection formCollection)
		{
			if (ModelState.IsValid)
			{
				var result = await _basicInfoRepository.CreateOilTankTypeAsync(input);
				if (result.State)
				{
					return RedirectToAction("OilTankTypeList");
				}
				else
				{
					ModelState.AddModelError("", result.Message ?? string.Empty);
				}
			}

			ViewData["ActiveLink"] = "oilTankType";
			return View(input);
		}

        #endregion

        #region TruckType
        [Breadcrumb("TruckTypes", FromAction = "List", FromController = typeof(DashboardController))]
        public async Task<IActionResult> TruckTypeList(int pageNumber = 1, int pageCount = 10, string filter = "")
        {
            var model = await _basicInfoRepository.GetPaginationTruckTypeAsync(pageNumber, pageCount, filter);
            model.PageInfo.Title = "TruckType List";
            model.PageInfo.Filter = filter;
            model.PageInfo.PageName = "TruckTypeList";
            ViewData["ActiveLink"] = "truckType";
            return View(model);
        }

        [Authorize(Roles = "admin, gateoperator")]
        [Breadcrumb("CreateTruckType", FromAction = "TruckTypeList", FromController = typeof(BasicInfoController))]
        public async Task<IActionResult> CreateTruckType()
        {
            ViewData["ActiveLink"] = "truckType";
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin, gateoperator")]
        [Breadcrumb("CreateTruckType", FromAction = "TruckTypeList", FromController = typeof(BasicInfoController))]
        public async Task<IActionResult> CreateTruckType(TruckType input, IFormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                var result = await _basicInfoRepository.CreateTruckTypeAsync(input);
                if (result.State)
                {
                    return RedirectToAction("TruckTypeList");
                }
                else
                {
                    ModelState.AddModelError("", result.Message ?? string.Empty);
                }
            }

            ViewData["ActiveLink"] = "truckType";
            return View(input);
        }

        #endregion

        #region Settings
        [Authorize(Roles = "admin")]
        [Breadcrumb("Settings", FromAction = "List", FromController = typeof(DashboardController))]
        public IActionResult Settings()
		{
			return View();
		}
		#endregion

		private string EditTels(string input, IFormCollection formCollection)
		{
			foreach (var item in formCollection)
			{
				if (item.Key.Contains("tel"))
				{
					input += item.Value + ',';
				}
			}
			do
			{
				input = input?.Substring(0, input.Length - 1);
			} while (input.EndsWith(','));
			return input;
		}


		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}