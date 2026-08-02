using AraxGate.Core.Domain.Entities.Operation;
using AraxGate.Utilities.Exceptions;

namespace AraxGate.Application.Operation;

public interface IOilTankGateService
{
    BLMessage GateInOperation(GateEntrance gateEntrance, byte[] gateInImage);
    BLMessage GateOutOperation(long id, string driverName, string baskool, uint weightOut, byte[] gateOutFrontPlatePic, string? description);
}
