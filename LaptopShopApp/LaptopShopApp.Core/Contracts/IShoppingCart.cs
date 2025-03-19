using LaptopShopApp.Infrastructure.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopShopApp.Core.Contracts
{
    public interface IShoppingCart
    {
        bool AddToCart(string userId, int productId ,int quantity);
        bool RemoveById(string userId, int productId);
        bool CleanCart(string userId);
    }
}
