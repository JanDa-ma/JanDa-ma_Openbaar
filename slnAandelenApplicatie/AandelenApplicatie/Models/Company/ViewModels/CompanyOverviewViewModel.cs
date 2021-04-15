using AandelenApplicatie.Data.Poco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AandelenApplicatie.Models.Company.ViewModels
{
    public class CompanyOverviewViewModel
    {
        public IEnumerable<StockList> StockList { get; set; }
    }
}