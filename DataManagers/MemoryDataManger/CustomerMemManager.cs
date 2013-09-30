using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerWebMgrPOC.Enums;
using CustomerWebMgrPOC.Interfaces;

namespace DataManagers
{
    public class CustomerMemManager : CustomerManagerBase
    {
        

        #region Constructors
        public CustomerMemManager(AllowDuplicates allowDupes) : base(allowDupes) { }        
        public CustomerMemManager(List<ICustomer> customers, AllowDuplicates allowDupes) : this(allowDupes)
        {
            _customers = customers;
        }
        #endregion


    }
}
