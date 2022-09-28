using Models;
using ModelsDb;
using Services.Exceptions;
using Services.Filters;
using Services.Storage;

namespace Services
{
    public class ClientService
    {

        public DbBank _dbContext;

        public ClientService()
        {
            _dbContext = new DbBank();
        }

        public ClientDb GetClient(Guid clientId)
        {
            var client = _dbContext.clients.FirstOrDefault(c => c.Id == clientId);

            if (client == null)
            {
                throw new ExistsException("Этого клиента не сущетсвует");
            }

            return client;
        }
        
        public List<Client> GetClients(ClientFilter clientFilter)
        {
            var selection = _dbContext.clients.Select(p => p);

            if (clientFilter.Name != null)
                selection = selection.
                    Where(p => p.Name == clientFilter.Name);

            if (clientFilter.PasportNum != 0)
                selection = selection.
                    Where(p => p.PasportNum == clientFilter.PasportNum);

            if (clientFilter.StartDate != new DateTime())
                selection = selection.
                    Where(p => p.BirtDate == clientFilter.StartDate);

            if (clientFilter.EndDate != new DateTime())
                selection = selection.
                    Where(p => p.BirtDate == clientFilter.EndDate);

            return selection.Select(clientDb => new Client()
            {
                Id = clientDb.Id,
                Name = clientDb.Name,
                PasportNum = clientDb.PasportNum,
                BirtDate = clientDb.BirtDate,
                Bonus = clientDb.Bonus
            })
            .ToList();
        }

        public void AddClient(Client client)
        {
            var clientDb = new ClientDb
            {
                Id = client.Id,
                Name = client.Name,
                PasportNum = client.PasportNum,
                BirtDate = client.BirtDate,
                Bonus = client.Bonus
            };
            if (clientDb.PasportNum == 0)
            {
                throw new NoPasportData("У клиента нет паспортных данных");
            }

            if (DateTime.Now.Year - clientDb.BirtDate.Year < 18)
            {
                throw new Under18Exception("Клиент меньше 18 лет");
            }
            var accountDb = new AccountDb
            {
                Id = Guid.NewGuid(),
                ClientId = clientDb.Id,
                Currency = new CurrencyDb
                {
                    Name = "RUB",
                    Code = 1,
                }
            };
            
            _dbContext.clients.Add(clientDb);
            _dbContext.accounts.Add(accountDb);
            _dbContext.SaveChanges();
        }


        public void AddAccount(Client client, Account account)
        {
            var accountDb = new AccountDb
            {
                Id = Guid.NewGuid(),
                ClientId = client.Id,
                Currency = new CurrencyDb
                {
                    Name = account.Currency.Name,
                    Code = account.Currency.Code,
                }
            };
            if (accountDb.Currency.Name == null)
                throw new ExistsException("Этот аккаунт не привязан ни к одному клиенту");

            _dbContext.accounts.Add(accountDb);
            _dbContext.SaveChanges();
        }

        public void UpdateClient(Client client)
        {
            var clientDb = _dbContext.clients.FirstOrDefault(c => c.Id == client.Id);

            if (clientDb == null)
            {
                throw new ExistsException("Этого клиента не существует");
            }

            _dbContext.clients.Update(clientDb);
            _dbContext.SaveChanges();
        }

        public void UpdateAccount(Client client, Account account)
        {
            var clientDb = _dbContext.clients.FirstOrDefault(c => c.Id == client.Id);
            if (clientDb == null)
                throw new ExistsException("В базе нет такого клиента");

            var accountDb = _dbContext.accounts.FirstOrDefault(a => a.Currency.Name == account.Currency.Name);

            if (accountDb == null)
                throw new ExistsException("У клиента нет такого счета");

            _dbContext.accounts.Update(accountDb);
            _dbContext.SaveChanges();
        }

        public void DeleteClient(Client client)
        {
            var clientDb = _dbContext.clients.FirstOrDefault(c => c.Id == client.Id);

            if (clientDb == null)
            {
                throw new ExistsException("Этого клиента не существует");
            }

            _dbContext.clients.Remove(clientDb);
            _dbContext.SaveChanges();
        }

        public void DeleteAccount(Client client, Account account)
        {
            var clientDb = _dbContext.clients.FirstOrDefault(c => c.Id == client.Id);
            if (clientDb == null)
                throw new ExistsException("В базе нет такого клиента");

            var accountDb = _dbContext.accounts.FirstOrDefault(a => a.Currency.Name == account.Currency.Name);

            if (accountDb == null)
                throw new ExistsException("У клиента нет такого счета");

            _dbContext.accounts.Remove(accountDb);
            _dbContext.SaveChanges();
        }
    }
}