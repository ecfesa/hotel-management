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
            employee.Id = (int)row["UserID"];
            employee.Username = row["Username"].ToString();
            employee.IsAdmin = (bool)row["IsAdmin"];
            employee.PasswordHash = row["PasswordHash"].ToString();
            employee.PersonID = (int)row["PersonID"];
            return employee;
        }

    }
}