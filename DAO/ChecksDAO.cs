using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hotel_management.Models;
using System.Data.SqlClient;
using System.Data;

namespace hotel_management.DAO
{
    public class ChecksDAO : StandardDAO<CheckViewModel>
    {

        public ChecksDAO()
        {
            Table = "CheckIns";
        }

        protected override SqlParameter[] CreateParameters(CheckViewModel model)
        {
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@CheckInDateTime", model.CheckInDateTime);
            parameters[1] = new SqlParameter("@CheckOutDateTime", model.CheckOutDateTime);
            parameters[2] = new SqlParameter("@ReservationID", model.ReservationID);
            return parameters;
        }

        protected override CheckViewModel MountModel(DataRow row)
        {
            CheckViewModel checkIn = new CheckViewModel();
            checkIn.Id = (int)row["CheckInID"];
            checkIn.CheckInDateTime = (DateTime)row["CheckInDateTime"];
            checkIn.CheckOutDateTime = (DateTime)row["CheckOutDateTime"];
            checkIn.ReservationID = (int)row["ReservationID"];
            return checkIn;
        }
    }
}