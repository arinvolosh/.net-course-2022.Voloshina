using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using ModelsDb;
using Services.Exceptions;

namespace Services.Storage
{
    public class ClientStorage : IClientStorage
    {
        public Dictionary<Client, List<Account>> Data { get; }

        public void Add(Client client)
        {
            Data.Add(
                client,
                new List<Account>
                {
                    new Account
                    {
                        Currency = new Currency
                        {
                            Code = 1,
                            Name = "Euro",
                        },
                        Amount = 0
                    }
                });
        }

        public void AddAccount(Client client, Account account)
        {
            Data[client].Add(account);
        }

        public void Remove(Client item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAccount(Client client, Account account)
        {
            throw new NotImplementedException();
        }

        public void Update(Client item)
        {
            var oldClient = Data.Keys.First(p => p.PasportNum == item.PasportNum);
            oldClient.Name = item.Name;
            oldClient.PasportNum = item.PasportNum;
            oldClient.BirtDate = item.BirtDate;
        }

        public void UpdateAccount(Client client, Account account)
        {
            var oldAccount = Data[client].FirstOrDefault(p => p.Currency.Name == account.Currency.Name);
            oldAccount.Currency.Name = account.Currency.Name;
            oldAccount.Currency.Code = account.Currency.Code;
            oldAccount.Amount = account.Amount;
        }
    }
}
