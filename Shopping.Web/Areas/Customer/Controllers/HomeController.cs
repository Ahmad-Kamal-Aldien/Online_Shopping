using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping.DataAccessLayer.Repositorys.IRepository;
using Shopping.DataAccessLayer.Repositorys.Repository;
using Shopping.Entites.Model;
using Shopping.Entites.Model.ViewModels;
using Shopping.Utilities;
using System.Security.Claims;


namespace Shopping.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    //Customer/Home/Index
    
    public class HomeController : Controller
    {
        public IUnitOfWork unitOfWork;
        public HomeController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;

        }
        public IActionResult Index()
        {
          
            
           

            return View(unitOfWork.product.Get());
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetCart()
        {
            var user = (ClaimsIdentity)User.Identity;
            var claim = user.FindFirst(ClaimTypes.NameIdentifier);

            string userid = claim.Value;

            CartViewModel cartViewModel = new CartViewModel()
            {
                carts = unitOfWork.cart.Get(x => x.UserID == userid,include: "Product")
                
            };
            decimal total;
            foreach (var item in cartViewModel.carts)
            {
                cartViewModel.total += (item.Count * item.Product.Price);
            }


            return View(cartViewModel);
        }
        [HttpGet]
        public IActionResult Details(int ProID)
        {

            ShoppingCart shoppingCartViewModel = new ShoppingCart()
            {
               ProID= ProID,
                Product = unitOfWork.product.GetFirst(x => x.Id == ProID, include: "Category"),
                Count = 1
            };



            return View(shoppingCartViewModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {

            var user = (ClaimsIdentity)User.Identity;
            var claim = user.FindFirst(ClaimTypes.NameIdentifier);

            shoppingCart.UserID = claim.Value;

            //var OldData = unitOfWork.cart.GetFirst(x => x.UserID == claim.Value &&
            // x.ProID == shoppingCart.ProID
            // );
            var OldData = unitOfWork.cart.GetFirst(x => x.UserID == claim.Value &&
            x.ProID == shoppingCart.ProID
            );
            
            if (OldData == null)
            {
                unitOfWork.cart.Add(shoppingCart);
                //After Add 
                //HttpContext.Session.SetInt32("SessionCountCart",);

            }
            else
            {
                shoppingCart.Id = OldData.Id;
                unitOfWork.cart.Update(shoppingCart);
                



            }

            unitOfWork.complete();

            return RedirectToAction("Index");




        }

        [HttpGet]
        public IActionResult CheckOut()
            {
            var user = (ClaimsIdentity)User.Identity;
            var claim = user.FindFirst(ClaimTypes.NameIdentifier);

            string userid = claim.Value;

            CartViewModel cartViewModel = new CartViewModel()
            {
                carts = unitOfWork.cart.Get(x => x.UserID == userid, include: "Product")

            };
            decimal total;
            foreach (var item in cartViewModel.carts)
            {
                cartViewModel.total += (item.Count * item.Product.Price);
            }


            return View(cartViewModel);
        }


        [HttpPost]
        public IActionResult CheckOut(OrderHeader orderHeader)
        {
           
            var user = (ClaimsIdentity)User.Identity;
            var claim = user.FindFirst(ClaimTypes.NameIdentifier);

            string userid = claim.Value;

            CartViewModel cartViewModel = new CartViewModel()
            {
                carts = unitOfWork.cart.Get(x => x.UserID == userid, include: "Product"),
                //Initilize
                orderheader = new()

            };
            cartViewModel.orderheader.City = orderHeader.City;
            cartViewModel.orderheader.Name = orderHeader.Name;

            cartViewModel.orderheader.NameUserID = userid;


            cartViewModel.orderheader.Address= orderHeader.Address;

            cartViewModel.orderheader.PaymentStatus =SD.Pending;
            cartViewModel.orderheader.orderStatus = SD.Pending;



            cartViewModel.orderheader.PhoneNumber = orderHeader.PhoneNumber;
           
            cartViewModel.orderheader.OrderDate = DateTime.Now;

            ////Allow Null
            //ShippingDate
            //PaymentDate

            decimal total;
            foreach (var item in cartViewModel.carts)
            {
                cartViewModel.total += (item.Count * item.Product.Price);
            }

            cartViewModel.orderheader.total = cartViewModel.total;
            unitOfWork.orderHeader.Add(cartViewModel.orderheader);
            unitOfWork.complete();

            //Save Deatils

            foreach (var item in cartViewModel.carts)
            {
                OrderDetails orderDetails = new OrderDetails()
                {
                    Count = item.Count,
                    OrderID = cartViewModel.orderheader.ID,
                    Price = item.Product.Price,
                    ProID = item.ProID
                };
                unitOfWork.orderDetails.Add(orderDetails);
                unitOfWork.complete();

                //Stripe Api 
                //Stripe Api 
                //Stripe Api 
                //Stripe Api 

            }




            return RedirectToAction("Index");
        }
        public IActionResult Plus(int Id)
        {
        
            var OldData = unitOfWork.cart.GetFirst(x=>x.Id== Id);
           
            unitOfWork.cart.IncreaseNum(OldData,1);
            unitOfWork.complete();
            //return RedirectToAction("Index");

            return RedirectToAction(nameof(GetCart));


     


        }
        public IActionResult Minus(int Id)
        {
          
            var OldData = unitOfWork.cart.GetFirst(x=>x.Id == Id);

            if (OldData.Count == 1)
            {
              


                unitOfWork.cart.Delete(OldData);





                unitOfWork.complete();

                return RedirectToAction("Index");
            }
            else
            {
                unitOfWork.cart.DecreaseNum(OldData, 1);
                unitOfWork.complete();
                //return RedirectToAction("Index");

                return RedirectToAction(nameof(GetCart));
            }


         

        }
        public IActionResult Delete(ShoppingCart shoppingCart,int Id)
        {
           
            //User Hale Shla And Product He 

            var user = (ClaimsIdentity)User.Identity;
            var claim = user.FindFirst(ClaimTypes.NameIdentifier);

            shoppingCart.UserID = claim.Value;

            shoppingCart = unitOfWork.cart.GetFirst(x => x.UserID == shoppingCart.UserID &&
             x.Id == shoppingCart.Id
             );
            if (shoppingCart != null)
            {
                unitOfWork.cart.Delete(shoppingCart);

            }


            unitOfWork.complete();

            return RedirectToAction("Index");
        }

        public IActionResult Shipping()
        {
            return RedirectToAction("Index");
        }
    }
}
