using Exercise_03.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise_03.Repository
{
   public interface IInvoiceRepository
    {
        Task<List<AssignPartyModel>> BindProductAsync(long _PartyId);
        Task<double> BindRate(long productId);
        Task<List<InvoiceModel>> ShowInvoice(long id);
        Task<long> AddInvoice(InvoiceModel model, long PId);
    }
}
