using LaptopShopApp.Core.Contracts;
using LaptopShopApp.Data;
using LaptopShopApp.Infrastructure.Data.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopShopApp.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Create(string userId, string address, IList<OrderProduct> orderProducts)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null) 
            { 
                return false;
            }

            Order order = new Order
            { 
                UserId = userId,
                Address = address,
                OrderDate = DateTime.Now,
                OrderStatus = OrderStatus.Procesing,  
                OrderProducts = orderProducts
            };

            _context.Orders.Add(order);

            foreach (var orderProduct in orderProducts)
            {
                orderProduct.OrderId = order.Id; 
                _context.OrderProducts.Add(orderProduct);
            }

            return _context.SaveChanges() != 0;
        }

        public Order GetOrderById(int orderId)
        {
            return _context.Orders.First(x => x.Id == orderId);
        }

        public IList<Order> GetOrders()
        {
            return _context.Orders.OrderByDescending(x => x.OrderDate).ToList();
        }

        public IList<Order> GetOrdersByUser(string userId)
        {
            return _context.Orders.Where(x => x.UserId == userId).ToList();
        }

        public bool UpdateStatus(int orderId, OrderStatus orderStatus)
        {
            Order? order = _context.Orders.FirstOrDefault(x => x.Id == orderId);
            if (order == null)
            {
                return false;
            }

            order.OrderStatus = orderStatus;
            _context.Orders.Update(order);
            return _context.SaveChanges() != 0;
        }
    }
}
