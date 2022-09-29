using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise_03.Models
{
    public class ProductModel
    {
        public long Id { get; set; }

        [Display (Name ="Product Name")]
        [Required (ErrorMessage ="Enter Product name")]
        public string productName { get; set; }
    }
}
