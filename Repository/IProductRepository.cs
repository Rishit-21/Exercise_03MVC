using Exercise_03.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise_03.Repository
{
    public interface IProductRepository
    {
        Task<List<ProductModel>> GetAllProducts();
        Task<int> AddProduct(ProductModel model);
        Task<int> UpdateProduct(ProductModel model, int id);
        Task<bool> DeleteProduct(ProductModel model, int id);
    }
}
