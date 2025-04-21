using LaptopShopApp.Infrastructure.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopShopApp.Core.Contracts
{
    public interface IMessages
    {
        bool Create(string name,string email,string message);
        IList<Messages> GetMessages();
    }
}
