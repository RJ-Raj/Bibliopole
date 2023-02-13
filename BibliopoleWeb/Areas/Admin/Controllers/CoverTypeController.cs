using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bibliopole.DataAccess;
using Bibliopole.DataAccess.Repository.IRepository;
using Bibliopole.Models;
using Bibliopole.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BibliopoleWeb.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = staticDetails.Role_Admin)]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _db;

        public CoverTypeController(IUnitOfWork db)
        {

            _db = db;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            IEnumerable<CoverType> objCoverTypeList = _db.CoverType.GetAll();
            return View(objCoverTypeList);
        }

        //Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _db.CoverType.Add(obj);
                _db.Save();
                TempData["success"] = "Cover Type created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        //Edit
        public IActionResult Edit(int? id)
        {
            if(id==null || id==0)
            {
                return NotFound();
            }
            var coverTypeFromDb = _db.CoverType.GetFirstOrDefault(q => q.Id == id);

            if(coverTypeFromDb == null)
            {
                return NotFound();
            }

            return View(coverTypeFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _db.CoverType.Update(obj);
                _db.Save();
                TempData["success"] = "Cover Type updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var coverTypeFromDb = _db.CoverType.GetFirstOrDefault(q => q.Id == id);

            if (coverTypeFromDb == null)
            {
                return NotFound();
            }

            _db.CoverType.Remove(coverTypeFromDb);
            _db.Save();
            TempData["success"] = "Cover Type deleted successfully";
            return RedirectToAction("Index");
        }

    }
}

