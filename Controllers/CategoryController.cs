using Microsoft.AspNetCore.Mvc;
using SampleNewProject.Data;
using SampleNewProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleNewProject.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories;
            return View(objCategoryList);
        }
        //GET
        public IActionResult Create()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            //Custom validations on the server side
            if(obj.Name ==obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Error", "Both values should not be same");
            }
            if (ModelState.IsValid)
            {
            _db.Categories.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }
            var Categoryfromdb = _db.Categories.Find(id);
            //var Categoryfromdb = _db.Categories.SingleOrDefault(x=>x.Id==id);
            if (Categoryfromdb == null)
            {
                return NotFound();
            }
            return View(Categoryfromdb);
        }

        public IActionResult Edit()
        {
            return View();
        }
    }
}
