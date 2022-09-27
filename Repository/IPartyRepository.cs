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
        Task<int> AddParty(partyModel model);
        Task<int> UpdateParty(partyModel model, int id);
        Task<bool> DeleteParty(partyModel model, int id);
    }
}
