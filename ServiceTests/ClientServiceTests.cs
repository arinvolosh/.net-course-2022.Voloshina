using Services;
using Models;
using Xunit;
using Services.Exceptions;
using Services.Filters;

namespace ServiceTests
{
    public class ClientServiceTests
    {
        [Fact]
        public async Task AddClientLimit18YearsExceptionTest()
        {
            var clientService = new ClientService();
            // Arrange
            var ivan = new Client
            {
                Name = "Ivan",
                BirtDate = new DateTime(2006, 01, 01),
                PasportNum = 324763
            };
            // Act&Assert
            try
            {
                await clientService.AddClient(ivan);
            }
            catch (Under18Exception e)
            {
                Assert.Equal("Клиенту меньше 18", e.Message);
                Assert.Equal(typeof(Under18Exception), e.GetType());
            }
            catch (Exception e)
            {
                Assert.True(false);
            }
        }
        [Fact]
        public async Task AddClientNoPasportDataExceptionTest()
        {
            // Arrange
            var clientService = new ClientService();
            var ivan = new Client
            {
                Name = "Ivan",
                BirtDate = new DateTime(2000, 01, 01)
            };
            // Act&Assert
            try
            {
                await clientService.AddClient(ivan);
            }
            catch (NoPasportData e)
            {
                Assert.Equal("У клиента нет паспортных данных", e.Message);
                Assert.Equal(typeof(NoPasportData), e.GetType());
            }
            catch (Exception e)
            {
                Assert.True(false);
            }
        }
        [Fact]
        public async Task AddClientExistingClientTest()
        {
            // Arrange
            var clientService = new ClientService();
            var testDataGenerator = new TestDataGenerator();
            var oldClient = testDataGenerator.GetFakeDataClient().Generate();
            var newClient = new Client()
            {
                Name = oldClient.Name,
                PasportNum = oldClient.PasportNum,
                BirtDate = oldClient.BirtDate
            };
            // Act&Assert
            try
            {
                await clientService.AddClient(oldClient);
                await clientService.AddClient(newClient);

            }
            catch (ExistsException e)
            {
                Assert.Equal("Этот клиент уже существует", e.Message);
                Assert.Equal(typeof(ExistsException), e.GetType());
            }
            catch (Exception e)
            {
                Assert.True(false);
            }
        }
        [Fact]
        public async Task GetClientsFilterTest()
        {
            // Arrange
            var clientService = new ClientService();
            var testDataGenerator = new TestDataGenerator();
            var clientFilter = new ClientFilter();
            var client = new Client();

            for (int i = 0; i < 10; i++)
            {
                client = testDataGenerator.GetFakeDataClient().Generate();
                await clientService.AddClient(client);
            };

            // Act&Assert
            clientFilter.Name = client.Name;
            clientFilter.PasportNum = client.PasportNum;

            Assert.NotNull(clientService.GetClients(clientFilter));

        }

        [Fact]
        public void DeleteClientKeyNotFoundExceptionTest()
        {
            // Arrange
            var clientService = new ClientService();
            var testDataGenerator = new TestDataGenerator();
            var existsClient = testDataGenerator.GetFakeDataClient().Generate();
            var noExistsClient = testDataGenerator.GetFakeDataClient().Generate();

            // Act&Assert
            try
            {
                var ivan = new Client
                {
                    Name = "Ivan",
                    BirtDate = new DateTime(2000, 01, 01)
                };
                var ivanI = new Client
                {
                    Name = "IvanI",
                    BirtDate = new DateTime(2000, 01, 01)
                };
                var newAccount = new Account
                { 
                    Amount = 1,
                    Currency = new Currency { }
                };
                clientService.AddClient(ivan);
                clientService.AddAccount(ivan, newAccount);

                //Assert.Throws<ExistsException>(() => clientService.AddAccount(ivanI, newAccount));
                //Assert.Throws<ExistsException>(() => clientService.AddAccount(ivan, newAccount));
                //Assert.Contains(newAccount, clientStorage.Data[ivan]);
            }
            catch (Exception e)
            {
                Assert.True(false);
            }

        }
    }
}