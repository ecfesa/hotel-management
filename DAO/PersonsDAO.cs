using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hotel_management.Models;
using System.Data.SqlClient;
using System.Data;
using System.Text.Json;

namespace hotel_management.DAO
{
    public class PersonsDAO : StandardDAO<PersonViewModel>
    {

  		public PersonsDAO()
        {
            Table = "Persons";
        }

        protected override SqlParameter[] CreateParameters(PersonViewModel model)
        {
            SqlParameter[] parameters = new SqlParameter[6];
            parameters[0] = new SqlParameter("@FirstName", model.FirstName);
            parameters[1] = new SqlParameter("@LastName", model.LastName);
            parameters[2] = new SqlParameter("@Email", model.Email);
            parameters[3] = new SqlParameter("@Username", model.Username);
            parameters[4] = new SqlParameter("@PasswordHash", model.PasswordHash);
            parameters[5] = new SqlParameter("@PhoneNumber", model.PhoneNumber ?? (object)DBNull.Value);
            return parameters;
        }

        protected override PersonViewModel MountModel(DataRow row)
        {
            PersonViewModel person = new PersonViewModel();
            person.Id = (int)row["PersonID"];
            person.FirstName = row["FirstName"].ToString();
            person.LastName = row["LastName"].ToString();
            person.Email = row["Email"].ToString();
            person.PhoneNumber = row["PhoneNumber"] != DBNull.Value ? row["PhoneNumber"].ToString() : null;
            
            return person;
        }
        
        public bool LoginVerification(string username, string password){

            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@Username", username);
            parameters[1] = new SqlParameter("@PasswordHash", password);


            Console.WriteLine("parameter[0]:" + parameters[1]);

            Console.WriteLine("username:" + username);
            Console.WriteLine("password:" + password);

            string sql = "SELECT * FROM " + Table + " WHERE Username = @Username AND PasswordHash = @PasswordHash";

            DataTable table = HelperDAO.ExecuteSelect(sql, parameters);

            Console.WriteLine("table row count:" + table.Rows.Count);

            return table.Rows.Count >= 1;
        }
    }
}