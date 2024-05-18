using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hotel_management.Models
{
    public class CheckViewModel : StandardViewModel
    {
        public DateTime CheckInDateTime { get; set; }
        public DateTime CheckOutDateTime { get; set; }
        public int ReservationID { get; set; }
    }
}