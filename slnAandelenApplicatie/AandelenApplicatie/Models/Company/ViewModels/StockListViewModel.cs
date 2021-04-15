using AandelenApplicatie.Data.Poco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AandelenApplicatie.Models.Company.ViewModels
{
    public class StockListViewModel
    {
        public int StockListId { get; set; }

        [Required]
        public string Name { get; set; }

        public IEnumerable<Stock> Stocks { get; set; }
    }
}