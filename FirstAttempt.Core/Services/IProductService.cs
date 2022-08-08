﻿using FirstAttempt.Core.DTOs;
using FirstAttempt.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAttempt.Core.Services
{
    public interface IProductService:IService<Product>
    {

        Task<List<ProductWithCategoryDto>> GetProductsWithCategory();

    }
}
