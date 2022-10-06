using Models;
using Bogus;
using Bogus.DataSets;
using System.Reflection.Emit;
using System.Dynamic;
using CurrencyM = Models.Currency;


namespace Services
{
    public class TestDataGenerator
    {
        public Faker<Client> GetFakeDataClient()
        {
            var generator = new Faker<Client>("ru")
                .StrictMode(true)
                .RuleFor(u => u.Name, f => f.Name.ToString())
                .RuleFor(u => u.PasportNum, f => f.Random.Int(100000, 999999))
                .RuleFor(u => u.BirtDate, f => f.Date.Past(100));
            return generator;
        }

        public Faker<Employee> GetFakeDataEmployee()
        {
            var generator = new Faker<Employee>("ru")
                .StrictMode(true)
                .RuleFor(u => u.Name, f => f.Name.ToString())
                .RuleFor(u => u.PasportNum, f => f.Random.Int(100000, 999999))
                .RuleFor(u => u.BirtDate, f => f.Date.Past(100))
                .RuleFor(u => u.Contract, (f, u) => "PasportNum: " + u.PasportNum +
                                                    "\nName: " + u.Name +
                                                    "\nBirtDate: " + u.BirtDate.ToString("D"))

                .RuleFor(u => u.Salary, f => f.Random.Int(1000, 9999));
            return generator;
        }



        public List<Client> GetClientsList()
        {
            var clientList = GetFakeDataClient().Generate(1000);
            return clientList;
        }

        public Dictionary<int, Client> GetClientsDictionary()
        {
            Dictionary<int, Client> clientDictionary = new Dictionary<int, Client>();
            for (int i = 0; i < 1000; i++)
            {
                Client client = GetFakeDataClient().Generate();
                clientDictionary.Add(client.PasportNum, client);
            }
            return clientDictionary;
        }

        public List<Employee> GetEmployeesList()
        {
            var employeeList = GetFakeDataEmployee().Generate(1000);
            return employeeList;
        }

        public Dictionary<Client, List<Account>> GetAccounDictionary()
        {
            Dictionary<Client, List<Account>> accounDictionary = new Dictionary<Client, List<Account>>();

            accounDictionary.Add(
                new Client
                {

                },
                new List<Account>
                {
                    new Account
                    {
                        Currency = new CurrencyM
                        {
                            Name = "Euro",
                            Code = 1,
                        },
                        Amount = 33,
                    },
                    new Account
                    {
                        Currency = new CurrencyM
                        {
                            Name = "RUB",
                            Code = 2,
                        },
                        Amount = 34,
                    }
                });

            accounDictionary.Add(
                new Client
                {
                    Name = "Kolya",
                    PasportNum = 349833,
                    BirtDate = new DateTime(2000, 01, 01)
                },
                new List<Account>
                {
                    new Account
                    {
                        Currency = new CurrencyM
                        {
                           Name = "Euro",
                           Code = 3,
                        },
                        Amount = 35
                    },
                    new Account
                    {
                        Currency = new CurrencyM
                        {
                            Name = "RUB",
                            Code = 4,
                        },
                        Amount = 36,
                    }
                });

            return accounDictionary;
        }
    }
}