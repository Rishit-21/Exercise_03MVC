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
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly PartyProductDbContext _partyProductDbContext;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public InvoiceRepository(PartyProductDbContext partyProductDbContext, IConfiguration configuration, IMapper mapper)
        {
            _partyProductDbContext = partyProductDbContext;
            _configuration = configuration;
            _mapper = mapper;
        }


        public async Task<List<AssignPartyModel>> BindProductAsync(long _PartyId)
        {

            return await _partyProductDbContext.AssignParty.Where(x => x.PartyId == _PartyId).
                 Select(product => new AssignPartyModel()
                 {
                     ProductName = product.Product.productName,
                     ProductId = product.ProductId
                 }
                 ).Distinct().ToListAsync();

        }
        public async Task<double> BindRate(long productId)
        {
            var rates = await _partyProductDbContext.ProductRate.Where(x => x.ProductId == productId).Select(x => x.ProductRates).FirstOrDefaultAsync();
            return rates;
        }

        public async Task<List<InvoiceModel>> ShowInvoice(long id)
        {



            return await _partyProductDbContext.Invoice.Where(x=>x.partyId==id).Select(invoice => new InvoiceModel()
            {
                Id = invoice.Id,
                partyId = invoice.partyId,
                partyName = _partyProductDbContext.Party.Where(x => x.Id == invoice.partyId).FirstOrDefault().partyName,
                productId = invoice.productId,
                productName = _partyProductDbContext.Product.Where(x => x.Id == invoice.productId).FirstOrDefault().productName,
                CurrentRate = invoice.CurrentRate,
                Quantity = invoice.Quantity,
                Total = (invoice.Quantity * invoice.CurrentRate)

            }).ToListAsync();
        }
        public async Task<long> AddInvoice(InvoiceModel model, long PId)
        {

            var Invoices = new Invoice()
            {
                //Id = model.Id,
                partyId = model.partyId,
                productId = model.productId,
                CurrentRate = model.CurrentRate,
                Quantity = model.Quantity,
                Total = model.CurrentRate * model.Quantity

            };

            await _partyProductDbContext.Invoice.AddAsync(Invoices);
            await _partyProductDbContext.SaveChangesAsync();

            long grandTotal = (from x in _partyProductDbContext.Invoice.Where(x=>x.partyId==PId) select x.Total).Sum();

            return grandTotal;

        }

    }
}
