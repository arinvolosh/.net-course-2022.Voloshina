using CsvHelper;
using CsvHelper.Configuration;
using Models;
using System.Globalization;
using System.Text;

namespace ExportTool
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
            string fullPath = GetFullPathToFile(_pathToDirectory, _csvFileName);

            using (FileStream fileStream = new FileStream(fullPath, FileMode.OpenOrCreate))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
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
            var clientList = new List<Client>();

            string fullPath = GetFullPathToFile(_pathToDirectory, _csvFileName);

            using (var fileStream = new FileStream(fullPath, FileMode.OpenOrCreate))
            {
               using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                    { Delimiter = ";" };
                    using (var reader = new CsvReader(streamReader, config))
                    {
                        var clients = reader.EnumerateRecords(new Client());
                        foreach (var c in clients)
                        {
                            clientList.Add(new Client
                            {
                                Name = c.Name,
                                PasportNum = c.PasportNum,
                                BirtDate = c.BirtDate,
                                Bonus = c.Bonus
                            });
                        }
                    }
                }
            }

            return clientList;
        }


        private string GetFullPathToFile(string pathToFile, string fileName)
        {
            return Path.Combine(pathToFile, fileName);
        }
    }
}