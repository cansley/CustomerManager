using CustomerWebMgrPOC.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerWebMgrPOC.Models
{
    public class CustomerListModel
    {
        public List<ICustomer> Customers { get; set; }
    }
}