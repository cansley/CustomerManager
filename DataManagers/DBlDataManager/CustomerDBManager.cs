using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerWebMgrPOC.Interfaces;
using CustomerWebMgrPOC.Enums;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Data.SQLite;

namespace DataManagers
{
    //TODO: Build a data mapper between object/properties and db fields to make this somewhat less cumbersome on fetch/update
    public class CustomerDBManager : CustomerManagerBase
    {
        SQLiteConnection _conn;

        public CustomerDBManager(AllowDuplicates allowDupes, string ConnectionString)
            : base(allowDupes)
        {
            _conn = new SQLiteConnection(ConnectionString);
            _customers = loadCustomers();
        }

        /// <summary>
        /// Saves all changes back to the underlying data store.
        /// This is really heavy handed, in the real world, there should be a lot more delta checks etc.
        /// FOr the purposes of this POC, the list will be considered to be definitive.
        /// </summary>
        /// <returns></returns>
        public override IOperationResult Persist()
        {
            IOperationResult result = new OperationalResult();
            try
            {
                if (_conn.State != System.Data.ConnectionState.Open) _conn.Open();
                StringBuilder command = new StringBuilder();

                //here's the heavy hand...
                command.Append("delete from Customer;");
                
                string baseInsertString = "insert into Customer values({0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}'); ";
                foreach (Customer customer in _customers)
                {
                    command.AppendFormat(baseInsertString, customer.CustomerId, customer.FirstName, customer.MiddleName, customer.LastName,
                        customer.Address.Address1, customer.Address.Address2, customer.Address.Address3, customer.Address.Address4,
                        customer.Address.City, customer.Address.StateOrProvince, customer.Address.PostalCode);
                }

                SQLiteCommand cmd = new SQLiteCommand(command.ToString(), _conn);
                cmd.ExecuteNonQuery();
                _conn.Close();
                result.Result = OperationResultStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = OperationResultStatus.Fail;
                result.ExternalErrorMessage = "Unable to save change to data store.";
                result.InternalErrorMessage = ex.Message;
            }

            return result;
        }

        private List<ICustomer> loadCustomers()
        {
            List<ICustomer> customers = new List<ICustomer>();

            if (_conn.State != System.Data.ConnectionState.Open) _conn.Open();

            SQLiteCommand cmd = new SQLiteCommand("select * from customer", _conn);

            using (SQLiteDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    Customer record = new Customer(rdr.GetInt32(rdr.GetOrdinal("iCustomerId")))
                    {
                        FirstName = rdr.GetString(rdr.GetOrdinal("cFirstName")),
                        MiddleName = rdr.GetString(rdr.GetOrdinal("cMiddleName")),
                        LastName = rdr.GetString(rdr.GetOrdinal("cLastName")),
                        Address = new Address() {
                            Address1 = rdr.GetString(rdr.GetOrdinal("cAddress1")),
                            Address2 = rdr.GetString(rdr.GetOrdinal("cAddress2")),
                            Address3 = rdr.GetString(rdr.GetOrdinal("cAddress3")),
                            Address4 = rdr.GetString(rdr.GetOrdinal("cAddress4")),
                            City = rdr.GetString(rdr.GetOrdinal("cCity")),
                            StateOrProvince = rdr.GetString(rdr.GetOrdinal("cState")),
                            Country = rdr.GetString(rdr.GetOrdinal("cCountry")),
                            PostalCode = rdr.GetString(rdr.GetOrdinal("cPostalCode"))
                        }
                    };

                    customers.Add(record);
                }
            }

            _conn.Close();

            return customers;
        }
    }
}
