﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type, string? sort);
        Task<Product?> GetProductAsync(int id );

        Task<IReadOnlyList<String>> GetBrandsAsync();
        Task<IReadOnlyList<String>> GetTypesAsync();

        void CreateProduct(Product product);
        void UpdateProduct( Product product);
        void DeleteProduct(Product product);

        bool ProductExists (int id);

        Task<bool> SaveChangesAsync();

    }
}
