using System.Text;
using System.Text.Json;
using PlatformService.Dtos;

namespace PlatformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration _config )
        {
            _httpClient = httpClient;
        }
        public async Task SendPlatformToCommand(PlatformReadDto platform)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(platform),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync("http://localhost:5197/api/c/platforms", httpContent);

            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Synced POST to Command Service was okay");
            }
            else
            {
                Console.WriteLine("--> Synced POST to Command Service was not okay");
            }

      
        }
    }
}