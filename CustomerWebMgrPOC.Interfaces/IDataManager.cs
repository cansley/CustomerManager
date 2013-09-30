using CustomerWebMgrPOC.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerWebMgrPOC.Interfaces
{
    public interface IDataManager<T>
    {
        AllowDuplicates AllowDuplicates { get; }
        IReadOnlyList<T> Customers { get; }

        T GetById(int id);
        IOperationResult Add(T Item);
        IOperationResult Remove(int customerId);
        IOperationResult Update(T Item);
        IOperationResult Persist();
    }
}
