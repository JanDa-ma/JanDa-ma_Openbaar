using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AandelenApplicatie.Data.Poco
{
    public class Stock
    {
        [Required]
        public int StockId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        //The probability that a stock will rise or fall
        [Required]
        public int ValueChance { get; set; }

        //The value with which a stock rises or falls
        [Required]
        public int ChangeValue { get; set; }

        [Required]
        public bool IsSold { get; set; } = false;

        [Required]
        public bool IsDeleted { get; set; }

        #region Relations

        //==Price
        //A stock has multiple prices

        public IEnumerable<Price> Prices { get; set; }

        //==StockList
        //A stock belongs to 1 StockList
        public int StockListId { get; set; }

        public StockList StockList { get; set; }

        //==Ownership
        //A stock can have multiple ownerships

        public IEnumerable<Ownership> Ownership { get; set; }

        #endregion Relations
    }
}