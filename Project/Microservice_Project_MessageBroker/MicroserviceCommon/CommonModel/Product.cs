using System.Runtime.Serialization;

namespace MicroserviceCommon.Model
{
    [DataContract]
    public class Product
    {
        public Product(string name, string description, decimal price, int availableUnits)
        {
            Name = name;
            Description = description;
            Price = price;
            AvailableUnits = availableUnits;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int AvailableUnits { get; set; }
        public string Status => AvailableUnits > 0 ? "Available" : "Out of stock";
    }
}
