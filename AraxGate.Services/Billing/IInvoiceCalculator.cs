
using SinaOTOS.Core.Domain.Dtos;
using SinaOTOS.Framework.Exceptions;

namespace SinaOTOS.Services.Billing;

public interface IInvoiceCalculator
{
    Task<PreInvoiceDto> CalculateAsync(ulong voyageId, IEnumerable<ulong> vesselStoppages);
    Task<BLMessage> Invoicing(PreInvoiceDto preInvoice);
    Task<PreLoadingDischargeInvoiceDto> CalculateLoadingDischargeInvoiceAsync(LoadingDischargeInvoiceDto loadingDischargeDto);
    Task<BLMessage> LoadingDischargeInvoicing(PreLoadingDischargeInvoiceDto preLoadingDischargeInvoiceDto);
}
