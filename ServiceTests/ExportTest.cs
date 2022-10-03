using Models;
using ExportTool;
using Xunit;

namespace ServiceTests
{
    public class ExportTest
    {
        [Fact]
        public void ExportClientTest()
        {
            //Arrange
            string pathToDirectory = Path.Combine("C:\\Курс\\.net-course-2022.Voloshina", "ExportData");
            string fileName = "client.csv";
            ClientExporter clientExporter = new ClientExporter(pathToDirectory, fileName);

            Client client = new Client()
            {
                Name = "Ivan",
                BirtDate = new DateTime(1999, 01, 01),
                PasportNum = 234342

            };

            //Act
            clientExporter.WriteClientToCsv(client);
            Client clientFromFile = clientExporter.ReadClientFromCsv();

            //Assert
            Assert.Equal(client.Name, clientFromFile.Name);
            Assert.Equal(client.BirtDate, clientFromFile.BirtDate);
            Assert.Equal(client.PasportNum, clientFromFile.PasportNum);
        }
        public void ExportClientDataBaseTest()
        {
            string pathToDirectory = Path.Combine();
        }
    }
}
