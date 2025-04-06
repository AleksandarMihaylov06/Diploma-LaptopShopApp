using LaptopShopApp.Infrastructure.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopShopApp.Core.Contracts
{
    public interface IShoppingCartService
    {
        List<ShoppingCart> GetAll(string userId);
        bool AddToCart(string userId, int productId ,int quantity);
        bool RemoveById(string userId, int productId);
        bool CleanCart(string userId);
        bool ChangeQuantity(string userId, int productId, int quantity);
    }
}
