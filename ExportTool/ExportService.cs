using CsvHelper;
using Models;
using System.Globalization;

namespace ExportTool
{
    public class ClientExporter
    {
        private string _pathToDirecory { get; set; }
        private string _csvFileName { get; set; }

        public ClientExporter(string pathToDirectory, string csvFileName)
        {
            _pathToDirecory = pathToDirectory;
            _csvFileName = csvFileName;
        }

        public void WriteClientToCsv(Client client)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(_pathToDirecory);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            string fullPath = GetFullPathToFile(_pathToDirecory, _csvFileName);

            using (FileStream fileStream = new FileStream(fullPath, FileMode.OpenOrCreate))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    using (var writer = new CsvWriter(streamWriter, CultureInfo.CurrentCulture))
                    {
                        writer.WriteField(nameof(Client.Name));
                        writer.WriteField(nameof(Client.BirtDate));
                        writer.WriteField(nameof(Client.PasportNum));

                        writer.NextRecord();

                        writer.WriteField(client.Name);
                        writer.WriteField(client.BirtDate);
                        writer.WriteField(client.PasportNum);

                        writer.NextRecord();

                        writer.Flush();
                    }
                }
            }
        }

        public Client ReadClientFromCsv()
        {
            string fullPath = GetFullPathToFile(_pathToDirecory, _csvFileName);
            using (FileStream fileStream = new FileStream(fullPath, FileMode.OpenOrCreate))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    using (var reader = new CsvReader(streamReader, CultureInfo.CurrentCulture))
                    {
                        reader.Read();
                        return reader.GetRecord<Client>();
                    }
                }
            }
        }


        private string GetFullPathToFile(string pathToFile, string fileName)
        {
            return Path.Combine(pathToFile, fileName);
        }
    }
}