using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hotel_management.Models;

namespace hotel_management.DAO
{
    public class RoomsViewModel : StandardViewModel
    {

		public int RoomNumber { get; set; }
        public string RoomType { get; set; }
        public decimal Rate { get; set; }
        public string Description { get; set; }
        public bool IsAvailable { get; set; }
        public IFormFile Image { get; set; }
        public byte[] InternalImage { get; set; }

    }
}