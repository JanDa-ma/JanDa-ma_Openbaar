using AandelenApplicatie.Data.Poco;
using AandelenApplicatie.Models.Company.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AandelenApplicatie.Data.Repository
{
    public interface IRepository
    {
        int AddPrice(int stockId, int valuePrice);

        int AddStock(StockModel model, int priceValue);

        void DeleteStock(int stockid);

        void EditStock(StockModel model);

        Stock GetStockById(int id);

        StockList GetStockListById(int id);

        IEnumerable<StockList> GetStockLists(int id);
    }
}