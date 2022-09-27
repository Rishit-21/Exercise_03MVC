using Exercise_03.Data;
using Exercise_03.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise_03.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly PartyProductDbContext _partyProductDbContext;
        private readonly IConfiguration _configuration;

        public ProductRepository(PartyProductDbContext partyProductDbContext, IConfiguration configuration)
        {
            _partyProductDbContext = partyProductDbContext;
            _configuration = configuration;
        }

        public async Task<List<ProductModel>> GetAllProducts()
        {
            return await _partyProductDbContext.Product.Select(product => new ProductModel()
            {
                productName = product.productName,
                Id = product.Id,
            }).ToListAsync();
        }

        public async Task<long> AddProduct(ProductModel model)
        {

            var y = _partyProductDbContext.Product
           .Where(x => x.productName == model.productName).FirstOrDefault();
            if (y == null)
            {
                var newProduct = new Product()
                {
                    productName = model.productName

                };
                await _partyProductDbContext.Product.AddAsync(newProduct);
                await _partyProductDbContext.SaveChangesAsync();
                return newProduct.Id;

            }
            return 0;

        }

        public async Task<long> UpdateProduct(ProductModel model, long id)
        {
            var y = _partyProductDbContext.Product
                    .Where(x => x.productName == model.productName).FirstOrDefault();

            if (y == null)
            {
            var Product = new Product()
            {
                Id = id,
                productName = model.productName,

            };


            _partyProductDbContext.Product.Update(Product);
            await _partyProductDbContext.SaveChangesAsync();
                return 1;
            }
            return 0;
        }


        public async Task<bool> DeleteProduct(ProductModel model, long id)
        {
            //if (id == model.Id)
            //{

            var newProduct = new Product()
            {
                Id = id,


            };
            _partyProductDbContext.Product.Remove(newProduct);
            await _partyProductDbContext.SaveChangesAsync();
            return true;
            //}


        }
    }
}
