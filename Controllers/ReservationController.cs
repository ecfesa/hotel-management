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

            // Show all Reservations made

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

            RoomsDAO roomsDAO = new RoomsDAO();

            try
            {
                ReservationViewModel model = new ReservationViewModel();

                model.IsPaid = false;
                model.TotalAmount = 0;

                model.AvailableRooms = roomsDAO.GetAvailableRooms();

                return View("ReservationForm", model);
            }
            catch (Exception error)
            {
                return View("Error", new ErrorViewModel(error.ToString()));
            }
        }

        public IActionResult SaveReservation(){

            return RedirectToAction("Index", "Home");

        }

    }
}