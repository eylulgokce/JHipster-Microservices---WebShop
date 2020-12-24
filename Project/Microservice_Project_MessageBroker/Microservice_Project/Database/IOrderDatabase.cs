using MicroserviceCommon.Model;
using System.Collections.Generic;
using MicroserviceCommon.CommonModel.Order;

namespace OrderService.Database
{
    public interface IOrderDatabase
    {
        IEnumerable<Order> GetAllOrders();
        void AddOrder(Order order);

        IEnumerable<Product> GetAllProductsByOrderId(int idOrder);
    }
}
