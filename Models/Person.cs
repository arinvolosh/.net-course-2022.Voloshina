namespace Models
{
    public class Person
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime BirtDate { get; set; }
        public int PasportNum { get; set; }
        public int Bonus { get; set; }
        
    }
}