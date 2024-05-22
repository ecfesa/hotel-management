using hotel_management.DAO;
using hotel_management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace hotel_management.Controllers
{
    public class RoomController : StandardController<RoomsViewModel>
    {
        public RoomController()
        {
            NeedsAuthentication = true;
        }

        public override IActionResult Index()
        {

            ViewBag.IsAdmin = HelperController.AdminSesstionVerification(HttpContext.Session);
            ViewBag.IsEmployee = HelperController.EmployeeSesstionVerification(HttpContext.Session);
            
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
            try
            {
                return View("RoomForm", new RoomsViewModel());
            }
            catch (Exception error)
            {
                return View("Error", new ErrorViewModel(error.ToString()));
            }
        }

        public IActionResult EditRoom(int id)
        {
            try
            {
                RoomsDAO roomsDAO = new RoomsDAO();

                RoomsViewModel room = roomsDAO.Get(id);
                return View("RoomForm", room);
            }
            catch (Exception error)
            {
                return View("Error", new ErrorViewModel(error.ToString()));
            }
        }

        public IActionResult DeleteRoom(int id)
        {
            try
            {
                RoomsDAO roomsDAO = new RoomsDAO();

                roomsDAO.Delete(id);
                return RedirectToAction("Index", "Room");
            }
            catch (Exception error)
            {
                return View("Error", new ErrorViewModel(error.ToString()));
            }
        }

        public override IActionResult Save(RoomsViewModel model, string operation)
        {
            RoomsDAO roomsDAO = new RoomsDAO();

            roomsDAO.Insert(model);
            return RedirectToAction("Index", "Room");
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file, RoomsViewModel model)
        {
            RoomsDAO roomsDAO = new RoomsDAO();

            if (file != null && file.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                model.InternalImage = memoryStream.ToArray();
            }
            else
            {
                if (model.Id != 0)
                {
                    var existingRoom = roomsDAO.Get(model.Id);
                    if (existingRoom != null)
                    {
                        model.InternalImage = existingRoom.InternalImage;
                    }
                }
            }

            if (model.Id == 0)
            {
                roomsDAO.Insert(model);
            }
            else
            {
                roomsDAO.Update(model);
            }

            return RedirectToAction("Index", "Room");

        }

        public async Task<ActionResult> RenderImage(int id)
        {
            RoomsDAO roomsDAO = new RoomsDAO();
            RoomsViewModel room = roomsDAO.Get(id);

            if (room == null || room.InternalImage == null)
            {
                var path = Path.Combine("img", "image-not-found-icon.webp");
                return new VirtualFileResult(path, "image/webp");
            }

            return File(room.InternalImage, "image/png");
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

        [HttpGet]
        public IActionResult GetAvailableRooms()
        {
            RoomsDAO roomsDAO = new RoomsDAO();
            List<RoomsViewModel> availableRooms = roomsDAO.GetAvailableRooms();
            return Json(availableRooms);
        }

        public IActionResult GetAllRooms()
        {
            RoomsDAO roomsDAO = new RoomsDAO();
            List<RoomsViewModel> allRooms = roomsDAO.GetAll();
            return Json(allRooms);
        }
    }

}
