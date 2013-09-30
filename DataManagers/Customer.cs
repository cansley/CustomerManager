using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerWebMgrPOC.Interfaces;

namespace DataManagers
{
    /// <summary>
    /// Represents the basic customer object.
    /// </summary>
    public class Customer : CustomerBase
    {
        public Customer() { }
        public Customer(int id) : base(id) {}

        new public Address Address
        {
            get { return (Address)base.Address; }
            set { base.Address = value; }
        }
    }
}
