namespace PeopleManagement.Models
{
    public class Person
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public override string ToString()
        {
            return $"Person:\n{Firstname} {Lastname}";
        }
    }
}
