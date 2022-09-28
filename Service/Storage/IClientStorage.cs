using ModelsDb;
using Models;

namespace Services.Storage
{
    public interface IClientStorage : IStorage<Client>
    {
        public void AddAccount(Client client, Account account);
        public void RemoveAccount(Client client, Account account);
        public void UpdateAccount(Client client, Account account);
        public Dictionary<Client, List<Account>> Data { get; }
    }
}