using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hotel_management.Models;
using System.Data.SqlClient;
using System.Data;

namespace hotel_management.DAO
{
    public class CustomersDAO : StandardDAO<CustomerViewModel>
    {

        public CustomersDAO()
        {
            Table = "Customers";
        }

        protected override SqlParameter[] CreateParameters(CustomerViewModel model)
        {
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@Username", model.Username);
            parameters[1] = new SqlParameter("@PasswordHash", model.PasswordHash);
            parameters[2] = new SqlParameter("@PersonID", model.PersonID);

            return parameters;
        }

        protected override CustomerViewModel MountModel(DataRow row)
        {
            CustomerViewModel customer = new CustomerViewModel();
            customer.Id = (int)row["UserID"];
            customer.Username = row["Username"].ToString();
            customer.PasswordHash = row["PasswordHash"].ToString();
            customer.PersonID = (int)row["PersonID"];
            
            return customer;
        }

    }
}