using Exercise_03.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise_03.Repository
{
   public interface IPartyRepository
    {
        Task<List<partyModel>> GetAllParties();
        Task<long> AddParty(partyModel model);
        Task<long> UpdateParty(partyModel model, long id);
        Task<bool> DeleteParty(partyModel model, long id);
    }
}
