using Models;
using ModelsDb;
using Services;
using Xunit;
using Xunit.Abstractions;

namespace ServiceTests
{

    public class ThreadAndTaskTests
    {
        object locker = new object();

        private ITestOutputHelper _outPut;

        public ThreadAndTaskTests(ITestOutputHelper outPut)
        {
            _outPut = outPut;
        }

        [Fact]
        public void Test()
        {
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
        public void WriteFromCsvAndReadToCsv()
        {
            //arrange
            var bankContext = new DbBank();
            var clientStorage = new ClientStorage();
            var clientService = new ClientService(clientStorage);
            string pathToDirectory = Path.Combine("C:\\Курс\\.net-course-2022.Voloshina", "ExportData");
            string fileName = "client.csv";
            ExportService exportService = new ExportService(pathToDirectory, fileName);

            var threadWriteFromCsv = new Thread(() =>
            {
                List<Client> listClients = new List<Client>();
                lock (locker)
                {
                    foreach (var client in bankContext.clients)
                    {
                        listClients.Add(new Client
                        {
                            Id = client.Id,
                            Name = client.Name,
                            BirtDate = client.BirtDate,
                            PasportNum = client.PasportNum,
                            Bonus = client.Bonus
                        });
                        Thread.Sleep(1000);
                    }

                    exportService.WriteClientToCsv(listClients);
                }
                foreach (var client in listClients)
                {
                    _outPut.WriteLine($"Клиент: ID:{client.Id}; ФИО:{client.Name}");
                }
            });

            threadWriteFromCsv.Start();
            threadWriteFromCsv.Join();

            var threadReadToCsv = new Thread(() =>
            {
                lock (locker)
                {
                    List<Client> clientsFromCsv = exportService.ReadClientFromCsv();

                    foreach (var client in clientsFromCsv)
                    {
                        clientService.AddClient(client);
                    }
                    Thread.Sleep(1000);
                    _outPut.WriteLine($"Данные прочитанны");
                }
            });

            threadReadToCsv.Start();
            threadReadToCsv.Join();

            Thread.Sleep(30000);
        }
    }
}
