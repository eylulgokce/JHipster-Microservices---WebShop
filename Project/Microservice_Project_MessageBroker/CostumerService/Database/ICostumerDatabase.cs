namespace CostumerService.Database
{
    public interface ICostumerDatabase
    {
        int FindCostumerID(string firstname, string lastname, string email);
    }
}
