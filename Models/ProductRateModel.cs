using Exercise_03.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise_03.Models
{
    public class ProductRateModel
    {
        public int Id { get; set; }



        [Display(Name = "Product")]
        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [Display(Name = "Product Rate")]
        [Required]
        public double ProductRates { get; set; }

        [Display(Name = "Date of  Rate") ]
        [Required]
        
        public DateTime DateOfRate { get; set; }

        public Product Product { get; set; }

    }
}
