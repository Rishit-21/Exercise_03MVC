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
        Task<int> AddAssignParty(AssignPartyModel model);
        Task<int> UpdateAssignParty(AssignPartyModel model, int id);
        Task<bool> DeleteAssignParty(AssignPartyModel model, int id);
        Task<List<AssignPartyModel>> GetAllAssignUniqueParty();
        Task<List<ProductModel>> getNotAssigendProduct(int partyId);

    }
}
