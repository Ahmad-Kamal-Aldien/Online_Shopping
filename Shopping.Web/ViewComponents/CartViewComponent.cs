using Microsoft.AspNetCore.Mvc;
using Shopping.DataAccessLayer.Repositorys.IRepository;
using Shopping.Utilities;
using System.Security.Claims;

namespace Shopping.Web.ViewComponents
{
    public class CartViewComponent:ViewComponent
    {
        private IUnitOfWork unitOfWork;
        public CartViewComponent(IUnitOfWork _unitOfWork)
        {
            unitOfWork= _unitOfWork;
        }
        //Store Only Count In Session
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claims =(ClaimsIdentity) User.Identity;
            var claim = claims.FindFirst(ClaimTypes.NameIdentifier);

            //If User Login
            if (claim != null)
            {
                //Get Current Count 
                //if (HttpContext.Session.GetInt32(SD.SessionCountCart) != null)
                //{

                //    return View(HttpContext.Session.GetInt32(SD.SessionCountCart));

                //}
                //else
                //{
                //    HttpContext.Session.SetInt32(SD.SessionCountCart, unitOfWork.cart.Get(x => x.UserID == claim.Value).ToList().Count());
                //    return View(HttpContext.Session.GetInt32(SD.SessionCountCart));


                //}

              
                    HttpContext.Session.SetInt32(SD.SessionCountCart, unitOfWork.cart.Get(x => x.UserID == claim.Value).ToList().Count());

                    return View(HttpContext.Session.GetInt32(SD.SessionCountCart));

              
                

            }
            else
            {
                HttpContext.Session.Clear();
                return View(0);
            }

            return View();
        }

    }
}
