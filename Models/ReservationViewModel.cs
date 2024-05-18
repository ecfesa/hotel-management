using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hotel_management.Models
{
    public class ReservationViewModel : StandardViewModel
    {
        
        public decimal TotalAmount { get; set; }
        public bool IsPaid { get; set; }
        public int CustomerID { get; set; }
        public int RoomID { get; set; }

    }
}