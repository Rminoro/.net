namespace Sprint4dotnet.Services;

using Sprint4dotnet.Models;

public interface IClientService
{
    Task RegisterUser(string email, string password, string name);
    Task UpdateClientAsync(Client client);
    Task DeleteClientAsync(int id); 
}
