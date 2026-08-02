using AraxGate.Core.Domain.Entities.Operation;
using AraxGate.Utilities.Exceptions;

namespace AraxGate.Core.Domain.Interfaces;

public interface IOperationRepository
{
    Task<uint> GetBaskoolOperationAsync(string baskool);
    Task<List<GateEntrance>> GetTrucksInTheYardAsync();
    Task<List<GateEntrance>> GetTrucksEnterencedByDateAsync(DateTime date);
    Task<SqlException> SaveGateEnteranceAsync(GateEntrance gateEnterance, byte[] gateInImage);
    Task<SqlException> GateExitAsync(GateEntrance gateEntrance, byte[]? gateOutFrontPlatePic);
    GateEntrance GetGateEntrance(long id);


}
