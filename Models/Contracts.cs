using System.ComponentModel.DataAnnotations;
namespace TransferenciasMockApi.Models;

public sealed class LoginRequest {
    [Required, MinLength(1)] public string Username { get; init; } = "";
    [Required, MinLength(1)] public string Password { get; init; } = "";
}
public sealed record LoginResponse(string access_token, string token_type, int expires_in);
public sealed record ErrorResponse(string responseCode, string error, string message);
public sealed record OperationResponse(string responseCode, string additionalResponseData);
public sealed record TransferResponse(string responseCode, string transactionalId, string originAccount, string amountTrant, string destinyAccount, string date);
public sealed record LastMovementResponse(string responseCode, string transactionalId, string additionalResponseData);

public sealed class TransferRequest : IValidatableObject {
    [Range(1, int.MaxValue)] public int Iduser { get; init; }
    [Required, MinLength(1)] public string firstName { get; init; } = "";
    [Required, MinLength(1)] public string lastName { get; init; } = "";
    [Required, RegularExpression(@"^[0-9]{7,20}$")] public string originAccount { get; init; } = "";
    [Required, RegularExpression(@"^[0-9]+$")] public string amountTrant { get; init; } = "";
    public bool active { get; init; }
    [Required, MinLength(1), MaxLength(64)] public string transactionalId { get; init; } = "";
    [Required, RegularExpression(@"^[0-9]{7,20}$")] public string destinyAccount { get; init; } = "";
    [Required, MinLength(2), MaxLength(50)] public string destinyBank { get; init; } = "";
    public IEnumerable<ValidationResult> Validate(ValidationContext context) {
        if (originAccount == destinyAccount) yield return new ValidationResult("Origen y destino no pueden ser la misma cuenta.", new[] { nameof(originAccount), nameof(destinyAccount) });
        if (decimal.TryParse(amountTrant, out var amount) && amount <= 0) yield return new ValidationResult("amountTrant debe ser mayor que cero.", new[] { nameof(amountTrant) });
    }
}
public sealed class MovementQuery {
    [Required, RegularExpression(@"^[0-9]+$")] public string iduser { get; init; } = "";
    [Required, MinLength(1)] public string transactionalId { get; init; } = "";
}
public sealed class CreateUserRequest {
    [Required, MinLength(1)] public string firstName { get; init; } = "";
    [Required, MinLength(1)] public string lastName { get; init; } = "";
    [Required, RegularExpression("^(CC|CE|NIT|PAS)$")] public string doctype { get; init; } = "";
    [Required, RegularExpression(@"^[0-9]{5,15}$")] public string docNmber { get; init; } = "";
    [Required, EmailAddress] public string email { get; init; } = "";
    [Required, RegularExpression(@"^[0-9]{10}$")] public string phoneNumber { get; init; } = "";
}
public sealed class DeleteUserRequest {
    [Range(1, int.MaxValue)] public int Iduser { get; init; }
    [Required, MinLength(1)] public string firstName { get; init; } = "";
    [Required, MinLength(1)] public string lastName { get; init; } = "";
    [Required, RegularExpression("^(CC|CE|NIT|PAS)$")] public string doctype { get; init; } = "";
    [Required, RegularExpression(@"^[0-9]{5,15}$")] public string docNmber { get; init; } = "";
}
