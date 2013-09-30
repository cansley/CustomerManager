using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerWebMgrPOC.Interfaces
{
    public interface IAddress
    {
        string Address1 { get; set; }
        string Address2 { get; set; }
        string Address3 { get; set; }
        string Address4 { get; set; }
        string City { get; set; }
        string StateOrProvince { get; set; }
        string Country { get; set; }
        string PostalCode { get; set; }
    }
}
