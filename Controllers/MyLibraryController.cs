using MoviesLibrary.ActionFilter;
using MoviesLibrary.Models;
using MoviesLibrary.ViewModels.MyLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoviesLibrary.Controllers
{
    [AuthFilter]
    public class MyLibraryController : Controller
    {
        // GET: MyLibrary
        public ActionResult Index()
        {
            Users loggedUser = (Users)Session["loggedUser"];
            MoviesModel context = new MoviesModel();
            IndexVM model = new IndexVM();
   
            model.Items = context.MyLibrary.Where(i => i.UserId == loggedUser.UseId).ToList();
                    
            return View(model);

        }
        [HttpGet]
        public ActionResult Add(int? Id)
        {
            Users loggedUser = (Users)Session["loggedUser"];
            MoviesModel context = new MoviesModel();
            MyLibrary myLib = new MyLibrary();
            MoviesTable item = Id == null ? new MoviesTable() : context.MoviesTable.Where(u => u.MovieId==Id.Value).FirstOrDefault();

            if (item == null)
                return RedirectToAction("Index", "Home");

            myLib.UserId = loggedUser.UseId;
            myLib.MovieId = item.MovieId;
            myLib.UserName = loggedUser.Username;
            myLib.MovieName = item.MovieName;
            context.MyLibrary.Add(myLib);

            context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }

        public ActionResult Delete(int Id)
        {
            
            MoviesModel context = new MoviesModel();
            Users loggedUser = (Users)Session["loggedUser"];

            MyLibrary item = context.MyLibrary.Where(u => u.MovieLibraryId == Id && u.UserId == loggedUser.UseId).FirstOrDefault();

            if (item != null)
            {

                context.MyLibrary.Remove(item);
                context.SaveChanges();

                return RedirectToAction("Index", "MyLibrary");
            }
            else
            {
                return RedirectToAction("Index", "MyLibrary");
            }

            


        }
    }
}