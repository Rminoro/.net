using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Sprint4dotnet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClimaController : ControllerBase
    {
        [HttpGet("clima/{cidade}")]
        public async Task<IActionResult> ObterClima(string cidade, [FromServices] IHttpClientFactory httpClientFactory)
        {
            var client = httpClientFactory.CreateClient("openweather");
            var apiKey = "839ba1cc9d18faa2d677e6dbd7804c05"; 
            var response = await client.GetAsync($"weather?q={cidade}&appid={apiKey}");

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest("Cidade não encontrada.");
            }

            var clima = await response.Content.ReadFromJsonAsync<object>();
            return Ok(clima);
        }
    }
}
