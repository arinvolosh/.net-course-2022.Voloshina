using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDb
{
    [Table(name: "clients")]
    public class ClientDb
    {
        [Key]
        [Column (name: "id_client")]
        public Guid Id { get; set; }

        [Column(name: "name")]
        public string Name { get; set; }

        [Column(name: "birt_date")]
        public DateTime BirtDate { get; set; }

        [Column(name: "pasport_num")]
        public int PasportNum { get; set; }

        [Column(name: "bonus")]
        public int Bonus { get; set; }

        [Column(name: "accounts_id")]
        public List<AccountDb> Accounts { get; set; }
    }
}