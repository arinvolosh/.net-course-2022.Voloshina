using Models;

namespace Services
{
    public class BankService
    {
        public int SalaryOwnerOfBank(int bankProfit, int expenses, int owners)
        {
            return (int)((bankProfit - expenses) / owners);
        }
        public Employee ClientInEmployee(Client client)
        {
            return new Employee()
            {
                Name = client.Name,
                BirtDate = client.BirtDate,
                PasportNum = client.PasportNum,
                Salary = 0
            };
        }

        public List<Person> blackList = new List<Person>();

        public void AddBonus<T>(T person) where T : Person
        {
            person.Bonus++;
        }
        public void AddToBlackList<T>(T person) where T : Person
        {
            if (!blackList.Contains(person))
                blackList.Add(person);
        }
        public bool PersonInBlackList<T>(T person) where T : Person
        {
            return blackList.Contains(person);
        }
    }
}