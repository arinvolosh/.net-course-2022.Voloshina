using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDb
{
    [Table(name: "employees")]
    public class EmployeeDb
    {
        [Column(name: "id_employee")]
        public Guid Id { get; set; }

        [Column(name: "name")]
        public string Name { get; set; }

        [Column(name: "birt_date")]
        public DateTime BirtDate { get; set; }

        [Column(name: "pasport_num")]
        public int PasportNum { get; set; }

        [Column(name: "salary")]
        public int Salary { get; set; }

    }
}
