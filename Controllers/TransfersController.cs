using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransferenciasMockApi.Models;
using TransferenciasMockApi.Services;
namespace TransferenciasMockApi.Controllers;

[ApiController, Route("api/transfers"), Authorize]
public sealed class TransfersController(MockDataStore store) : ControllerBase {
    [HttpPost]
    public IActionResult Create(TransferRequest request) {
        if (!decimal.TryParse(request.amountTrant, out var amount) || amount <= 0)
            return UnprocessableEntity(new ErrorResponse("VAL_422", "Validation Error", "amountTrant debe ser mayor que cero."));
        var result = new TransferResponse("00", request.transactionalId, request.originAccount, request.amountTrant, request.destinyAccount, DateTime.UtcNow.ToString("ddMMyyyy"));
        store.SaveTransfer(result);
        return Ok(result);
    }
}
