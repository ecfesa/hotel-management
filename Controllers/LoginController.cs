using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using hotel_management.DAO;
using hotel_management.Models;
using hotel_management.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.WebEncoders.Testing;

namespace hotel_management.Controllers
{
    public class LoginController : StandardController<PersonViewModel>
    {

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        public IActionResult Login(PersonViewModel model)
        {
            
            PersonsDAO DAO = new PersonsDAO();
            EmployeesDAO employeesDAO = new EmployeesDAO();

            if(DAO.LoginExists(model.Username, HashHelper.ComputeSha256Hash(model.PasswordHash)))
            {
                HttpContext.Session.SetString("UserLogin", "true");

                if((model.Username == AdminLogin.admin_login && model.PasswordHash == AdminLogin.admin_password) || employeesDAO.IsAdmin(model.Username, HashHelper.ComputeSha256Hash(model.PasswordHash)))
                    HttpContext.Session.SetString("IsAdmin", "true");
                else if(employeesDAO.IsEmployee(model.Username, model.PasswordHash))
                    HttpContext.Session.SetString("IsEmployee", "true");

                HttpContext.Session.SetInt32("ID", DAO.LoginExists(model.Username, HashHelper.ComputeSha256Hash(model.PasswordHash), true));

                return RedirectToAction("index", "Home");
            }
            else
            {
                return View("Index");
            } 
        }
    
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}