using LinqToDB.Mapping;
using System;
using System.ComponentModel;

namespace hotel_management.Models
{
    [Table("Rooms")]
    public class Room
    {
        [Column, PrimaryKey, Identity]
        public int RoomID { get; set; }

        [Column(Name = "RoomNumber"), NotNull]
        public string RoomNumber { get; set; }

        [Column(Name = "RoomType"), NotNull]
        public string RoomType { get; set; }

        [Column(Name = "Rate"), NotNull]
        public decimal Rate { get; set; }

        [Column(Name = "Description")]
        public string Description { get; set; }

        [Column(Name = "IsAvailable"), NotNull, DefaultValue(true)]
        public bool IsAvailable { get; set; }
    }
}
