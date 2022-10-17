using Microsoft.EntityFrameworkCore;
using Models;
using ModelsDb;

namespace Services
{
    public class RateUpdater
    {
        private ClientService _clientService;

        public RateUpdater(ClientService clientService)
        {
            _clientService = clientService;
        }

        DbBank dbContext = new DbBank();

        public async Task AccruingInterest(CancellationToken token)
        {
            var accountsDb = await dbContext.accounts.Take(10).ToListAsync();
            
            while (!token.IsCancellationRequested)
            {
                foreach (var accountDb in accountsDb)
                {
                    var updateAccount = new Account
                    {
                        Amount = accountDb.Amount,
                        Currency = new Currency { Name = accountDb.Currency.Name }
                    };
                    updateAccount.Amount += 5;
                    await _clientService.UpdateAccount(accountDb.ClientId, updateAccount);
                }
                Task.Delay(5000).Wait();
            }
        }
    }
}
