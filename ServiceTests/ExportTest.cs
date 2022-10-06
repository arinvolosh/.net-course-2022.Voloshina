using ExportTool;
using Xunit;
using Services;
using Models;

namespace ServiceTests
{
    public class ExportTest
    {
        [Fact]
        public void WriteClientTest()
        {
            //Arrange
            string pathToDirectory = Path.Combine("C:\\Курс\\.net-course-2022.Voloshina", "ExportData");
            string fileName = "client.csv";
            ExportService clientExporter = new ExportService(pathToDirectory, fileName);
            var listClients = new TestDataGenerator().GetClientsList();
            //Act
            clientExporter.WriteClientToCsv(listClients);
            var tests = clientExporter.ReadClientFromCsv(pathToDirectory, fileName);
            //Assert
            Assert.NotNull(tests);
        }
        [Fact]
        public void ReadClientTest()
        {
            //Arrange
            var clientService = new ClientService();

            var listClients = new List<Client>();
            var clientIvan = new Client();
            
            clients.Add(clientIvan);


            //Act
            string pathToDirectory = Path.Combine("C:\\Курс\\.net-course-2022.Voloshina", "ExportData");
            string fileName = "client.csv";
            ExportService clientExporter = new ExportService(pathToDirectory, fileName);
            clientExporter.WriteClientToCsv(clients);

            var clientsRead = clientExporter.ReadClientFromCsv();
            foreach (var client in clientsRead)
            {
                service.AddClient(client);
            }

            //Assert
            Assert.NotEmpty(clientsRead);
        }

    }
}
