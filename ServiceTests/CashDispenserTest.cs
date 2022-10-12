using Models;
using ModelsDb;
using Services;
using Xunit;

namespace ServiceTests
{
    public class CashDispenserTest
    {
        [Fact]
        public void TestCashDispenser()
        {
            var cashDispenserServise = new CashDispenserService();
            var dbContext = new DbBank();
            var listTask = new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                var accountDb = dbContext.accounts.Skip(i).Take(1).SingleOrDefault();
                var account = new Account
                {
                    Amount = accountDb.Amount,
                    Currency = new Currency { Name = accountDb.Currency.Name }
                };
                listTask.Add(cashDispenserServise.AccountCashingOut(accountDb.ClientId, account));
                Task.Delay(1000).Wait();
            }
            foreach (Task task in listTask)
            {
                task.Wait();
            }
        }
    }
}
