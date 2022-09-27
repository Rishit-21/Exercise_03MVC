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
        Task<long> AddProduct(ProductModel model);
        Task<long> UpdateProduct(ProductModel model, long id);
        Task<bool> DeleteProduct(ProductModel model, long id);
    }
}
