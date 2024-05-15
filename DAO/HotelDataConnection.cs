namespace hotel_management.DAO;

using hotel_management.Models;
using LinqToDB;
using LinqToDB.Data;

public class HotelDataConnection : DataConnection
{
    public HotelDataConnection() : base("Default") { }

    // Add DbSet properties here for each entity you want to query
    public ITable<Room> Rooms => this.GetTable<Room>();
    // Add other DbSet properties as needed
}
