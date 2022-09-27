using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise_03.Data
{
    public class Invoice
    {
        public long Id { get; set; }

        [Display(Name = "Party")]
        [Required]
        //[ForeignKey("Party")]
        public long partyId { get; set; }

        [Display(Name = "Product")]
        [Required]
        //[ForeignKey("Product")]
        public long productId { get; set; }

        [Required]
        [Display(Name = "Current Rate")]
        public long CurrentRate { get; set; }

        [Required]
        public long Quantity { get; set; }

        public long Total { get; set; }


        //public Party Party { get; set; }

        //public Product Product { get; set; }
    }
}
