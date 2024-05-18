using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hotel_management.Models;
using System.Data.SqlClient;
using System.Data;

namespace hotel_management.DAO
{
    public class RoomsDAO : StandardDAO<RoomsViewModel>
    {

      public RoomsDAO()
      {
        Table = "Rooms";
      }

      protected override SqlParameter[] CreateParameters(RoomsViewModel model)
      {
          SqlParameter[] parameters = new SqlParameter[5];
          parameters[0] = new SqlParameter("@RoomNumber", model.RoomNumber);
          parameters[1] = new SqlParameter("@RoomType", model.RoomType);
          parameters[2] = new SqlParameter("@Rate", model.Rate);
          parameters[3] = new SqlParameter("@Description", model.Description ?? (object)DBNull.Value);
          parameters[4] = new SqlParameter("@IsAvailable", model.IsAvailable);
          return parameters;
      }

      protected override RoomsViewModel MountModel(DataRow row)
      {
          RoomsViewModel room = new RoomsViewModel();
          room.Id = (int)row["RoomID"];
          room.RoomNumber = (int)row["RoomNumber"];
          room.RoomType = row["RoomType"].ToString();
          room.Rate = (decimal)row["Rate"];
          room.Description = row["Description"] != DBNull.Value ? row["Description"].ToString() : null;
          room.IsAvailable = (bool)row["IsAvailable"];
          
          return room;
      }
        
    }
}