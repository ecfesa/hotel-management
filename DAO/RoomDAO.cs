using hotel_management.Models;

namespace hotel_management.DAO
{
    public static class RoomDAO
    {
        public static List<Room> GetRooms() {
            var db = new HotelDataConnection();

            var query = from room in db.Rooms
                        select room;

            return query.ToList();         
        }
    }
}
