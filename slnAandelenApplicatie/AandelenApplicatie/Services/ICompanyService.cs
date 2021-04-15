using AandelenApplicatie.Models.Company.BindingModels;
using AandelenApplicatie.Models.Company.ViewModels;

namespace AandelenApplicatie.Services
{
    public interface ICompanyService
    {
        void AddStock(StockModel model);
        void DeleteStock(int id);
        void EditStock(StockModel model);
        StockModel GetStockById(int id);
        StockListViewModel GetStockListById(int id);
        CompanyOverviewViewModel GetStockLists(int id);
    }
}