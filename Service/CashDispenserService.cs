using Models;
using Services.Exceptions;

namespace Services
{
    public class CashDispenserService
    {
        ClientService clientService = new ClientService();

        public Task AccountCashingOut(Guid id, Account account)
        {
            return Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    if (account.Amount >= 5)
                    {
                        account.Amount -= 5;
                        clientService.UpdateAccount(id, account);
                        Task.Delay(5000).Wait();
                    }
                    else
                    {
                        throw new InsufficientFundsInAccountException("Недостаточно средств!");
                    }
                }
            });
        }
    }
}
