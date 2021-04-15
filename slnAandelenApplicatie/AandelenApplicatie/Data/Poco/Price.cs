using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AandelenApplicatie.Data.Poco
{
    public class Price
    {
        [Required]
        public int PriceId { get; set; }

        [Required]
        public int ValuePrice { get; set; }

        [Required]
        public DateTime DatePrice { get; set; }

        #region Relations

        //A price belongs to 1 stock
        [Required]
        public int StockId { get; set; }

        public Stock Stock { get; set; }

        #endregion Relations
    }
}