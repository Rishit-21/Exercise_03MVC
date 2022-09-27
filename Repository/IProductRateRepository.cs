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
        Task<long> AddProductRate(ProductRateModel model);
        Task<long> UpdateProductRate(ProductRateModel model, long id);
        Task<bool> DeleteProductRate(ProductRateModel model, long id);
    }
}
