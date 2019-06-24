using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ContactLog.ContactClass
{
    class Contacts
    {
        
        /*Declaring and setting up the variables available on the form*/
        public int ContactID { set; get;}

        public string FirstName { set; get; }

        public string LastName { set; get; }

        public string Email { set; get; }

        public string Address { set; get; }

        // Creating a connection string to connect to the specific database
        static string connectionString = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

/*****************************************************************************************************************************************************/
/*****************************************************************************************************************************************************/

        // SELECTING ALL THE DATA FROM THE DATABASE
        
        public DataTable DisplayAllData()
        {
            // Step 1. Connecting to the Database

            SqlConnection connection = new SqlConnection(connectionString);

            // Creating a DataTable to store a Value received from the database

            DataTable dt = new DataTable();

            try {
            
                // Step 2. Writing a Query
                string sql = "SELECT * FROM all_contacts";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                connection.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally {
                connection.Close();
            }

            return dt;
        }
/*****************************************************************************************************************************************************/
/*****************************************************************************************************************************************************/

        // INSERTING A DATA INTO THE DATABASE
        public bool Insert(Contacts contactsObject)
        {
            bool isSuccess = false;

            SqlConnection connection = new SqlConnection(connectionString);
            try {
                string sql = "INSERT INTO all_contacts VALUES(@FirstName,@LastName,@Email,@Address)";

                SqlCommand command = new SqlCommand(sql, connection);
                
                //Adding a values to the Parameters
                command.Parameters.AddWithValue("@FirstName", contactsObject.FirstName);
                command.Parameters.AddWithValue("@LastName", contactsObject.LastName);
                command.Parameters.AddWithValue("@Email", contactsObject.Email);
                command.Parameters.AddWithValue("@Address", contactsObject.Address);

                connection.Open();
                int rows = command.ExecuteNonQuery();

                if(rows>0)
                { isSuccess = true; }
                else { isSuccess = false; }


            }
            catch (Exception ex) {Console.WriteLine(ex.Message); }
            finally { connection.Close(); }


            return isSuccess;
        
        }

/******************************************************************************************************************************************************/
/*****************************************************************************************************************************************************/

        // UPDATING A DATA in THE DATABASE

        public bool Update(Contacts contactsObject)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            bool isSuccess = false;

            try
            {
                string sql = "UPDATE all_contacts SET FirstName=@FirstName,LastName=@LastName,Email=@Email,Address=@Address WHERE ContactID=@ContactID";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("ContactID", contactsObject.ContactID);
                command.Parameters.AddWithValue("@FirstName", contactsObject.FirstName);
                command.Parameters.AddWithValue("@LastName", contactsObject.LastName);
                command.Parameters.AddWithValue("@Email", contactsObject.Email);
                command.Parameters.AddWithValue("@Address", contactsObject.Address);
                connection.Open();
                int rows = command.ExecuteNonQuery();
                if (rows > 0) { isSuccess = true; }
                else { isSuccess = false; }

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return isSuccess;
        }
/*****************************************************************************************************************************************************/
/*****************************************************************************************************************************************************/

        // DELETING SPECIFIC RECORD FROM THE DATABASE

        public bool Delete(Contacts contactsObject)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            bool isSuccess = false;

            try {
                string sql = "DELETE FROM all_contacts WHERE ContactID=@ContactID";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ContactID", contactsObject.ContactID);
                connection.Open();
                int rows = command.ExecuteNonQuery(); 
                if(rows>0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { connection.Close(); }

            return isSuccess;
        }
/*****************************************************************************************************************************************************/
/*****************************************************************************************************************************************************/

    }
}
