using System;
using System.IO;
using System.Data.SqlClient;

namespace hotel_management.DAO
{
    public class ConnectionDAO
    {
        public static SqlConnection GetConnection()
        {
            string filePath = "connectionString.txt"; 
            string strConn;
            
            try
            {
                strConn = File.ReadAllText(filePath); 
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Could not read the connection string from the file.", ex);
            }

            SqlConnection conn;
            try
            {
                conn = new SqlConnection(strConn);
                conn.Open();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Could not open a connection to the database.", ex);
            }

            return conn;
        }
    }
}
