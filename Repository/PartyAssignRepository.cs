using AutoMapper;
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
    public class PartyAssignRepository : IPartyAssignRepository
    {
        private readonly PartyProductDbContext _partyProductDbContext;

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public PartyAssignRepository(PartyProductDbContext partyProductDbContext, IConfiguration configuration, IMapper mapper)
        {
            _partyProductDbContext = partyProductDbContext;

            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<List<AssignPartyModel>> GetAllAssignParty()
        {

            var records = await _partyProductDbContext.AssignParty.Include(x => x.Party).Include(x => x.Product).ToListAsync();
            return _mapper.Map<List<AssignPartyModel>>(records);
        }

        public async Task<int> AddAssignParty(AssignPartyModel model)
        {
            var y = _partyProductDbContext.AssignParty
                  .Where(x => x.PartyId == model.PartyId && x.ProductId == model.ProductId).FirstOrDefault();

            if (y == null)
            {
                var AssignParty = new AssignParty()
                {
                    PartyId = model.PartyId,
                    ProductId = model.ProductId

                };
                await _partyProductDbContext.AssignParty.AddAsync(AssignParty);
                await _partyProductDbContext.SaveChangesAsync();
                return AssignParty.Id;
            }

            return 0;
            //var AssignParty = _mapper.Map<AssignParty>(model);
        }

        public async Task<int> UpdateAssignParty(AssignPartyModel model, int id)
        {
            var y = _partyProductDbContext.AssignParty
                 .Where(x => x.PartyId == model.PartyId && x.ProductId == model.ProductId).FirstOrDefault();

            if (y == null)
            {
            var newAssignParty = new AssignParty()
            {
                Id = id,
                PartyId = model.PartyId,
                ProductId = model.ProductId


            };
            _partyProductDbContext.AssignParty.Update(newAssignParty);
            await _partyProductDbContext.SaveChangesAsync();
                return 1;
            }
            return 0;


        }

        public async Task<bool> DeleteAssignParty(AssignPartyModel model, int id)
        {
            //if (id == model.Id)
            //{

            var AssignParty = new AssignParty()
            {
                Id = id,


            };
            _partyProductDbContext.AssignParty.Remove(AssignParty);
            await _partyProductDbContext.SaveChangesAsync();
            return true;
            //}
        }
        public async Task<List<AssignPartyModel>> GetAllAssignUniqueParty()
        {
            return await _partyProductDbContext.AssignParty.Select(partyAssign => new AssignPartyModel()
            {
                PartyId = partyAssign.PartyId,
                Party = partyAssign.Party
            }
             ).Distinct().ToListAsync();

          
        }
        public async Task<List<ProductModel>> getNotAssigendProduct(int partyId)
        {
            var products = await _partyProductDbContext.Product.Except(_partyProductDbContext.AssignParty.Where(x => x.PartyId == partyId).Include(x => x.Product).Select(x => x.Product)).ToListAsync();
          
            return _mapper.Map<List<ProductModel>>(products);

           
        }
    }
}
