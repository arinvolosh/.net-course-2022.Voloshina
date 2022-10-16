using CsvHelper;
using Models;
using System.Globalization;
using System.Text;

namespace Services
{
    public class ExportService
    {
        private string _pathToDirectory { get; set; }

        private string _csvFileName { get; set; }

        public ExportService(string pathToDirectory, string csvFileName)
        {
            _pathToDirectory = pathToDirectory;
            _csvFileName = csvFileName;
        }

        public async Task WriteClientToCsv(List<Client> clients)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(_pathToDirectory);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            string fullPath = Path.Combine(_pathToDirectory, _csvFileName);

            await using (FileStream fileStream = new FileStream(fullPath, FileMode.OpenOrCreate))
            {
                await using(StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
                {
                    await using(var writer = new CsvWriter(streamWriter, CultureInfo.CurrentCulture))
                    {
                        writer.WriteRecords(clients);
                        writer.Flush();
                    }
                }
            }
        }

        public async Task <List<Client>> ReadClientFromCsv()
        {
            string fullPath = Path.Combine(_pathToDirectory, _csvFileName);

            await using(FileStream fileStream = new FileStream(fullPath, FileMode.OpenOrCreate))
            {
                using(StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    using(var reader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                    {
                        var clients = reader.EnumerateRecords(new Client());
                        return clients.ToList();
                    }
                }
            }
        }
    }
}