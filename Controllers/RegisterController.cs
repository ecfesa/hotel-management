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
    public class RegisterController : StandardController<PersonViewModel>
    {
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }


        public override IActionResult Save(PersonViewModel model, string operation){
            
            PersonsDAO DAO = new PersonsDAO();

            model.PasswordHash = HashHelper.ComputeSha256Hash(model.PasswordHash);

            ValidateRegistry(model);

            if(ModelState.IsValid == true){

                DAO.Insert(model);
                return RedirectToAction("index", "Home");
            }

            return View("Index", model);

        }

        public void ValidateRegistry(PersonViewModel model){

            ModelState.Clear();

            if(string.IsNullOrEmpty(model.PasswordHash) || model.PasswordHash.Length < 8)
                ModelState.AddModelError("PasswordHash", "The Password must be longer than 8 caracthers!!!");

        }
    }
}