using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace hotel_management.DAO
{
    public class HelperDAO
    {
        public static void ExecutaProc(string nomeProc, SqlParameter[] parameters)
        {
            using (SqlConnection conn = ConnectionDAO.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(nomeProc, conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                        command.Parameters.AddRange(parameters);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static DataTable ExecutaProcSelect(string nomeProc, SqlParameter[] parameters)
        {
            using (SqlConnection conn = ConnectionDAO.GetConnection())
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(nomeProc, conn))
                {
                if (parameters != null)
                    adapter.SelectCommand.Parameters.AddRange(parameparameterstros);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable table = new DataTable();
                adapter.Fill(table);

                return table;
                }
            }
        }
    }
}