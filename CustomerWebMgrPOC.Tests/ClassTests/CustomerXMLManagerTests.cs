using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomerWebMgrPOC.Interfaces;
using CustomerWebMgrPOC.Enums;
using DataManagers;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Data.SQLite;
using System.Text;

namespace CustomerWebMgrPOC.Tests.ClassTests
{
    [TestClass]
    public class CustomerDBManagerTests
    {
        private string _connString = @"data source=..\..\..\CustomerWebMgrPOC\App_Data\Customers.db;version=3";

        [TestMethod]
        public void CreateDefaultDatabaseRecords()
        {
            SQLiteConnectionStringBuilder connBldr = new SQLiteConnectionStringBuilder();
            connBldr.DataSource = @"..\..\..\CustomerWebMgrPOC\App_Data\Customers.db";
            connBldr.Version = 3;

            using (SQLiteConnection conn = new SQLiteConnection(_connString))
            {
                conn.Open();
                SQLiteCommand cmd;
                
                // Drop customer and address tables.
                try
                {
                    cmd = new SQLiteCommand(@"DROP TABLE Customer;", conn);
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    // can ignore this error as it probably means the table doesnt exist yet.
                }


                // create customer table.
                StringBuilder cmdText = new StringBuilder();
                cmdText.Append("create table Customer(iCustomerId int, cFirstName varchar(50), cMiddleName varchar(50), cLastName varchar(50), ");
                cmdText.Append("cAddress1 varchar(100), cAddress2 varchar(100), cAddress3 varchar(100), cAddress4 varchar(100), ");
                cmdText.Append("cCity varchar(100), cState varchar(100), cCountry varchar(100), cPostalCode varchar(10)");
                cmdText.Append(");");
                cmd = new SQLiteCommand(cmdText.ToString(), conn);
                cmd.ExecuteNonQuery();

                // insert customer records
                string baseInsertString = "insert into Customer values({0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}'); ";
                cmdText = new StringBuilder();
                cmdText.AppendFormat(baseInsertString, "1", "Eric", "", "Clapton", "123 Main St.", "", "", "", "London", "England", "UK", "123-234");
                cmdText.AppendFormat(baseInsertString, "2", "Joe", "", "Satriani", "123 Main St.", "", "", "", "Los Angeles", "CA", "US", "12345-1234");
                cmdText.AppendFormat(baseInsertString, "3", "Steve", "", "Vai", "123 Main St.", "", "", "", "Englewood", "CA", "US", "12344-1234");
                cmdText.AppendFormat(baseInsertString, "4", "Stevie", "Ray", "Vaughn", "123 Main St.", "", "", "", "Pinewood", "NV", "US", "12345");
                cmdText.AppendFormat(baseInsertString, "5", "Muddy", "", "Waters", "123 Main St.", "", "", "", "St. Louis", "MO", "US", "63101-1234");
                cmdText.AppendFormat(baseInsertString, "6", "Charles", "", "Johnson", "123 Main St.", "", "", "", "New York", "New York", "US", "11121-0001");
                cmd = new SQLiteCommand(cmdText.ToString(), conn);
                cmd.ExecuteNonQuery();


                cmd = new SQLiteCommand("select * from Customer", conn);
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader.ToString());
                    }
                }
            }
            
            

        }

        [TestMethod]
        public void ClassInitializesAndLoads()
        {
            CustomerDBManager mgr = new CustomerDBManager(AllowDuplicates.Allow, _connString);

            Assert.IsNotNull(mgr.Customers);
        }

    }
}
