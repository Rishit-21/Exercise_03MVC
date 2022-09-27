using Exercise_03.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise_03.Models
{
    public class AssignPartyModel
    {
        public long Id { get; set; }

        [Display (Name ="Party")]
        [Required]
        [ForeignKey("Party")]
        public long PartyId { get; set; }


        [Display(Name = "Product")]
        [Required]
        [ForeignKey("Product")]
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public Party Party { get; set; }

        public Product Product { get; set; }
    }
}
