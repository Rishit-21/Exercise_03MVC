using Exercise_03.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise_03.Repository
{
   public interface IPartyAssignRepository
    {
        Task<List<AssignPartyModel>> GetAllAssignParty();
        Task<long> AddAssignParty(AssignPartyModel model);
        Task<long> UpdateAssignParty(AssignPartyModel model, long id);
        Task<bool> DeleteAssignParty(AssignPartyModel model, long id);
        Task<List<AssignPartyModel>> GetAllAssignUniqueParty();
        Task<List<ProductModel>> getNotAssigendProduct(long partyId);

    }
}
