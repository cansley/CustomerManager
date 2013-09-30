using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerWebMgrPOC.Enums;
using CustomerWebMgrPOC.Interfaces;

namespace DataManagers
{
    public class CustomerManagerBase : IDataManager<ICustomer>
    {
        #region Fields
        protected List<ICustomer> _customers;
        protected AllowDuplicates _allowDupes;
        #endregion

        #region Constructors
        public CustomerManagerBase(AllowDuplicates allowDupes)
        {
            _allowDupes = allowDupes;
        }
        #endregion

        #region Properties
        public IReadOnlyList<ICustomer> Customers
        {
            get { return _customers.AsReadOnly(); }
        }

        /// <summary>
        /// Indicates how duplicate entries should be handled for Add operations.
        /// </summary>
        public AllowDuplicates AllowDuplicates { get { return _allowDupes; } }
        #endregion

        #region Private Methods
        /// <summary>
        /// Checks for an exact matching customer. Do not use for searches on partial match.
        /// </summary>
        /// <param name="customer">Customer object to look for.</param>
        /// <returns>true if the customer is within the collection</returns>
        private bool customerExists(ICustomer customer)
        {
            return _customers.Contains(customer);
        }
        #endregion

        #region Public Methods
        public ICustomer GetById(int customerId)
        {
            return _customers.Find(cust => cust.CustomerId == customerId);
        }

        /// <summary>
        /// Add a new customer to the collection.
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
        public IOperationResult Add(ICustomer Item)
        {
            IOperationResult result = new OperationalResult();

            try
            {
                // double check that the item has an Id. if it doesnt, create one.
                if (Item.CustomerId <= 0)
                {
                    int nextVal = (_customers.Count == 0) ? 1 : _customers.OrderByDescending(cust => cust.CustomerId).First().CustomerId + 1;
                    Item.CustomerId = nextVal;
                }


                int itemLOC = -1;

                try
                {
                    itemLOC = _customers.FindIndex(x => x.CustomerId == Item.CustomerId);
                }
                catch
                { // thows an exception if no matching object found 
                }

                switch (_allowDupes)
                {
                    case AllowDuplicates.Strong:
                        if (itemLOC >= 0)
                        {
                            result.Result = OperationResultStatus.Fail;
                            result.ExternalErrorMessage = "Duplicate customer exists.";
                            result.InternalErrorMessage = "Duplicates not allowed.";
                            return result;
                        }
                        break;
                    case AllowDuplicates.Weak:
                        if (itemLOC >= 0)
                        {
                            _customers[itemLOC] = Item;
                            result.Result = OperationResultStatus.Success;
                            return result;
                        }
                        break;
                }

                _customers.Add(Item);
            }
            catch (Exception ex)
            {
                result.Result = OperationResultStatus.Fail;
                result.ExternalErrorMessage = "Unable to add customer.";
                result.InternalErrorMessage = ex.Message;
            }

            return result;
        }

        public IOperationResult Remove(int customerId)
        {
            IOperationResult result = new OperationalResult();
            try
            {
                _customers.RemoveAll(z => z.CustomerId == customerId);
                result.Result = OperationResultStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = OperationResultStatus.Fail;
                result.ExternalErrorMessage = "Unable to remove customer entry;";
                result.InternalErrorMessage = ex.Message;
            }

            return result;
        }

        public IOperationResult Update(ICustomer Item)
        {
            IOperationResult result = new OperationalResult();

            try
            {
                int itemLOC = _customers.FindIndex(cust => cust.CustomerId == Item.CustomerId);
                if (itemLOC >= 0)
                {
                    _customers[itemLOC] = Item;
                    result.Result = OperationResultStatus.Success;
                }
                else
                {
                    result.Result = OperationResultStatus.Fail;
                    result.ExternalErrorMessage = "Customer does not exist";
                    result.InternalErrorMessage = "Referenced customer Id does not exist in customer collection.";
                }
            }
            catch (Exception ex)
            {
                result.Result = OperationResultStatus.Fail;
                result.ExternalErrorMessage = "Unable to update customer entry;";
                result.InternalErrorMessage = ex.Message;
            }

            return result;
        }

        public virtual IOperationResult Persist()
        {
            return new OperationalResult() { Result = OperationResultStatus.Success };
        }
        #endregion
    }
}
