using System.Runtime.Serialization;

namespace CostumerService.Model
{
    [DataContract]
    public class Customer
    {
        public Customer() { }
        public Customer(string firstName, string surname, string email, string address, string city, string country)
        {
            FirstName = firstName;
            Surname = surname;
            Email = email;
            Address = address;
            Country = country;
            City = city;
        }

        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }

        [DataMember(Name = "surname")]
        public string Surname { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "address")]
        public string Address { get; set; }

        [DataMember(Name = "country")]
        public string Country { get; set; }

        [DataMember(Name = "city")]
        public string City { get; set; }
    }
}