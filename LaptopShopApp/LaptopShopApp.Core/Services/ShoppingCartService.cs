﻿using LaptopShopApp.Core.Contracts;
using LaptopShopApp.Data;
using LaptopShopApp.Infrastructure.Data.Domain;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopShopApp.Core.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly ApplicationDbContext _context; 
        private readonly IProductService _productService;

        public ShoppingCartService(ApplicationDbContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }

        public bool AddToCart(string userId, int productId, int quantity)
        {
            if (productId == null)
            {
                return false;
            }

            Product? product = _context.Products.FirstOrDefault(x => x.Id == productId);

            if (product?.Quantity <= 0)
            {
                return false;
            }

            ShoppingCart? cart = _context.ShoppingCarts.FirstOrDefault(x => x.UserId == userId && x.ProductId == productId);
            if (quantity <= 0)
            {
                return false;
            }

            if (cart != null)
            {
                return ChangeQuantity(userId,productId,quantity);
            }

            ShoppingCart shoppingCart = new ShoppingCart()
            {
                UserId=userId,
                ProductId=productId,
                Quantity=quantity
            };
            
            _context.ShoppingCarts.Add(shoppingCart);
            return _context.SaveChanges() != 0;
        }

        public bool ChangeQuantity(string userId, int productId, int quantity)
        {
            ShoppingCart? cart = _context.ShoppingCarts.FirstOrDefault(x => x.UserId == userId && x.ProductId == productId);
            if (cart == null)
            {
                return false;
            }
            cart.Quantity += quantity;
            if (cart.Quantity <= 0 || cart.Product.Quantity < cart.Quantity)
            {
                return false;
            }
            _context.ShoppingCarts.Update(cart);
            return _context.SaveChanges() != 0;
        }

        public bool CleanCart(string userId)
        {
            List<ShoppingCart> userCartItems = _context.ShoppingCarts.Where(x => x.UserId == userId).ToList();
            if (!userCartItems.Any()) return false;

            _context.ShoppingCarts.RemoveRange(userCartItems);
            return _context.SaveChanges() != 0;
        }

        public List<ShoppingCart> GetAll (string userId)
        {
            List<ShoppingCart> carts = _context.ShoppingCarts.Where(x => x.UserId == userId).ToList();
            return carts;
        }

        public bool RemoveById(string userId, int productId)
        {
            ShoppingCart? cart = _context.ShoppingCarts.FirstOrDefault(x => x.UserId == userId && x.ProductId == productId);
            if (cart == null) return false;

            _context.ShoppingCarts.Remove(cart);
            return _context.SaveChanges() != 0;
        }

        
    }
}
