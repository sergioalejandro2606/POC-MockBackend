using System.Collections.Concurrent;
using TransferenciasMockApi.Models;
namespace TransferenciasMockApi.Services;

public sealed class MockDataStore {
    private readonly ConcurrentDictionary<string, CreateUserRequest> users = new();
    private readonly ConcurrentDictionary<string, TransferResponse> transfers = new();
    public bool CreateUser(CreateUserRequest user) => users.TryAdd(user.docNmber, user);
    public bool DeleteUser(string doc) => users.TryRemove(doc, out _);
    public void SaveTransfer(TransferResponse transfer) => transfers[transfer.transactionalId] = transfer;
    public bool TryGetTransfer(string id, out TransferResponse? transfer) => transfers.TryGetValue(id, out transfer);
}
