using AandelenApplicatie.Data.Poco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AandelenApplicatie.Models.Company.BindingModels
{
    public class StockModel
    {
        public int StockId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int ValueChance { get; set; }

        [Required]
        public int ChangeValue { get; set; }

        [Required]
        public bool IsSold { get; set; } = false;

        [Required]
        public bool IsDeleted { get; set; } = false;

        #region Relations

        [Required]
        public int StockListId { get; set; }

        public StockList StockList { get; set; }
        public int OwnershipId { get; set; }

        public IEnumerable<Ownership> Ownership { get; set; }

        #endregion Relations
    }
}