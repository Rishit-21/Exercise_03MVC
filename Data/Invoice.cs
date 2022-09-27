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
        public int Id { get; set; }

        [Display(Name = "Party")]
        [Required]
        //[ForeignKey("Party")]
        public int partyId { get; set; }

        [Display(Name = "Product")]
        [Required]
        //[ForeignKey("Product")]
        public int productId { get; set; }

        [Required]
        [Display(Name = "Current Rate")]
        public int CurrentRate { get; set; }

        [Required]
        public int Quantity { get; set; }

        public int Total { get; set; }


        //public Party Party { get; set; }

        //public Product Product { get; set; }
    }
}
