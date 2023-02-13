using System;
using System.Security.Claims;
using Bibliopole.DataAccess.Repository.IRepository;
using Bibliopole.Utility;
using Microsoft.AspNetCore.Mvc;

namespace BibliopoleWeb.ViewComponents
{
    public class ShoppingCartViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShoppingCartViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if(claim!=null)
            {
                if(HttpContext.Session.GetInt32(staticDetails.SessionCart) != null)
                {
                    return View(HttpContext.Session.GetInt32(staticDetails.SessionCart));
                }
                else
                {
                    HttpContext.Session.SetInt32(staticDetails.SessionCart, _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value).ToList().Count);
                    return View(HttpContext.Session.GetInt32(staticDetails.SessionCart));
                }
            }
            else
            {
                HttpContext.Session.Clear();
                return View(0);
            }
        }


    }
}

