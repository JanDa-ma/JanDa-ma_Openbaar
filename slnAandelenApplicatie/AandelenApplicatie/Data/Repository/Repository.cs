using AandelenApplicatie.Data.Context;
using AandelenApplicatie.Data.Poco;
using AandelenApplicatie.Models.Company.BindingModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AandelenApplicatie.Data.Repository
{
    public class Repository : IRepository
    {
        private ApplicationDbContext _context; //No using due to services.AddScopde in startup (dependency injection)

        #region StockList

        /// <summary>
        /// Gets all the stocklists with the stocks
        /// </summary>
        /// <param name="id">id of the company</param>
        /// <returns>All stocklists of the company</returns>
        public IEnumerable<StockList> GetStockLists(int id)
        {
            var stockLists = _context.StockLists.Include(x => x.Stocks).Select(x => new StockList()
            {
                CompanyId = x.CompanyId,
                Name = x.Name,
                Stocks = x.Stocks.Where(x => x.IsDeleted == false)
            });
            return stockLists;
        }

        public StockList GetStockListById(int id)
        {
            return _context.StockLists.Where(_stockList => _stockList.StockListId == id).Include(stocklist => stocklist.Stocks).FirstOrDefault();
        }

        #endregion StockList

        //===Price
        public int AddPrice(int stockId, int valuePrice)
        {
            Price price = new Price()
            {
                DatePrice = DateTime.Now,
                StockId = stockId,
                ValuePrice = valuePrice,
            };

            _context.Prices.Add(price);
            _context.SaveChanges();

            return price.PriceId;
        }

        #region Stock

        public int AddStock(StockModel model, int priceValue)
        {
            Stock stock = new Stock()
            {
                Name = model.Name,
                ChangeValue = model.ChangeValue,
                Description = model.Description,
                StockListId = model.StockListId,
                ValueChance = model.ValueChance,
                IsDeleted = model.IsDeleted,
                IsSold = model.IsSold,
            };
            _context.Stocks.Add(stock);
            _context.SaveChanges();

            Price price = new Price()
            {
                DatePrice = DateTime.Now,
                StockId = stock.StockId,
                ValuePrice = priceValue,
            };
            _context.Prices.Add(price);
            _context.SaveChanges();

            return stock.StockId;
        }

        public void DeleteStock(int stockid)
        {
            Stock stock = _context.Stocks.FirstOrDefault(stock => stock.StockId == stockid);
            stock.IsDeleted = true;
            _context.SaveChanges();
        }

        public Stock GetStockById(int id)
        {
            Stock stock = _context.Stocks.Include(stock => stock.StockList).FirstOrDefault(x => x.StockId == id);
            return stock;
        }

        public void EditStock(StockModel model)
        {
            Stock stock = _context.Stocks.FirstOrDefault(stock => stock.StockId == model.StockId);
            stock.Name = model.Name;
            stock.Description = model.Description;
            stock.ValueChance = model.ValueChance;
            stock.ChangeValue = model.ChangeValue;
            _context.Stocks.Update(stock);
            _context.SaveChanges();
        }

        #endregion Stock

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}