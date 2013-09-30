using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerWebMgrPOC.Interfaces;
using CustomerWebMgrPOC.Enums;

namespace DataManagers
{
    public class OperationalResult : IOperationResult
    {
        public OperationResultStatus Result { get; set; }

        public string InternalErrorMessage { get; set; }

        public string ExternalErrorMessage { get; set; }
    }
}
