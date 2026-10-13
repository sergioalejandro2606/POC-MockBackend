using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransferenciasMockApi.Models;
using TransferenciasMockApi.Services;
namespace TransferenciasMockApi.Controllers;

[ApiController, Route("api/movements"), Authorize]
public sealed class MovementsController(MockDataStore store) : ControllerBase {
    [HttpGet("last")]
    public IActionResult Last([FromQuery] MovementQuery query) {
        if (!store.TryGetTransfer(query.transactionalId, out var t) || t is null)
            return NotFound(new ErrorResponse("RES_404", "Not Found", "No se encontró un movimiento con ese transactionalId."));
        var data = $"{t.transactionalId}{t.originAccount}{t.amountTrant}{t.destinyAccount}";
        return Ok(new LastMovementResponse("00", t.transactionalId, data));
    }
}
