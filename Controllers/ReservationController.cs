using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using hotel_management.Models;
using hotel_management.DAO;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace hotel_management.Controllers
{
    public class ReservationController : StandardController<ReservationViewModel>
    {
        public ReservationController(){
        }

        public IActionResult ViewReservations(){

            // Shows all Reservations made

            List<ReservationViewModel> reservations_list = new List<ReservationViewModel>();
            ReservationsDAO reservationsDAO = new ReservationsDAO();
            RoomsDAO roomsDAO = new RoomsDAO();

            ViewBag.RoomInfo = new Dictionary<int, string>();
            reservations_list = reservationsDAO.GetAll();

            foreach(var item in reservations_list){
                var room = roomsDAO.Get(item.RoomID);
                if (!ViewBag.RoomInfo.ContainsKey(item.RoomID))
                {
                    ViewBag.RoomInfo.Add(item.RoomID, room.RoomType);
                }
            }

            return View("Index", reservations_list);
        }

        public IActionResult NewReservation()
        {
            // Shows the form to make a new reservation

            try
            {
                return View("ReservationForm");
            }
            catch (Exception error)
            {
                return View("Error", new ErrorViewModel(error.ToString()));
            }
        }

    }
}