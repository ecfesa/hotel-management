using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hotel_management.DAO;

namespace hotel_management.Models
{
    public class ReservationViewModel : StandardViewModel
    {
        
        public decimal TotalAmount { get; set; }
        public bool IsPaid { get; set; }
        public int? PersonID { get; set; }
        public int RoomID { get; set; }
        public List<RoomsViewModel> AvailableRooms { get; set; }

    }
}