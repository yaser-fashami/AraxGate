using Microsoft.AspNetCore.Http;
using SinaOTOS.Core.Domain.Entities.Operation;
using SinaOTOS.Framework.Exceptions;
using SinaOTOS.Framework.Pagination;

namespace SinaOTOS.Services.Operation;

public interface IVesselStoppageService
{
    BLMessage AddVesselStoppage(VesselStoppage v, IFormCollection formCollection);
    BLMessage UpdateVesselStoppage(VesselStoppage v, IFormCollection formCollection);
}