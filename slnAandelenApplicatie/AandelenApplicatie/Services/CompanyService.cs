using AandelenApplicatie.Data.Context;
using AandelenApplicatie.Data.Poco;
using AandelenApplicatie.Data.Repository;
using AandelenApplicatie.Models.Company.BindingModels;
using AandelenApplicatie.Models.Company.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AandelenApplicatie.Services
{
    //Bij Get een POCO
    //Bij post een viewmodel
    public class CompanyService : ICompanyService
    {
        private readonly IRepository repository;

        public CompanyService(IRepository _repository)
        {
            repository = _repository;
        }

        #region StockList

        public CompanyOverviewViewModel GetStockLists(int id)
        {
            CompanyOverviewViewModel model = new CompanyOverviewViewModel()
            {
                StockList = repository.GetStockLists(id)
            };
            return model;
        }

        //===StockList
        public StockListViewModel GetStockListById(int id)
        {
            var stocklist = repository.GetStockListById(id);
            StockListViewModel model = new StockListViewModel()
            {
                Name = stocklist.Name,
                Stocks = stocklist.Stocks,
                StockListId = stocklist.StockListId
            };

            return model;
        }

        #endregion StockList

        #region Stock

        public void AddStock(StockModel model)
        {
            Random random = new Random();
            double randomNumber = random.Next(0, 100);
            int priceValue;
            //--Verify calculate chance to increase
            //Decrease
            if (randomNumber > Convert.ToDouble(model.ValueChance)) { priceValue = 100 - model.ChangeValue; }
            //Increase
            else { priceValue = 100 + model.ChangeValue; }
            repository.AddStock(model, priceValue);
        }

        public void DeleteStock(int id)
        {
            repository.DeleteStock(id);
        }

        public void EditStock(StockModel model)
        {
            repository.EditStock(model);
        }

        public StockModel GetStockById(int id)
        {
            Stock stock = repository.GetStockById(id);
            StockModel model = new StockModel()
            {
                Name = stock.Name,
                IsSold = stock.IsSold,
                StockId = stock.StockId,
                IsDeleted = stock.IsDeleted,
                ChangeValue = stock.ChangeValue,
                Description = stock.Description,
                ValueChance = stock.ValueChance
            };
            return model;
        }

        #endregion Stock

        //===Constructor
    }
}