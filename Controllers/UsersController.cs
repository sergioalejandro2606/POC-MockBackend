using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransferenciasMockApi.Models;
using TransferenciasMockApi.Services;
namespace TransferenciasMockApi.Controllers;

[ApiController, Route("api/users"), Authorize]
public sealed class UsersController(MockDataStore store) : ControllerBase {
    [HttpPut]
    public IActionResult Create(CreateUserRequest request) {
        if (!store.CreateUser(request)) return Conflict(new ErrorResponse("USR_409", "Conflict", "Ya existe un usuario con ese documento."));
        return Ok(new OperationResponse("00", "Successful process"));
    }
    [HttpDelete]
    public IActionResult Delete(DeleteUserRequest request) {
        if (!store.DeleteUser(request.docNmber)) return NotFound(new ErrorResponse("USR_404", "Not Found", "No existe un usuario con ese documento."));
        return Ok(new OperationResponse("00", "Successful process"));
    }
}
