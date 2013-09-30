using CustomerWebMgrPOC.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DataManagers
{
    [Serializable]
    public class CustomerBase : ICustomer
    {
        public CustomerBase() {
            Address = new Address();
        }
        public CustomerBase(int id) : this() { CustomerId = id; }

        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        
        [XmlIgnore]
        public IAddress Address { get; set; }
    }
}
