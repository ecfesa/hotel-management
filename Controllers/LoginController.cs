using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using hotel_management.DAO;
using hotel_management.Models;
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

        public IActionResult Login(PersonViewModel model){
            
            PersonsDAO DAO = new PersonsDAO();

            if(DAO.LoginVerification(model.Username, HashHelper.ComputeSha256Hash(model.PasswordHash)))
            {
                HttpContext.Session.SetString("Logado", "true");
                return RedirectToAction("index", "Home");
            }
            else
            {
                return View("Index");
            } 
        }
    }
}