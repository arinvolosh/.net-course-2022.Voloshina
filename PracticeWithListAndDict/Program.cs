using Services;
using Models;
using System.Diagnostics;

public class Program
{
    static void Main()
    {
        var testDataGenerator = new TestDataGenerator();
        var sw = new Stopwatch();
        var clientList = testDataGenerator.GetClientsList();
        var clientDictionary = testDataGenerator.GetClientsDictionary();
        var employeesList = testDataGenerator.GetEmployeesList();
        for (int i = 0; i < 4; i++)
        {
            if (i == 3)
            {
                clientList.Add(new Client
                {
                    Name = "Ivan",
                    BirtDate = new DateTime(1990, 01, 01),
                    PasportNum = 324786,
                });
                sw.Start();
                clientList.Find(p => p.PasportNum == 324786);
                sw.Stop();
                Console.WriteLine($"{i}Поиск клиента по его номеру телефона в list занял: {sw.Elapsed.Milliseconds}");
                sw.Restart();
            }
            else
            {
                sw.Start();
                clientList.Find(p => p.PasportNum == i);
                sw.Stop();
                Console.WriteLine($"{i}Поиск клиента по его номеру телефона в list занял: {sw.Elapsed.Milliseconds}");
                sw.Restart();

            }
        }

       

        for (int i = 0; i < 4; i++)
        {
            sw.Start();
            clientDictionary.ContainsKey(i);
            sw.Stop();
            Console.WriteLine($"\n{i}Поиск клиента по его номеру телефона в Dictionary занял: {sw.Elapsed.Milliseconds}");
            sw.Restart();
        }

        var clientsUnder18 = clientList.Where(y => DateTime.Now.Year - y.BirtDate.Year < 18).ToList();
        foreach (Client client in clientsUnder18)
        {
            Console.WriteLine("\nКлиенты младше 18:" +
                              $"\nИмя : {client.Name}" +
                              $" \nНомер паспорта : {client.PasportNum}" +
                              $" \nДата рождения : {client.BirtDate}");
        }
        var employeeMinSalary = employeesList.FirstOrDefault(s => s.Salary == employeesList.Min(s => s.Salary));
        Console.WriteLine($"\nСотрудник с минимально заработной платой:" +
                          $"\nИмя : {employeeMinSalary.Name}" +
                          $"\nНомер паспорта : {employeeMinSalary.PasportNum}" +
                          $"\nЗарплата : {employeeMinSalary.Salary}");
        for (int i = 0; i < 4; i++)
        {
            sw.Start();
            var lastСlientByFirstOrDefault = clientDictionary.FirstOrDefault(p => p.Key == clientDictionary.Keys.Last());
            sw.Stop();
            Console.Write($"\n{i} Поиск последнего клиента списка по ключу занял(способ FirstOrDefault): {sw.Elapsed.Milliseconds}");
            sw.Restart();

            sw.Start();
            var lastСlientByKey = clientDictionary[clientDictionary.Keys.Last()];
            sw.Stop();
            Console.Write($"\n{i} Поиск последнего клиента списка по ключу занял(способ с ключом): {sw.Elapsed.Milliseconds}");
            sw.Restart();
        }
    }
}
