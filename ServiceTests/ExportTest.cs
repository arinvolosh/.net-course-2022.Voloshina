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
            ExportService clientExporter = new ExportService(pathToDirectory, fileName);
            var listClients = new TestDataGenerator().GetClientsList();
            //Act
            clientExporter.WriteClientToCsv(listClients);
            var tests = clientExporter.ReadClientFromCsv(pathToDirectory, fileName);
            //Assert
            Assert.NotNull(tests);
        }
        [Fact]
        public async void ReadClientTest()
        {
            //Arrange
            var clientStorage = new ClientStorage();
            var clientService = new ClientService(clientStorage);

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
            Assert.NotEmpty(clientsRead);
        }
        [Fact]
        public async void ClientSerializationWriteAndReadFromFileAsync_Test()
        {
            //Arrenge
            TestDataGenerator testDataGenerator = new TestDataGenerator();
            string pathToDirectory = Path.Combine("C:\\Курс\\.net-course-2022.Voloshina", "ExportData");
            string fileName = "clientSerialization.json";
            ExportService exportService = new ExportService(pathToDirectory, fileName);
            List<Client> clients = testDataGenerator.GetFakeDataClient().Generate(10);

            //Act
            await exportService.PersonSerializationWriteToFile(clients, pathToDirectory, fileName);
            var clientsDesirialization = await exportService.PersonDeserializationReadFile<Client>(pathToDirectory, fileName);

            //Assert
            Assert.Equal(clients.First().Id, clientsDesirialization.First().Id);

        }

        [Fact]
        public async void EmployeeSerializationWriteAndReadFileTest()
        {
            //Arrenge
            TestDataGenerator testDataGenerator = new TestDataGenerator();
            string pathToDirectory = Path.Combine("C:\\Курс\\.net-course-2022.Voloshina", "ExportData");
            string fileName = "employeeSerialization.json";
            ExportService exportService = new ExportService(pathToDirectory, fileName);
            List<Employee> employees = testDataGenerator.GetFakeDataEmployee().Generate(10);

            //Act
            await exportService.PersonSerializationWriteToFile(employees, pathToDirectory, fileName);
            List<Employee> employeesDesirialization = await exportService.PersonDeserializationReadFile<Employee>(pathToDirectory, fileName);

            //Assert
            Assert.Equal(employees.First().Id, employeesDesirialization.First().Id);

        }
    }
}
