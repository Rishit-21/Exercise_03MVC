using AutoMapper;
using AutoMapper.Configuration;
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
    public class ProductRateRepository : IProductRateRepository
    {
        private readonly PartyProductDbContext _partyProductDbContext;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public ProductRateRepository(PartyProductDbContext partyProductDbContext, IConfiguration configuration, IMapper mapper)
        {
            _partyProductDbContext = partyProductDbContext;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<List<ProductRateModel>> GetAllProductRate()
        {
            var records = await _partyProductDbContext.ProductRate.Include(x => x.Product).ToListAsync();

            return _mapper.Map<List<ProductRateModel>>(records);
        }

        public async Task<long> AddProductRate(ProductRateModel model)
        {
            //var  =  _mapper.Map<ProductRate>(model);

            var y = _partyProductDbContext.ProductRate
               .Where(x => x.ProductId == model.ProductId).FirstOrDefault();

            if (y == null)
            {
                var AddProductRate = new ProductRate()
                {
                    ProductRates = model.ProductRates,
                    ProductId = model.ProductId,
                    DateOfRate = model.DateOfRate


                };

                await _partyProductDbContext.ProductRate.AddAsync(AddProductRate);
                await _partyProductDbContext.SaveChangesAsync();

                return AddProductRate.Id;
            }
            return 0;

        }

        public async Task<long> UpdateProductRate(ProductRateModel model, long id)
        {
            var y = _partyProductDbContext.ProductRate
                 .Where(x => x.ProductId == model.ProductId && x.ProductRates==model.ProductRates).FirstOrDefault();
            if (y==null)
            {
            var UpdateProductRate = new ProductRate()
            {
                Id = id,
                ProductRates = model.ProductRates,
                DateOfRate = DateTime.Now,
                ProductId = model.ProductId


            };
            _partyProductDbContext.ProductRate.Update(UpdateProductRate);
            await _partyProductDbContext.SaveChangesAsync();
                return 1;

            }
            return 0;
        }

        public async Task<bool> DeleteProductRate(ProductRateModel model, long id)
        {
            //if (id == model.Id)
            //{

            var productRate = new ProductRate()
            {
                Id = id,


            };
            _partyProductDbContext.ProductRate.Remove(productRate);
            await _partyProductDbContext.SaveChangesAsync();
            return true;
            //}
        }
    }
}
