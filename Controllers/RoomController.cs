using hotel_management.DAO;
using hotel_management.Models;
using Microsoft.AspNetCore.Mvc;

namespace hotel_management.Controllers
{
    public class RoomController : StandardController<RoomsViewModel>
    {
        public RoomController()
        {
        }

        public IActionResult ViewRooms()
        {
            // Shows all available Rooms

            try
            {
                RoomsDAO roomsDAO = new RoomsDAO();
                List<RoomsViewModel> room_list = new List<RoomsViewModel>();

                room_list = roomsDAO.GetAll();

                return View("Index", room_list);
            }
            catch (Exception error)
            {
                return View("Error", new ErrorViewModel(error.ToString()));
            }
        }

        public IActionResult NewRoom()
        {
            // Shows the form to make a new room

            try
            {
                return View("RoomForm");
            }
            catch (Exception error)
            {
                return View("Error", new ErrorViewModel(error.ToString()));
            }
        }
    }
}
