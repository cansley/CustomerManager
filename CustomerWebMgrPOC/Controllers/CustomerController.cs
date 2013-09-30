using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CustomerWebMgrPOC.Interfaces;
using CustomerWebMgrPOC.Enums;
using CustomerWebMgrPOC.Models;


namespace CustomerWebMgrPOC.Controllers
{
    public class CustomerController : Controller
    {
        private IDataManager<ICustomer> _dataManager;
        public CustomerController(IDataManager<ICustomer> dataManager)
        {
            _dataManager = dataManager;
        }


        //
        // GET: /Customer/
        public ActionResult Index()
        {
            CustomerListModel model = new CustomerListModel();
            model.Customers = _dataManager.Customers.ToList();
            return View(model);
        }


        #region View Actions
        public PartialViewResult Edit(int customerId)
        {
            CustomerModel model = new CustomerModel();
            model.Customer = _dataManager.GetById(customerId);

            return PartialView(model);
        }
        #endregion


        #region Commit Actions
        [HttpPost]
        public ActionResult Add(ICustomer customer)
        {
            IOperationResult result = _dataManager.Add(customer);

            return Json(new { success = result.Result.ToString(), message = result.ExternalErrorMessage });
        }

        [HttpPost]
        public ActionResult Delete(ICustomer customer)
        {
            IOperationResult result = _dataManager.Remove(customer);

            return Json(new { success = result.Result.ToString(), message = result.ExternalErrorMessage });
        }

        [HttpPost]
        public ActionResult Update(ICustomer customer)
        {
            IOperationResult result = _dataManager.Update(customer);

            return Json(new { success = result.Result.ToString(), message = result.ExternalErrorMessage });
        }

        [HttpPost]
        public ActionResult Persist()
        {
            IOperationResult result = _dataManager.Persist();

            return Json(new { success = result.Result.ToString(), message = result.ExternalErrorMessage });
        }
        #endregion
    }
}
