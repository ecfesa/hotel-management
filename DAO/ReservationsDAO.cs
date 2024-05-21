using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hotel_management.Models;
using System.Data.SqlClient;
using System.Data;

namespace hotel_management.DAO
{
    public class ReservationsDAO : StandardDAO<ReservationViewModel>
    {

        public ReservationsDAO()
        {
            Table = "Reservations";
        }

        protected override SqlParameter[] CreateParameters(ReservationViewModel model)
        {
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@TotalAmount", model.TotalAmount);
            parameters[1] = new SqlParameter("@IsPaid", model.IsPaid);
            parameters[2] = new SqlParameter("@PersonID", model.PersonID);
            parameters[3] = new SqlParameter("@RoomID", model.RoomID);
            
            return parameters;
        }

        protected override ReservationViewModel MountModel(DataRow row)
        {
            ReservationViewModel reservation = new ReservationViewModel();
            reservation.Id = (int)row["ReservationID"];
            reservation.TotalAmount = (decimal)row["TotalAmount"];
            reservation.IsPaid = (bool)row["IsPaid"];
            reservation.PersonID = (int)row["PersonID"];
            reservation.RoomID = (int)row["RoomID"];

            return reservation;
        }
        
        public List<ReservationViewModel> GetAllFromCustomer(int? id){

            List<ReservationViewModel> reservations_list = new List<ReservationViewModel>();

            if(id == null)
                return reservations_list;

            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@PersonID", id);

            string sql = "SELECT * FROM " + Table + " WHERE PersonID = @PersonID";

            var table = HelperDAO.ExecuteSelect(sql, p);

            foreach(DataRow item in table.Rows){
                reservations_list.Add(MountModel(item));
            }

            return reservations_list;

        }

    }
}