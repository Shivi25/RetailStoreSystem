using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Assignment.Object_Classes;
using Assignment.ViewModelClasses;
using System.Windows;

namespace Assignment
{
    class DataLayer
    {
        //Alter Database name
        const string _ConnectionString = "Server=localhost;Database=Assignment1;Integrated Security=SSPI;";
        private static SqlConnection _Connection;

        #region Connections
        private static bool Connect()
        {
            //Initialzing the connecion object
            if (_Connection == null)
                _Connection = new SqlConnection(_ConnectionString);

            //checking th state of connection -> close/open
            if (_Connection.State == System.Data.ConnectionState.Open)
                return true;

                //not connected
            else
            {
                // trying to get connected
                try
                {
                    _Connection.Open();
                    return true;
                }
                catch (Exception ex) { return false; }
            }
        }

        private bool Disconnect()
        {
            if (_Connection == null)
                return true;

            else if (_Connection.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    _Connection.Close();
                    return true;
                }
                catch (Exception ex) { return false; }
            }
            //Connection is already closed
            else
                return true;
        }
        #endregion

        #region Persons
    
        /*
        #####################################################################
        Q1
                        person._Person._PERSONCODE
                        person._Person._ADDRESSCODE
                        person._Person._FIRSTNAME
                        person._Person._LASTNAME
                        person._Person._EMAIL
                        person._Person._DOB

                        person._Address._ADDRESSCODE
                        person._Address._STREET_NO
                        person._Address._STREET_ADDRESS
                        person._Address._CITY
                        person._Address._STATE
                        person._Address._ZIP
                        person._Address._X
                        person._Address._Y

                        person._Phone._PERSONCODE
                        person._Phone._PHONENUMBER
                        person._Phone._TYPE
        #####################################################################
         */
        public static List<PersonPhoneAddress> SearchCustomers(string name, string family, string email)
        {

            string Q1Query = "select p.PERSONCODE, a.ADDRESSCODE, p.FIRSTNAME, p.LASTNAME, " +
            "p.EMAIL, p.DOB, a.STREET_NO, a.STREET_ADDRESS, a.CITY, a.STATE, a.ZIP, a.X, a.Y, " +
            "ph.PHONE_NUMBER, ph.PHONE_TYPE " +
            "from person p, address a, PHONE ph " +
            "where p.firstname like '%" + name + "%' and p.lastname like '%" + family + "%' and p.email like '%" + email + "%' " +
            "and p.ADDRESSCODE = a.ADDRESSCODE " +
            "and ph.PERSONCODE = p.PERSONCODE " +
            "and not exists (select * from WORKS_IN w where w.PERSONCODE = p.PERSONCODE) " +
            "order by p.LASTNAME asc, p.FIRSTNAME asc, p.PERSONCODE asc ";

            List<PersonPhoneAddress> persons = new List<PersonPhoneAddress>();
            //PersonPhoneAddress person;

            if (Connect())
            {
                //we are going to call query called Q1 
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = Q1Query;
                    sqlCommand.Connection = _Connection;
                    SqlDataReader reader;
                    try { reader = sqlCommand.ExecuteReader(); }
                    catch (Exception ex) { 
                        return null; 
                    }
                    //Converting query results to PersonPhoneAddress objects 
                    while (reader.Read())
                    {
                        PersonPhoneAddress person = new PersonPhoneAddress();
                        person._Person._PERSONCODE = Convert.ToInt32(reader[0]);
                        person._Person._ADDRESSCODE = Convert.ToInt32(reader[1]);
                        person._Person._FIRSTNAME = Convert.ToString(reader[2]);
                        person._Person._LASTNAME = Convert.ToString(reader[3]);
                        person._Person._EMAIL = Convert.ToString(reader[4]);
                        person._Person._DOB = Convert.ToString(reader[5]);

                        person._Address._ADDRESSCODE = Convert.ToInt32(reader[1]);
                        person._Address._STREET_NO = Convert.ToString(reader[6]);
                        person._Address._STREET_ADDRESS = Convert.ToString(reader[7]);
                        person._Address._CITY = Convert.ToString(reader[8]);
                        person._Address._STATE = Convert.ToString(reader[9]);
                        person._Address._ZIP = Convert.ToString(reader[10]);
                        person._Address._X = Convert.ToString(reader[11]);
                        person._Address._Y = Convert.ToString(reader[12]);

                        person._Phone._PERSONCODE = Convert.ToInt32(reader[0]);
                        person._Phone._PHONENUMBER = Convert.ToString(reader[13]);
                        person._Phone._TYPE = Convert.ToString(reader[14]);

                        persons.Add(person);
                    }
                    reader.Close();
                }
            }
               
