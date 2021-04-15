using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AandelenApplicatie.Data.Poco
{
    public class Company
    {
        [Required]
        public int CompanyId { get; set; }

        [Required]
        public string CompanyName { get; set; }

        #region Relations

        public StockList StockList { get; set; }

        #endregion Relations
    }
}