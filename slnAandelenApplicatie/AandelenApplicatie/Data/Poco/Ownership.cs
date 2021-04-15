using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AandelenApplicatie.Data.Poco
{
    public class Ownership
    {
        [Required]
        public int OwnershipId { get; set; }

        [Required]
        public double PurchasePrice { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }

        public double SellPrice { get; set; }
        public DateTime SellDate { get; set; }

        #region Relations

        public int StockId { get; set; }

        public Stock Stock { get; set; }

        public int UserId { get; set; }

        public Buyer Buyer { get; set; }

        #endregion Relations
    }
}