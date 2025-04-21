using LaptopShopApp.Core.Contracts;
using LaptopShopApp.Data;
using LaptopShopApp.Infrastructure.Data.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LaptopShopApp.Core.Services
{
    public class Mesages : IMessages
    {
        public readonly ApplicationDbContext _context;

        public Mesages(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Create(string name, string email, string text)
        {
            var message = new Messages
            {
                Name = name,
                Email = email,
                Message = text,
                SendDate = DateTime.Now
            };

            _context.Messages.Add(message);

            return _context.SaveChanges() != 0;
        }

        public IList<Messages> GetMessages()
        {
            return _context.Messages.OrderByDescending(x => x.Id).ToList();
        }
    }
}
