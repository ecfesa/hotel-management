using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using hotel_management.DAO;
using hotel_management.Models;
using System.Data.SqlTypes;
using Microsoft.VisualBasic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace hotel_management.Controllers
{
    public class AdminController : StandardController<StandardViewModel>
    {
        private readonly ILogger<AdminController> _logger;

        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
        }

        public override IActionResult Index()
        {

            ViewBag.IsAdmin = HelperController.AdminSesstionVerification(HttpContext.Session);

            EmployeesDAO employeesDAO = new EmployeesDAO();
            PersonsDAO personsDAO = new PersonsDAO();

            PersonsEmployeesViewModel lists = new PersonsEmployeesViewModel();

            lists.Employees = employeesDAO.GetAll();
            lists.Persons = personsDAO.GetAll();
            return View("Index", lists);
        }

        public IActionResult DeletePerson(int id)
        {
            PersonsDAO DAO = new PersonsDAO();

            DAO.Delete(id, "PersonID");

            return RedirectToAction("Index", "Admin");
        }

        public IActionResult DeleteEmployee(int id){

            EmployeesDAO DAO = new EmployeesDAO();

            DAO.Delete(id, "EmployeeID");

            return RedirectToAction("Index", "Admin");
        }

        public IActionResult EditPerson(int id)
        {

            PersonsDAO DAO = new PersonsDAO();
            return View("PersonEdit", DAO.Get(id));
            
        }

        public IActionResult SavePerson(PersonViewModel model)
        {
            PersonsDAO DAO = new PersonsDAO();

            try
            {
                model.PasswordHash = HashHelper.ComputeSha256Hash(model.PasswordHash);
                DAO.Update(model);
            }
            catch (Exception error)
            {
                Console.WriteLine(error.ToString());
                return View("Error", new ErrorViewModel(error.ToString()));
            }

            return RedirectToAction("Index", "Admin");
        }

        public IActionResult TurnToEmployee(int id){

            EmployeesDAO DAO = new EmployeesDAO();

            EmployeeViewModel new_employee = new EmployeeViewModel(){

                PersonID = id,
                IsAdmin = false

            };

            DAO.Insert(new_employee);

            return RedirectToAction("Index", "Admin");

        }

        public IActionResult UpdateAdminStatus(int id, bool isAdmin, int PersonId) {
            try {

                EmployeesDAO employeesDAO = new EmployeesDAO();

                employeesDAO.Update(new EmployeeViewModel{

                    Id = id,
                    IsAdmin = isAdmin,
                    PersonID = PersonId

                });

                return Json(new { success = true });

            } catch (Exception ex) {
                return Json(new { success = false, message = ex.Message });
            }
        }  

        public IActionResult SearchPersons(string query) {

            PersonsDAO DAO = new PersonsDAO();

            return Json(DAO.GetSearchPersons(query));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!HelperController.AdminSesstionVerification(HttpContext.Session)){
                context.Result = RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.UserLogin = true;
                base.OnActionExecuting(context);
            }
        }
    }
}