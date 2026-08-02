using SinaOTOS.Core.Domain.Entities;
using SinaOTOS.Core.Domain.Entities.Basic;
using SinaOTOS.Core.Domain.Entities.Operation;

namespace SinaOTOS.Core.Domain.Dtos;
public record PreLoadingDischargeInvoiceDto(
											string InvoiceNo,
											DateTime InvoiceDate,
											LoadingDischarge LoadingDiascharge,
											int LoadingDischargeTariffId,
											ShippingLineCompany ShippingLineCompany,
											byte DiscountPercent,
											ulong DiscountAmount,
											uint PerTonPrice,
											ulong LDCostR,
											double Tonage,
											ulong CraneTariff,
											ulong CraneCostR,
											ulong InventoryTariffPrice,
											ulong InventoryCostR,
											ulong TotalCostR,
											VesselStoppage VesselStoppage,
											Vessel Vessel,
											byte VatPercent,
											ulong VatCostR,
											string CurrentUser,
											string CurrentUserEmail
										  );