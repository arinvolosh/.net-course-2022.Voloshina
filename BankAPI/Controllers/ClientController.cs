using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace BankAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        ClientService _clientService;

        public ClientController(ILogger<ClientController> logger)
        {
            _logger = logger;
            _clientService = new ClientService();
        }

        [HttpGet]
        public async Task<Client> GetAsync(Guid Id)
        {
            return await _clientService.GetClient(Id);
        }

        [HttpPost]
        public async Task Add(Client client)
        {
            await _clientService.AddClient(client);
        }

        [HttpPut]
        public async Task Update(Client client)
        {
            await _clientService.UpdateClient(client);
        }

        [HttpDelete]
        public async Task Delete(Client client)
        {
            await _clientService.DeleteClient(client);
        }
    }
}
