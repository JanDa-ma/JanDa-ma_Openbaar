using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AandelenApplicatie.Data.Poco
{
    public class StockList
    {
        [Required]
        public int StockListId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool IsDeleted { get; set; } = false;

        #region Relations

        //A stocklist belongs to 1 company
        [Required]
        public int CompanyId { get; set; }

        public Company Company { get; set; }

        //A stocklist must have multiple stocks
        public IEnumerable<Stock> Stocks { get; set; }

        #endregion Relations
    }
}