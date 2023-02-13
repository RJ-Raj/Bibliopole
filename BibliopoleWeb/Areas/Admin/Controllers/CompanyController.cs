using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bibliopole.DataAccess;
using Bibliopole.DataAccess.Repository.IRepository;
using Bibliopole.Models;
using Bibliopole.Models.ViewModels;
using Bibliopole.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BibliopoleWeb.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = staticDetails.Role_Admin)]

    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _db;

        public CompanyController(IUnitOfWork db)
        {

            _db = db;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

       

        //Edit
        public IActionResult Upsert(int? id)
        {
            Company company = new();

            if(id==null || id==0)
            {
                //create Comapny
                //ViewBag.CategoryList = CategoryList;
                //ViewData["CoverTypeList"] = CoverTypeList;
                return View(company);
            }
            else
            {
                //update comapny
                company = _db.Company.GetFirstOrDefault(u => u.Id == id);
                return View(company);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company obj)
        {
            if (ModelState.IsValid)
            {

                if(obj.Id == 0)
                {
                    _db.Company.Add(obj);

                    TempData["success"] = "Company created successfully";
                }
                else
                {
                    _db.Company.Update(obj);

                    TempData["success"] = "Company updated successfully";
                }
                _db.Save();
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        #region API CALLS

        public IActionResult GetAll()
        {
            var companyList = _db.Company.GetAll();
            return Json(new { data = companyList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _db.Company.GetFirstOrDefault(q => q.Id == id);
            if(obj==null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }


            _db.Company.Remove(obj);
            _db.Save();
            return Json(new { success = true, message = "Company deleted successfully!" });

        }

        #endregion

    }
}

