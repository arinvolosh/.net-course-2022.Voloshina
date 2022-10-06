using ExportTool;
using Xunit;
using Services;

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
            ExportService clientExporter = new ExportService(pathToDirectory, fileName);
            var listClients = new TestDataGenerator().GetClientsList();
            clientExporter.WriteClientToCsv(listClients);
            var tests = clientExporter.ReadClientFromCsv(pathToDirectory, fileName);
            Assert.NotNull(tests);
        }

    }
}
