using MoviesLibrary.ActionFilter;
using MoviesLibrary.Models;
using MoviesLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoviesLibrary.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies
        public ActionResult Index(IndexVM model, string searchName, MoviesTable movieTb)
        {
            MoviesModel context = new MoviesModel();
            MoviesTable item = new MoviesTable();

           //if(!String.IsNullOrEmpty(searchName))
           // {
                model.Items = context.MoviesTable.OrderByDescending(i => i.MovieName).ToList();
           // }

            return View(model);
        }

        public ActionResult AddMovie(int id = 0)
        {
            MoviesTable movies = new MoviesTable();
            return View();
        }

        [HttpPost]
        public ActionResult AddMovie(MoviesTable movies)
        {

            using (MoviesModel db = new MoviesModel())
            {
                if (db.MoviesTable.Any(x => x.MovieName == movies.MovieName))
                {
                    ViewBag.DuplicateMessage = "Movie already exist!!";
                    return View("AddMovie", movies);
                }
                else
                {
                    db.MoviesTable.Add(movies);
                    db.SaveChanges();
                }
            }

            ModelState.Clear();
            ViewBag.SuccesMessage = "Add successful!!!";

            return View("AddMovie", new MoviesTable());
        }
    }
}