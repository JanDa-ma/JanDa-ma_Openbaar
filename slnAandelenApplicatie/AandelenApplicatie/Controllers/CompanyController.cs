using AandelenApplicatie.Data.Context;
using AandelenApplicatie.Models.Company.BindingModels;
using AandelenApplicatie.Models.Company.ViewModels;
using AandelenApplicatie.Models.Message;
using AandelenApplicatie.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AandelenApplicatie.Controllers
{
    public class CompanyController : Controller
    {
        #region Actions

        #region StockList

        /// <summary>
        /// Gives a list of all stocklist that belongs to the company.
        /// </summary>
        /// <param name="id">Id of the company</param>
        /// <returns>IEnumerable list of stocklists on view</returns>
        public IActionResult Overview(int id)
        {
            CompanyOverviewViewModel model = _service.GetStockLists(id);
            return View(model);
        }

        public IActionResult DetailsStockList(int id)
        {
            StockListViewModel model = _service.GetStockListById(id);

            return View(model);
        }

        #endregion StockList

        #region Stock

        /// <summary>
        /// This method delete a stock.
        /// It sets the delete property of the given stock on true
        /// </summary>
        /// <param name="id">Id of the stock</param>
        /// <returns>View with message</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteStock(int id)
        {
            _service.DeleteStock(id);
            //Succesfully deleted message for view
            Message message = new Message()
            {
                Title = "Stock deleted!",
                Description = $"The stock with id {id} is succesfully deleted!",
                Type = TypeMessage.Succes
            };
            return RedirectToAction(nameof(ActionMessage), message);
        }

        public IActionResult DetailsStock(int id)
        {
            StockModel model = _service.GetStockById(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditStock(StockModel model)
        {
            if (ModelState.IsValid)
            {
                _service.EditStock(model);
                Message message = new Message()
                {
                    Title = $"{model.Name} edited!",
                    Description = $"The stock {model.Name} is succesfully edited",
                    CompanyId = model.OwnershipId,
                    Type = TypeMessage.Succes
                };
                return View("/Views/ActionMessage/ActionMessage.cshtml", message);
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id">id of stocklist</param>
        /// <returns></returns>
        public IActionResult CreateStock(int id)
        {
            StockModel model = new StockModel()
            {
                StockListId = id
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateStock(StockModel model)
        {
            if (ModelState.IsValid)
            {
                double ha = Convert.ToDouble(model.ValueChance);
                _service.AddStock(model);
                return RedirectToAction(nameof(Overview));
            }
            return View(model);
        }

        #endregion Stock

        public IActionResult ActionMessage(Message message)
        {
            return View("/Views/ActionMessage/ActionMessage.cshtml", message);
        }

        #endregion Actions

        #region Constructor

        /// <summary>
        /// The constructor of the company controller makes a new context.
        /// It also makes a service.
        /// </summary>
        /// <param name="context">The db context</param>
        public CompanyController(ICompanyService service)
        {
            _service = service;
        }

        private readonly ICompanyService _service;

        #endregion Constructor
    }
}