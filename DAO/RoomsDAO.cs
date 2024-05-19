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

        public override void Update(RoomsViewModel model)
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("RoomID", model.Id),
                new SqlParameter("RoomNumber", model.RoomNumber),
                new SqlParameter("RoomType", model.RoomType),
                new SqlParameter("Rate", model.Rate),
                new SqlParameter("Description", model.Description ?? (object)DBNull.Value),
                new SqlParameter("IsAvailable", model.IsAvailable),
                new SqlParameter("Picture", model.InternalImage ?? (object)DBNull.Value),
            };

            HelperDAO.ExecutaProc("spUpdate_" + Table, p);
        }

        public override void Delete(int id)
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("RoomID", id),
            };

            HelperDAO.ExecutaProc("spDelete_" + Table, p);
        }

        public override RoomsViewModel Get(int id)
        {
            // Implementação do método de consulta por id

            var p = new SqlParameter[]{
                new SqlParameter("RoomID", id),
            };

            var table = HelperDAO.ExecutaProcSelect("spGet_" + Table, p);

            if (table.Rows.Count != 0)
                return MountModel(table.Rows[0]);
            else
                return null;
        }

        protected override SqlParameter[] CreateParameters(RoomsViewModel model)
        {
            object imgByte = model.InternalImage ?? (object)DBNull.Value;

            SqlParameter[] parameters = new SqlParameter[6];
            parameters[0] = new SqlParameter("@RoomNumber", SqlDbType.Int) { Value = model.RoomNumber };
            parameters[1] = new SqlParameter("@RoomType", SqlDbType.NVarChar, 50) { Value = model.RoomType };
            parameters[2] = new SqlParameter("@Rate", SqlDbType.Decimal) { Value = model.Rate };
            parameters[3] = new SqlParameter("@Description", SqlDbType.NVarChar, -1) { Value = model.Description ?? (object)DBNull.Value };
            parameters[4] = new SqlParameter("@IsAvailable", SqlDbType.Bit) { Value = model.IsAvailable };
            parameters[5] = new SqlParameter("@Picture", SqlDbType.VarBinary, -1) { Value = imgByte };

            return parameters;
        }

        protected override RoomsViewModel MountModel(DataRow row)
        {
            RoomsViewModel room = new RoomsViewModel();
            room.Id = (int)row["RoomID"];
            room.RoomNumber = Convert.ToInt32(row["RoomNumber"]);
            room.RoomType = row["RoomType"].ToString();
            room.Rate = (decimal)row["Rate"];
            room.Description = row["Description"] != DBNull.Value ? row["Description"].ToString() : null;
            room.IsAvailable = (bool)row["IsAvailable"];

            if (row["Picture"] != DBNull.Value)
                room.InternalImage = row["Picture"] as byte[];

            return room;
        }

    }
}