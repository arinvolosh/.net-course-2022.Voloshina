using ExportTool;
using Xunit;
using Services;
using Models;

namespace ServiceTests
{
    public class ExportTest
    {
        [Fact]
        public async Task WriteClientTest()
        {
            //Arrange
            string pathToDirectory = Path.Combine("C:\\Курс\\.net-course-2022.Voloshina", "ExportData");
            string fileName = "client.csv";
            var clientExporter = new ExportService(pathToDirectory, fileName);
            var listClients = new TestDataGenerator().GetClientsList();
            //Act
            await clientExporter.WriteClientToCsv(listClients);
            var tests = await clientExporter.ReadClientFromCsv(pathToDirectory, fileName);
            //Assert
            await Assert.NotNull(tests);
        }
        [Fact]
        public async Task ReadClientTest()
        {
            //Arrange
            var clientService = new ClientService();

            var listClients = new List<Client>();
            var clientIvan = new Client();
            
            listClients.Add(clientIvan);


            //Act
            string pathToDirectory = Path.Combine("C:\\Курс\\.net-course-2022.Voloshina", "ExportData");
            string fileName = "client.csv";
            ExportService clientExporter = new ExportService(pathToDirectory, fileName);
            clientExporter.WriteClientToCsv(listClients);

            var clientsRead = await clientExporter.ReadClientFromCsv();
            foreach (var client in clientsRead)
            {
                await clientService.AddClient(client);
            }

            //Assert
            await Assert.NotEmpty(clientsRead);
        }

    }
}
