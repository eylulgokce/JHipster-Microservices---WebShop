namespace PaymentService.Model
{
    public class Payment
    {
        public Payment() { }
        public Payment(int idCustomer, string paymentMethod)
        {
            IdOrder = idCustomer;
            PaymentMethod = paymentMethod;
        }

        public int IdOrder { get; set; }
        public string PaymentMethod { get; set; }
    }
}
