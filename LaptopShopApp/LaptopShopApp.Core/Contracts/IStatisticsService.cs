﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopShopApp.Core.Contracts
{
    public interface IStatisticsService
    {
        int CountProducts();
        int CountClients();
        int CountOrders();
        decimal SumOrders();
    }
}
