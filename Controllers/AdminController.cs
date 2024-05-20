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

            EmployeesDAO employeesDAO = new EmployeesDAO();
            PersonsDAO personsDAO = new PersonsDAO();

            PersonsEmployeesViewModel lists = new PersonsEmployeesViewModel();

            lists.Employees = employeesDAO.GetAll();
            lists.Persons = personsDAO.GetAll();
            return View("Index", lists);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!HelperController.LoginSessionVerification(HttpContext.Session))
                context.Result = RedirectToAction("Index", "Home");
            else
            {
                ViewBag.UserLogin = true;
                base.OnActionExecuting(context);
            }
        }
    }
}