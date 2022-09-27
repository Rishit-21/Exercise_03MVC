using Exercise_03.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise_03.Models
{
    public class InvoiceModel
    {
        public long Id { get; set; }

        [Display(Name = "Party")]
        [Required]
        //[ForeignKey("Party")]
        public long partyId { get; set; }

     
        public string partyName { get; set; }

      
        public string productName { get; set; }

        [Display(Name = "Product")]
        [Required]
        //[ForeignKey("Product")]
        public long productId { get; set; }

        [Required]
        [Display(Name = "Current Rate")]
       
        public long CurrentRate { get; set; }

        [Required]
        [Range(1,100)]
        public long Quantity { get; set; }

        public long Total { get; set; }

        //public Party Party { get; set; }

        //public Product Product { get; set; }
    }
}
