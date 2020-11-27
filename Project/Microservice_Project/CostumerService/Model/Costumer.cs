using System.Runtime.Serialization;

namespace CostumerService.Model
{
    [DataContract]
    public class Costumer
    {
        public Costumer() { }
        public Costumer(string firstname, string surname, string email, string address, string city, string country)
        {
            Firstname = firstname;
            Surname = surname;
            Email = email;
            Address = address;
            Country = country;
            City = city;
        }

        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}