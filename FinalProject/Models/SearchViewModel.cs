using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class SearchViewModel
    {
        [Display(Name = "Zip Code")]
        public string PostalCode { get; set; }
    }
}
