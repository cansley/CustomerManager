using CustomerWebMgrPOC.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerWebMgrPOC.Models
{
    public class CustomerModel
    {
        public ICustomer Customer { get; set; }
    }
}