using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDb
{
    [Table (name:"accounts")]
    public class AccountDb
    {
        [Key]
        [Column(name: "account_id")]
        public Guid Id { get; set; }

        [Column(name: "amount")]
        public int Amount { get; set; }

        [Column(name: "currency")]
        public CurrencyDb Currency { get; set; }

        [Column(name: "client")]
        public ClientDb Client { get; set; }

        [Column(name: "client_id")]
        public Guid ClientId { get; set; }
    }
}