            return persons;
        }


        /*
        #####################################################################
         Q2
                        customerCity._City
                        customerCity._Quantity
        #####################################################################
         */
        public static List<CustomersCities> GetCustomersCities()
        {
          string Q2Query ="select a.city ,count( *) personcount from person p "+
                          " inner join ADDRESS a on p.ADDRESSCODE = a.ADDRESSCODE " +
                          " group by a.city "+
                          " order by personcount desc ,city asc ";
            List<CustomersCities> customerCities = new List<CustomersCities>();

            if (Connect())
            {
                //we are going to call query called Q2 
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = Q2Query;
                    sqlCommand.Connection = _Connection;
                    SqlDataReader reader;
                    try { reader = sqlCommand.ExecuteReader(); }
                    catch (Exception ex)
                    {
                        return null;
                    }
                    //Converting query results to PersonPhoneAddress objects 
                    while (reader.Read())
                    {
                        CustomersCities customerCity = new CustomersCities();
                        customerCity._City = Convert.ToString(reader[0]);
                        customerCity._Quantity = Convert.ToInt32(reader[1]);
                        customerCities.Add(customerCity);
                    }
                    reader.Close();
                }
            }
            
            return customerCities;

        }     


        /*
       #####################################################################
        Q3
                       cityBOD._MinDOB
                       cityBOD._MaxDOB
       #####################################################################
        */
        public static List<CityBOD> GetCityDOB(string city)
        {
            string Q3Query = " select a.city, min ( p.DOB) as mindob, max(p.DOB) as maxdob  "+
                             " from person p " +
                              " inner join ADDRESS a on p.ADDRESSCODE = a.ADDRESSCODE "+
                              " and a.city= '"+city +"'"+
                              " group by a.city ";
            List<CityBOD> cityBODs = new List<CityBOD>();
            
            if (Connect())
            {
                //we are going to call query called Q3 
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = Q3Query;
                    sqlCommand.Connection = _Connection;
                    SqlDataReader reader;
                    try { 
                        reader = sqlCommand.ExecuteReader(); 
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                    //Converting query results to PersonPhoneAddress objects 
                    while (reader.Read())
                    {
                        CityBOD cityBOD = new CityBOD();
                        cityBOD._MinDOB = Convert.ToString(reader[1]);
                        cityBOD._MaxDOB = Convert.ToString(reader[2]);
                        cityBODs.Add(cityBOD);
                    }
                    reader.Close();
                }
            }

            return cityBODs;

        }

        #endregion Persons

        #region Items

        /*
        #####################################################################
        Q4
                        item._Item._ITEMCODE
                        item._Item._NAME
                        item._Item._BRAND
                        item._Item._PRICE 
                        item._Item._DESCRIPTION
        ###############################
 ######################################
         */
        public static List<ItemPhoto> SearchItems(string name, string brand)
        {
            string Q4Query = " select ITEMCODE, NAME, BRAND, PRICE, DESCRIPTION from item " +
                               " where name like '%"+name+"%' and brand like '%"+brand+"%' ";
 

            List<ItemPhoto> items = new List<ItemPhoto>();
            
            if (Connect())
            {
                //we are going to call query called Q4 
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = Q4Query;
                    sqlCommand.Connection = _Connection;
                    SqlDataReader reader;
                    try
                    {
                        reader = sqlCommand.ExecuteReader();
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                    //Converting query results to PersonPhoneAddress objects 
                    while (reader.Read())
                    {
                        ItemPhoto item = new ItemPhoto();
                        item._Item._ITEMCODE = Convert.ToInt32(reader[0]);
                        item._Item._NAME = Convert.ToString(reader[1]);
                        item._Item._BRAND= Convert.ToString(reader[2]); 
                        item._Item._PRICE =Convert.ToDouble(reader[3]);
                        item._Item._DESCRIPTION = Convert.ToString(reader[4]);
                        items.Add(item);
                    }
                    reader.Close();
                }
            }


            return items;
        }


        /*
        #####################################################################
        Q5
                        itemStoreSold._ITEMCODE
                        itemStoreSold._STORECODE
                        itemStoreSold._SOLD
        #####################################################################
         */
        public static List<ItemStoreSold> GetItemsStoresSold()
        {
            string Q5Query = " select t.STORECODE, i.ITEMCODE ,sum(i.quantity) " +
                               " as sold from includes i " +
                            " inner join TRANSACTIONS t on i.TRANSACTIONCODE = t.TRANSACTIONCODE " +
                            " group by t.STORECODE, i.ITEMCODE " +
                            " order by t.STORECODE asc, sold desc, i.ITEMCODE asc ";

            List<ItemStoreSold> itemStoreSolds = new List<ItemStoreSold>();

            if (Connect())
            {
                //we are going to call query called Q5 
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = Q5Query;
                    sqlCommand.Connection = _Connection;
                    SqlDataReader reader;
                    try
                    {
                        reader = sqlCommand.ExecuteReader();
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                    //Converting query results to PersonPhoneAddress objects 
                    while (reader.Read())
                    {
                        ItemStoreSold itemStoreSold = new ItemStoreSold();
                        itemStoreSold._STORECODE = Convert.ToInt32(reader[0]);
                        itemStoreSold._ITEMCODE = Convert.ToInt32(reader[1]);
                        
                        itemStoreSold._SOLD = Convert.ToInt32(reader[2]);

                        itemStoreSolds.Add(itemStoreSold);
                    }
                    reader.Close();
                }
            }

            return itemStoreSolds;
        }

        /*
        #####################################################################
        Q6
                        existsIn._STORECODE
                        existsIn._ITEMCODE
                        existsIn._QUANTITY
        #####################################################################
         */
        public static List<Exists_In> FindRunnuingOutItems()
        {
            string Q6Query = " select STORECODE, ITEMCODE, QUANTITY  from EXISTS_IN " +
                             " where QUANTITY < 5 " +
                             " order by STORECODE asc, quantity asc, ITEMCODE asc  ";

            List<Exists_In> existIns = new List<Exists_In>();
            Exists_In existsIn;
            
            if (Connect())
            {
                //we are going to call query called Q6 
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = Q6Query;
                    sqlCommand.Connection = _Connection;
                    SqlDataReader reader;
                    try
                    {
                        reader = sqlCommand.ExecuteReader();
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                    //Converting query results to PersonPhoneAddress objects 
                    while (reader.Read())
                    {
                        existsIn = new Exists_In();

                        existsIn._STORECODE = Convert.ToInt32(reader[0]);
                        existsIn._ITEMCODE = Convert.ToInt32(reader[1]);

                        existsIn._QUANTITY = Convert.ToInt32(reader[2]);

                        existIns.Add(existsIn);
                    }
                    reader.Close();
                }
            }


            return existIns;
        }


        /*
        #####################################################################
        Q7
                        bestSeller._ItemName
                        bestSeller._Quantity
        #####################################################################
         */
        public static Best_Seller GetBestSeller()
        {
            string Q7Query = " select top 1  it.ITEMCODE, sum(i.QUANTITY) as sold  from item it " +
                                " inner join INCLUDES i on " +

                                " it.ITEMCODE= i.ITEMCODE " +
                                 " group by it.ITEMCODE " +
                                "  order by sold desc, it.ITEMCODE asc ";

            Best_Seller bestSeller = new Best_Seller();
            if (Connect())
            {
                //we are going to call query called Q7
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = Q7Query;
                    sqlCommand.Connection = _Connection;
                    SqlDataReader reader;
                    try
                    {
                        reader = sqlCommand.ExecuteReader();
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                    //Converting query results to PersonPhoneAddress objects 
                    while (reader.Read())
                    {


                        
                        bestSeller._ItemName = Convert.ToString(reader[0]);
                        bestSeller._Quantity = Convert.ToInt32(reader[1]);



                        
                    }
                    reader.Close();
                }
            }




            return bestSeller;

        }


        /*
        #####################################################################
        Q8
                        mostPopular._Brand
                        mostPopular._Quantity
        #####################################################################
         */
        public static Most_Popular GetMostPopular()
        {
            string Q8Query= " select top 1 it.BRAND, sum(i.QUANTITY) as sold  from item it "+
                            " inner join INCLUDES i on "+
                            " it.ITEMCODE= i.ITEMCODE "+
                             " group by it.BRAND "+
                            " order by sold desc, it.BRAND asc ";

            Most_Popular mostPopular = new Most_Popular();
             if (Connect())
            {
                //we are going to call query called Q8
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = Q8Query;
                    sqlCommand.Connection = _Connection;
                    SqlDataReader reader;
                    try
                    {
                        reader = sqlCommand.ExecuteReader();
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                    //Converting query results to PersonPhoneAddress objects 
                    while (reader.Read())
                    {
                        mostPopular._Brand = Convert.ToString(reader[0]);
                        mostPopular._Quantity = Convert.ToInt32(reader[1]);
                    }
                    reader.Close();

                }

                }
            return mostPopular;
        }

        #endregion //Items

        #region Transactions

        /*
        #####################################################################
        Q9
                        transactionPerson._Transaction._TRANSACTIONCODE
                        transactionPerson._FirstName
                        transactionPerson._LastName
                        transactionPerson._Transaction._STORECODE
                        transactionPerson._Transaction._TRANSACTION_DATE
                        transactionPerson._Transaction._PAYMENT_METHOD
        #####################################################################
         */
        public static List<TransactionPerson> SearchTransactions(string personFamily, string paymentMethod)

        {
            string Q9Query = " select t.TRANSACTIONCODE, p.FIRSTNAME, p.LASTNAME, t.STORECODE, " +
                            " t.TRANSACTION_DATE, t.PAYMENT_METHOD " +
                            " from person p inner join   transactions t on  p.personcode = t.personcode " +
                            " where p.LASTNAME like '%" + personFamily + "%' and t.PAYMENT_METHOD like '%" +paymentMethod+"%' ";
            List < TransactionPerson> transactionsPersons=new List <TransactionPerson>();
            
            if (Connect())
            {
                //we are going to call query called Q9
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = Q9Query;
                    sqlCommand.Connection = _Connection;
                    SqlDataReader reader;
                    try
                    {
                        reader = sqlCommand.ExecuteReader();
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                    //Converting query results to PersonPhoneAddress objects 
                    while (reader.Read())
                    {
                        try
                        {
                            TransactionPerson transactionPerson = new TransactionPerson();
                            transactionPerson._Transaction._TRANSACTIONCODE = Convert.ToInt32(reader[0]);
                            transactionPerson._FirstName = Convert.ToString(reader[1]);
                            transactionPerson._LastName = Convert.ToString(reader[2]);
                            transactionPerson._Transaction._STORECODE = Convert.ToInt32(reader[3]);
                            transactionPerson._Transaction._TRANSACTION_DATE = Convert.ToDateTime(reader[4]);
                            transactionPerson._Transaction._PAYMENT_METHOD = Convert.ToString(reader[5]);

                            transactionsPersons.Add(transactionPerson);
                        }
                        catch (Exception ex)
                        {
                            return null;
                        }
                        
                    }
                    reader.Close();

                }
            }
            return transactionsPersons;
        }

        /*
        #####################################################################
        Q10
                        itemInclude._Item._NAME
                        itemInclude._Quantity
                        itemInclude._Item._PRICE
                        itemInclude._Total
        #####################################################################
        */
        public static List<Item_Include> GetItemDeatils(int transactionCode)
        {
            string Q10Query = " select it.NAME, i.QUANTITY, it.PRICE,(i.QUANTITY * it.PRICE) as total " +
                                " from INCLUDES i " +
                                " inner join item it " +
                                " on i.ITEMCODE= it.ITEMCODE " +
                                 " Where i.TRANSACTIONCODE = 1 " +
                                    " order by it.name asc ";
            List<Item_Include> itemsIncludes = new List<Item_Include>();
            
            //Item_Include itemInclude;
            if (Connect())
            {
                //we are going to call query called Q10
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = Q10Query;
                    sqlCommand.Connection = _Connection;
                    SqlDataReader reader;
                    try
                    {
                        reader = sqlCommand.ExecuteReader();
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                    //Converting query results to PersonPhoneAddress objects 
                    while (reader.Read())
                    {

                        Item_Include itemInclude = new Item_Include();
                        itemInclude._Item._NAME = Convert.ToString(reader[0]);
                        itemInclude._Quantity = Convert.ToInt32(reader[1]);
                        itemInclude._Item._PRICE = Convert.ToInt32(reader[2]);
                        itemInclude._Total = Convert.ToInt32(reader[3]);
                        itemsIncludes.Add(itemInclude);
                    }
                    reader.Close();
            
                }
            }
            return itemsIncludes;
        }

        /*
        #####################################################################
        Q11
                        transactionCity._City
                        transactionCity._Quantity
        #####################################################################
        */
        public static List<TransactionsCities> TransactionsCities()
        {
            string Q11Query= " select a.CITY, count(*) transactions  from ADDRESS a "+
                            " inner join store s on a.ADDRESSCODE= s.ADDRESSCODE "+
                            " inner join TRANSACTIONS t on t.STORECODE= s.STORECODE "+
                            " group by a.city "+
                            " order by transactions desc, a.CITY asc ";

            List<TransactionsCities> transactionsCities = new List<TransactionsCities>();
            TransactionsCities transactionCity;
           
             if (Connect())
            {
                //we are going to call query called Q11
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = Q11Query;
                    sqlCommand.Connection = _Connection;
                    SqlDataReader reader;
                    try
                    {
                        reader = sqlCommand.ExecuteReader();
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                    //Converting query results to PersonPhoneAddress objects 
                    while (reader.Read())
                    {
                        transactionCity   = new TransactionsCities();
                        transactionCity._City = Convert.ToString(reader[0]);
                        transactionCity._Quantity = Convert.ToInt32(reader[1]);
                        transactionsCities.Add(transactionCity);
                         
                    }
                    reader.Close();

                }

            }
            
            return transactionsCities;
        }

        #endregion Transactions

        #region Map

        /*
        #####################################################################
        Q12
                        storeLocation._STORECODE
                        storeLocation._Distance
                        storeLocation._X
                        storeLocation._Y
        #####################################################################
        */

        public static List<StoreDistance> FindNearestStore(double X, double Y)
        {
            List<StoreDistance> neareststores = new List<StoreDistance>();
           
            if (Connect())
            {
                //we are going to call query called Q11
                using (SqlCommand sqlCommand = new SqlCommand("Q12", _Connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@X", SqlDbType.VarChar).Value = X.ToString();
                    sqlCommand.Parameters.Add("@Y", SqlDbType.VarChar).Value = Y.ToString();
                    SqlDataReader reader;
                    try
                    {
                        reader = sqlCommand.ExecuteReader();
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                    //Converting query results to PersonPhoneAddress objects 
                    while (reader.Read())
                    {
                        StoreDistance storeLocation = new StoreDistance();
                        storeLocation._STORECODE = Convert.ToInt32(reader[0]);
                        storeLocation._Distance = Convert.ToString(reader[1]);
                        storeLocation._X = Convert.ToInt32(reader[2]);
                        storeLocation._Y = Convert.ToInt32(reader[3]);


                        neareststores.Add(storeLocation);

                    }
                    reader.Close();
                }
            }
            
                    return neareststores;
        }


        /*
        #####################################################################
        Q13
                        storeLocation._STORECODE
                        storeLocation._Distance
                        storeLocation._X
                        storeLocation._Y
        #####################################################################
        */
        public static List<StoreDistance> FindStoresInRange(double X, double Y, int range)
        {
            List<StoreDistance> neareststores = new List<StoreDistance>();
            
            if (Connect())
            {
                //we are going to call query called Q11
                using (SqlCommand sqlCommand = new SqlCommand("Q13", _Connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@X", SqlDbType.VarChar).Value = X.ToString();
                    sqlCommand.Parameters.Add("@Y", SqlDbType.VarChar).Value = Y.ToString();
                    sqlCommand.Parameters.Add("@range", SqlDbType.VarChar).Value = range.ToString();
                    SqlDataReader reader;
                    try
                    {
                        reader = sqlCommand.ExecuteReader();
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                    //Converting query results to PersonPhoneAddress objects 
                    while (reader.Read())
                    {
                        StoreDistance storeLocation = new StoreDistance();
                        storeLocation._STORECODE = Convert.ToInt32(reader[0]);
                        storeLocation._Distance = Convert.ToString(reader[1]);
                        storeLocation._X = Convert.ToInt32(reader[2]);
                        storeLocation._Y = Convert.ToInt32(reader[3]);


                        neareststores.Add(storeLocation);

                    }
                    reader.Close();
                }
            }
            
            

            return neareststores;
        }

        #endregion
    }
}
