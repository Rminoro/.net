using Microsoft.AspNetCore.Mvc;
using Sprint4dotnet.Models;
using Sprint4dotnet.Services; 

namespace Sprint4dotnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Client client)
        {
            if (client == null || string.IsNullOrEmpty(client.Email) || string.IsNullOrEmpty(client.Password) || string.IsNullOrEmpty(client.Name))
            {
                return BadRequest("Invalid client data.");
            }

            await _clientService.RegisterUser(client.Email, client.Password, client.Name);
            return Ok("Client registered successfully.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, [FromBody] Client client)
        {
            if (id != client.Id)
                return BadRequest();

            await _clientService.UpdateClientAsync(client);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            await _clientService.DeleteClientAsync(id);
            return NoContent();
        }

    }
}

