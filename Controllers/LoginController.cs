using MoviesLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoviesLibrary.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Autherize(Users userModel)
        {
            using (MoviesModel db = new MoviesModel())
            {
                var userDetails = db.Users.Where(x => x.Username == userModel.Username && x.Password == userModel.Password).FirstOrDefault();
                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "Wrong username or password!!!";
                    return View("Index", userModel);
                }
                else
                {
                    Session["loggedUser"] = userDetails;
                    Session["Username"] = userDetails.Username;
                    //Session["loggedUser"] = userDetails.UseId;
                    return RedirectToAction("Index", "Home");
                }
            }

        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}