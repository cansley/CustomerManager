using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerWebMgrPOC.Interfaces
{
    public interface ICustomer
    {
        int CustomerId { get; set; }
        string FirstName { get; set; }
        string MiddleName { get; set; }
        string LastName { get; set; }
        IAddress Address { get; set; }
    }
}
