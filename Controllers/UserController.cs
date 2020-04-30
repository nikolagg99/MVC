using MoviesLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoviesLibrary.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult AddOrEdit(int id = 0)
        {
            Users user = new Users();
            return View();
        }

        [HttpPost]
        public ActionResult AddOrEdit(Users user)
        {
            using (MoviesModel db = new MoviesModel())
            {
                if (db.Users.Any(x => x.Username == user.Username))
                {
                    ViewBag.DuplicateMessage = "Username already exist!";
                    return View("AddOrEdit", user);
                }
                else
                {
                    

                    db.Users.Add(user);
                    db.SaveChanges();
                }


            }
            ModelState.Clear();
            ViewBag.SuccesMessage = "Registration Successful!!";

            return RedirectToAction("Index", "Login");
        }
    }
}