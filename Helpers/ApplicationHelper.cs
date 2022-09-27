using AutoMapper;
using Exercise_03.Data;
using Exercise_03.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise_03.Helpers
{
    public class ApplicationHelper:Profile
    {
        public ApplicationHelper()
        {
            CreateMap<Party, partyModel>().ReverseMap();
            CreateMap<Product, ProductModel>().ReverseMap();
            CreateMap<AssignParty, AssignPartyModel>().ReverseMap();
            CreateMap<ProductRate, ProductRateModel>().ReverseMap();
            CreateMap<Invoice, InvoiceModel>().ReverseMap();

        }

    }
}


  
       
 
