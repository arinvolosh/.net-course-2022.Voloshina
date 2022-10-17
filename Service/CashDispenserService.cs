using Models;
using Services.Exceptions;

namespace Services
{
    public class CashDispenserService
    {
        private ClientService _clientService;

        public CashDispenserService(ClientService clientService)
        {
            _clientService = clientService;
        }

        public async Task AccountCashingOut(Guid id, Account account)
        {
            for (int i = 0; i < 10; i++)
            {
                if (account.Amount >= 5)
                {
                    account.Amount -= 5;
                    await _clientService.UpdateAccount(id, account);
                    Task.Delay(5000).Wait();
                }
                else
                {
                    throw new InsufficientFundsInAccountException("Недостаточно средств!");
                }
            }
        }
    }
}