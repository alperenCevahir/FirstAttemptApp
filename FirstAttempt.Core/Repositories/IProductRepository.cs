﻿using FirstAttempt.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAttempt.Core.Repositories
{
    public interface IProductRepository: IGenericRepository<Product>
    {
        Task<List<Product>> GetProductsWithCategory();
    }
}
