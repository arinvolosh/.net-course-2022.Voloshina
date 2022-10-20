using CsvHelper;
using Models;
using Newtonsoft.Json;
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

        public void WriteClientToCsv(List<Client> clients)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(_pathToDirectory);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            string fullPath = Path.Combine(_pathToDirectory, _csvFileName);

            using (FileStream fileStream = new FileStream(fullPath, FileMode.OpenOrCreate))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
                {
                    using (var writer = new CsvWriter(streamWriter, CultureInfo.CurrentCulture))
                    {
                        writer.WriteRecords(clients);
                        writer.Flush();
                    }
                }
            }
        }

        public List<Client> ReadClientFromCsv(string pathToDirectory, string fileName)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(_pathToDirectory);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            string fullPath = Path.Combine(_pathToDirectory, _csvFileName);

            using (var fileStream = new FileStream(fullPath, FileMode.OpenOrCreate))
            {
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    using (var reader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                    {
                        var clients = reader.EnumerateRecords(new Client());
                        return clients.ToList();
                    }
                }
            }
        }

        public async Task PersonSerializationWriteToFile<T>(List<T> person, string pathToDirectory, string fileName) where T : Person
        {
            DirectoryInfo dirInfo = new DirectoryInfo(pathToDirectory);

            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            string fullPath = Path.Combine(pathToDirectory, fileName);
            using (StreamWriter writer = new StreamWriter(fullPath))
            {
                string personSerialize = JsonConvert.SerializeObject(person);
                await writer.WriteAsync(personSerialize);
            }
        }
        public async Task<List<T>> PersonDeserializationReadFile<T>(string pathToDirectory, string fileName) where T : Person
        {
            DirectoryInfo dirInfo = new DirectoryInfo(pathToDirectory);

            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            string fullPath = Path.Combine(pathToDirectory, fileName);
            using (StreamReader reader = new StreamReader(fullPath))
            {
                string personSerialize = await reader.ReadToEndAsync();
                List<T> personDeserialize = JsonConvert.DeserializeObject<List<T>>(personSerialize);
                return personDeserialize;
            }

        }
    }
}