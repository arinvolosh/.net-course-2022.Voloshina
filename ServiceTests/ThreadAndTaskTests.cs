using Models;
using Services;
using Services.Filters;
using Xunit;
using Xunit.Abstractions;

namespace ServiceTests
{
    public class ThreadAndTaskTests
    {
        private ITestOutputHelper _outPut;

        public ThreadAndTaskTests(ITestOutputHelper outPut)
        {
            _outPut = outPut;
        }

        [Fact]
        public void Test()
        {
            var locker = new object();
            var account = new Account { Amount = 0 };
            void AddMoney()
            {
                for (var i = 0; i < 10; i++)
                {
                    lock (locker)
                    {
                        account.Amount += 100;
                        _outPut.WriteLine($"{Thread.CurrentThread.Name} пополнил счет на 100, " +
                                          $"Текущая сумма на счету: {account.Amount}");
                    }
                    Thread.Sleep(1000);
                }
            }
            var threadA = new Thread(AddMoney) { Name = "Thread A" };
            var threadB = new Thread(AddMoney) { Name = "Thread B" };
            threadA.Start();
            threadB.Start();

            Thread.Sleep(30000);
        }

        [Fact]
        public void ExportFromDbAndImportInDb()
        {
            //arrange
            var clientStorage = new ClientStorage();
            var clientService = new ClientService(clientStorage);



        }
    }

    
}
