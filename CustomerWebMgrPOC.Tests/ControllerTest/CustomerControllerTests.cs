using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using DataManagers;
using CustomerWebMgrPOC.Controllers;
using CustomerWebMgrPOC.Interfaces;
using CustomerWebMgrPOC.Enums;
using System.Web.UI.WebControls;
using System.Web.Mvc;
using CustomerWebMgrPOC.Models;

namespace CustomerWebMgrPOC.Tests.ControllerTest
{
    [TestClass]
    public class CustomerControllerTests
    {
        private List<ICustomer> _testCustomers;
        private string _connString = @"data source=..\..\..\CustomerWebMgrPOC\App_Data\Customers.db;version=3";

        [TestInitialize]
        public void initialize()
        {
            _testCustomers = new List<ICustomer>();
            for (int x = 1; x < 11; x++)
            {
                _testCustomers.Add(new Customer()
                {
                    CustomerId = x,
                    FirstName = String.Format("Customer {0}", x),
                    MiddleName = String.Format("Mid {0}", x),
                    LastName = String.Format("Last {0}", x),
                    Address = new Address() { 
                        Address1 = String.Format("Address {0}", x),
                        City = String.Format("City {0}", x),
                        StateOrProvince = String.Format("State {0}", x),
                        Country = String.Format("US"),
                        PostalCode = x.ToString()
                    }
                });
            }
        }

        [TestMethod]
        public void TestCustomerLoad()
        {
            CustomerManagerBase dataManger = new CustomerMemManager(_testCustomers, AllowDuplicates.Allow);
            CustomerController custController = new CustomerController((IDataManager<ICustomer>)dataManger);

            ViewResult result = custController.Index() as ViewResult;
            CustomerListModel model = result.Model as CustomerListModel;

            Assert.AreEqual(10, model.Customers.Count);
        }

        [TestMethod]
        public void TestCustomerDBLoad()
        {
            CustomerDBManager dbMgr = new CustomerDBManager(AllowDuplicates.Allow, _connString);
            CustomerController custController = new CustomerController((IDataManager<ICustomer>)dbMgr);

            ViewResult result = custController.Index() as ViewResult;
            CustomerListModel model = result.Model as CustomerListModel;

            Assert.AreEqual(6, model.Customers.Count);
        }
    }
}
