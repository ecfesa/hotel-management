using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using hotel_management.Models;
using System.Data.SqlClient;
using System.Data;

namespace hotel_management.DAO
{
    public class EmployeesDAO : StandardDAO<EmployeeViewModel>
    {
        
        public EmployeesDAO()
        {
            Table = "Employees";
        }

        protected override SqlParameter[] CreateParameters(EmployeeViewModel model)
        {
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@Username", model.Username);
            parameters[1] = new SqlParameter("@IsAdmin", model.IsAdmin);
            parameters[2] = new SqlParameter("@PasswordHash", model.PasswordHash);
            parameters[3] = new SqlParameter("@PersonID", model.PersonID);
            return parameters;
        }

        protected override EmployeeViewModel MountModel(DataRow row)
        {
            EmployeeViewModel employee = new EmployeeViewModel();
            employee.Id = (int)row["EmployeeID"];
            employee.IsAdmin = (bool)row["IsAdmin"];
            employee.PersonID = (int)row["PersonID"];
            return employee;
        }

        public bool IsAdmin(string username, string password){

            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@Username", username);
            parameters[1] = new SqlParameter("@PasswordHash", password);

            string sql = "SELECT e.EmployeeID, e.PersonID, e.IsAdmin FROM " + Table + " e INNER JOIN Persons p ON e.PersonID = p.PersonID WHERE e.IsAdmin = 1 AND p.Username = @Username AND p.PasswordHash = @PasswordHash";

            return HelperDAO.ExecuteSelect(sql, parameters).Rows.Count >= 1;

        }

    }
}