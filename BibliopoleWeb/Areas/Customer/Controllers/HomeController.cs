using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bibliopole.Models;
using Bibliopole.DataAccess.Repository.IRepository;
using System.Collections.Generic;
using Bibliopole.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Bibliopole.Utility;

namespace BibliopoleWeb.Controllers;

[Area("Customer")]

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _db;


    public HomeController(ILogger<HomeController> logger,IUnitOfWork db)
    {
        _logger = logger;
        _db = db;
    }


    public IActionResult Index()
    {
        IEnumerable<Product> productList = _db.Product.GetAll(includeProperties: "Category,CoverType");
        return View(productList);
    }

    public IActionResult Details(int productId)
    {
        ShoppingCart cartobj = new()
        {
            Count = 1,
            ProductId = productId,
            Product = _db.Product.GetFirstOrDefault(u => u.Id == productId, includeProperties: "Category,CoverType")
        };


        return View(cartobj);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public IActionResult Details(ShoppingCart shoppingCart)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        shoppingCart.ApplicationUserId = claim.Value;

        ShoppingCart cartFromDb = _db.ShoppingCart.GetFirstOrDefault(
            u => u.ApplicationUserId == claim.Value && u.ProductId == shoppingCart.ProductId
            );

        if(cartFromDb == null)
        {
            _db.ShoppingCart.Add(shoppingCart);
            _db.Save();

            HttpContext.Session.SetInt32(staticDetails.SessionCart, _db.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value).ToList().Count);

        }
        else
        {
            _db.ShoppingCart.IncrementCount(cartFromDb, shoppingCart.Count);
            _db.Save();
        }

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

