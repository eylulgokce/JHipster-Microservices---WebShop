namespace CostumerService.Database
{
    public interface ICustomerDatabase
    {
        int FindCostumerID(string firstname, string lastname, string email);
        void insertCostumer(string firstname, string lastname, string email, string address, string city, string country);
    }
}
