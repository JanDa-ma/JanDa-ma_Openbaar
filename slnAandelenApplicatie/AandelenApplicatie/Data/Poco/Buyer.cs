using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AandelenApplicatie.Data.Poco
{
    public class Buyer
    {
        [Required]
        public int BuyerId { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public double Balance { get; set; }

        public IEnumerable<Ownership> Ownerships { get; set; }
    }
}