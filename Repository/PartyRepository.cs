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
    public class PartyRepository : IPartyRepository
    {
        private readonly PartyProductDbContext _partyProductDbContext;
        private readonly IConfiguration _configuration;

        public PartyRepository(PartyProductDbContext partyProductDbContext, IConfiguration configuration)
        {
            _partyProductDbContext = partyProductDbContext;
            _configuration = configuration;
        }

        public async Task<List<partyModel>> GetAllParties()
        {
            return await _partyProductDbContext.Party.Select(party => new partyModel()
            {
                partyName = party.partyName,
                Id = party.Id,
            }).ToListAsync();
        }

        public async Task<int> AddParty(partyModel model)
        {

            var y = _partyProductDbContext.Party
                .Where(x => x.partyName == model.partyName).FirstOrDefault();
            if (y == null)
            {
                var newParty = new Party()
                {
                    partyName = model.partyName,

                };
                await _partyProductDbContext.Party.AddAsync(newParty);
                await _partyProductDbContext.SaveChangesAsync();
                return newParty.Id;
            }
            return 0;

        } 

       
        public async Task<int> UpdateParty(partyModel model, int id)
        {
            var y = _partyProductDbContext.Party
        .Where(x => x.partyName == model.partyName).FirstOrDefault();

            if (y == null)
            {
                var newParty = new Party()
                {
                    Id = id,
                    partyName = model.partyName,

                };


                _partyProductDbContext.Party.Update(newParty);
                await _partyProductDbContext.SaveChangesAsync();
                return 1;

            }
            return 0;


        }

        public async Task<bool> DeleteParty(partyModel model, int id)
        {
            //if (id == model.Id)
            //{

            var newParty = new Party()
            {
                Id = id,


            };
            _partyProductDbContext.Party.Remove(newParty);
            await _partyProductDbContext.SaveChangesAsync();
            return true;
            //}


        }
    }
}
