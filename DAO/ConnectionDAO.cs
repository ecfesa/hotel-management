using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace hotel_management.DAO
{
    public class ConnectionDAO
    {
        public static SqlConnection GetConnection()
        {
            string strConn = "Data Source=LOCALHOST; Database=AULADB; user id=sa; password=123456";
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            return conn;
        }
        
    }
}