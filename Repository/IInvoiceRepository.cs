using Exercise_03.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise_03.Repository
{
   public interface IInvoiceRepository
    {
        Task<List<AssignPartyModel>> BindProductAsync(int _PartyId);
        Task<double> BindRate(int productId);
        Task<List<InvoiceModel>> ShowInvoice(int id);
        Task<int> AddInvoice(InvoiceModel model,int PId);
    }
}
