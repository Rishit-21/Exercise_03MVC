using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise_03.Models
{
    public class partyModel
    {
        public long Id { get; set; }

        [Display(Name ="Party Name")]
        [Required(ErrorMessage ="Enter party Name")]
        public string partyName { get; set; }
    }
}
