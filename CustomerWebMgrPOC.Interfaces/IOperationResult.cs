using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustomerWebMgrPOC.Enums;

namespace CustomerWebMgrPOC.Interfaces
{
    public interface IOperationResult
    {
        OperationResultStatus Result { get; set; }
        String InternalErrorMessage { get; set; }
        String ExternalErrorMessage { get; set; }
    }
}
