using LaptopShopApp.Infrastructure.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopShopApp.Core.Contracts
{
    public interface IOrderService
    {
        bool Create(string userId, string address, IList<OrderProduct> orderProducts);
        IList<Order> GetOrders();
        IList<Order> GetOrdersByUser(string userId);
        Order GetOrderById(int orderId);
        bool UpdateStatus(int orderId, OrderStatus orderStatus);
    }
}
