using System.Runtime.Serialization;

namespace CartService.Model
{
    [DataContract]
    public class SelectedProduct
    {
        public SelectedProduct() { }

        public SelectedProduct(int idProduct, int numUnits)
        {
            IdProduct = idProduct;
            NumUnits = numUnits;
        }

        public int IdProduct { get; set; }
        public int NumUnits { get; set; }
    }
}
