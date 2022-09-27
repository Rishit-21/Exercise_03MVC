using Exercise_03.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise_03.Repository
{
   public interface IProductRateRepository
    {
        Task<List<ProductRateModel>> GetAllProductRate();
        Task<int> AddProductRate(ProductRateModel model);
        Task<int> UpdateProductRate(ProductRateModel model, int id);
        Task<bool> DeleteProductRate(ProductRateModel model, int id);
    }
}
