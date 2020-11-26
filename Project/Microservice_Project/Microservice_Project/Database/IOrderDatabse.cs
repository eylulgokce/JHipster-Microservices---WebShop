﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService
{
    interface IOrderDatabse
    {
        IEnumerable<Order> GetAllOrders();
        void AddOrder(Order order);
    }
}
