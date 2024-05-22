using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using hotel_management.Models;
using hotel_management.DAO;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace hotel_management.Controllers
{
    public class ReservationController : StandardController<ReservationViewModel>
    {
        public ReservationController(){
            NeedsAuthentication = true;
        }

        public override IActionResult Index(){

            ViewBag.IsAdmin = HelperController.AdminSesstionVerification(HttpContext.Session);
            ViewBag.IsEmployee = HelperController.EmployeeSesstionVerification(HttpContext.Session);

            // Shows all Reservations made

            List<ReservationViewModel> reservations_list = new List<ReservationViewModel>();

            ReservationsDAO reservationsDAO = new ReservationsDAO();
            RoomsDAO roomsDAO = new RoomsDAO();
            EmployeesDAO employeesDAO = new EmployeesDAO();

            ViewBag.RoomInfo = new Dictionary<int, string>();

            if(HelperController.AdminSesstionVerification(HttpContext.Session) || HelperController.EmployeeSesstionVerification(HttpContext.Session))
                reservations_list = reservationsDAO.GetAll();
            else
                reservations_list = reservationsDAO.GetAllFromCustomer(HelperController.ActualUserID(HttpContext.Session));

            foreach(var item in reservations_list){
                var room = roomsDAO.Get(item.RoomID);

                if (!ViewBag.RoomInfo.ContainsKey(item.RoomID))
                {
                    ViewBag.RoomInfo.Add(item.RoomID, room.RoomType);
                }
            }

            return View("Index", reservations_list);
        }

        public JsonResult GetPersonNameById(int id)
        {

            PersonsDAO DAO = new PersonsDAO();
            
            var person = DAO.Get(id);

            if (person != null)
            {
                return Json(new { personName = $"{person.FirstName} {person.LastName}" });
            }
            return Json(new { personName = "Unknown" });
        }

        public IActionResult NewReservation()
        {
            // Shows the form to make a new reservation
            RoomsDAO roomsDAO = new RoomsDAO();

            try
            {
                ReservationViewModel model = new ReservationViewModel();

                model.IsPaid = false;
                model.TotalAmount = 0;
                model.PersonID = HelperController.ActualUserID(HttpContext.Session);

                model.AvailableRooms = roomsDAO.GetAvailableRooms();

                return View("ReservationForm", model);
            }
            catch (Exception error)
            {
                return View("Error", new ErrorViewModel(error.ToString()));
            }
        }

        public IActionResult SaveReservation(ReservationViewModel model){

            ReservationsDAO DAO = new ReservationsDAO();

            DAO.Insert(model);

            return RedirectToAction("Index", "Home");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (NeedsAuthentication && !HelperController.LoginSessionVerification(HttpContext.Session))
                context.Result = RedirectToAction("Index", "Login");
            else
            {
                ViewBag.UserLogin = true;
                base.OnActionExecuting(context);
            }
        }

        public override IActionResult Delete(int id, string id_route)
        {

            ReservationsDAO DAO = new ReservationsDAO();

            try
            {
                DAO.Delete(id, id_route);
                return RedirectToAction(IndexViewName);
            }
            catch (Exception error)
            {
                return View("Error", new ErrorViewModel(error.ToString()));
            }
        }

    }
}