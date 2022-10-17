using Microsoft.EntityFrameworkCore;
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

        public async Task<ClientDb> GetClient(Guid clientId)
        {
            var client = await _dbContext.clients.FirstOrDefaultAsync(c => c.Id == clientId);

            if (client == null)
            {
                throw new ExistsException("Этого клиента не сущетсвует");
            }

            return client;
        }
        
        public async Task<List<Client>> GetClients(ClientFilter clientFilter)
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

            return await selection.Select(clientDb => new Client()
            {
                Id = clientDb.Id,
                Name = clientDb.Name,
                PasportNum = clientDb.PasportNum,
                BirtDate = clientDb.BirtDate,
                Bonus = clientDb.Bonus
            })
            .ToListAsync();
        }

        public async Task AddClient(Client client)
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
            
            await _dbContext.clients.AddAsync(clientDb);
            await _dbContext.accounts.AddAsync(accountDb);
            await _dbContext.SaveChangesAsync();
        }


        public async Task AddAccount(Client client, Account account)
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
            {
                throw new ExistsException("Этот аккаунт не привязан ни к одному клиенту");
            }
            await _dbContext.accounts.AddAsync(accountDb);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateClient(Client client)
        {
            var clientDb = await _dbContext.clients.FirstOrDefaultAsync(c => c.Id == client.Id);

            if (clientDb == null)
            {
                throw new ExistsException("Этого клиента не существует");
            }

            _dbContext.clients.Update(clientDb);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAccount(Client client, Account account)
        {
            var clientDb = await _dbContext.clients.FirstOrDefaultAsync(c => c.Id == client.Id);
            if (clientDb == null)
            {
                throw new ExistsException("В базе нет такого клиента");
            }
            var accountDb = await _dbContext.accounts.FirstOrDefaultAsync(a => a.Currency.Name == account.Currency.Name);

            if (accountDb == null)
            {
                throw new ExistsException("У клиента нет такого счета");
            }
            _dbContext.accounts.Update(accountDb);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteClient(Client client)
        {
            var clientDb = await _dbContext.clients.FirstOrDefaultAsync(c => c.Id == client.Id);

            if (clientDb == null)
            {
                throw new ExistsException("Этого клиента не существует");
            }

            _dbContext.clients.Remove(clientDb);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAccount(Client client, Account account)
        {
            var clientDb = await _dbContext.clients.FirstOrDefaultAsync(c => c.Id == client.Id);
            if (clientDb == null)
            {
                throw new ExistsException("В базе нет такого клиента");
            }
            var accountDb = await _dbContext.accounts.FirstOrDefaultAsync(a => a.Currency.Name == account.Currency.Name);

            if (accountDb == null)
            {
                throw new ExistsException("У клиента нет такого счета");
            }
            _dbContext.accounts.Remove(accountDb);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateAccount(Guid id, Account account)
        {
            var clientDb = await _dbContext.clients.FirstOrDefaultAsync(c => c.Id == id);
            if (clientDb == null)
            {
                throw new ExistsException("В базе нет такого клиента");
            }
            var accountDb = await _dbContext.accounts.FirstOrDefaultAsync(a => a.Currency.Name == account.Currency.Name);

            if (accountDb == null)
            {
                throw new ExistsException("У клиента нет такого счета");
            }

            accountDb.Amount = account.Amount;
            accountDb.Currency.Name = account.Currency.Name;

            _dbContext.accounts.Update(accountDb);
            await _dbContext.SaveChangesAsync();

        }
    }
}