using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping.DataAccessLayer.Repositorys.IRepository;
using Shopping.DataAccessLayer.Repositorys.Repository;
using Shopping.Entites.Model;
using Shopping.Entites.Model.ViewModels;
using Shopping.Utilities;
using Stripe;

using Stripe.FinancialConnections;
using System.Security.Claims;


using Stripe.Checkout;
using Microsoft.Extensions.Options;
using X.PagedList;

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
        public IActionResult Index(int? page)
        {

            var pagenumber = page ?? 1;
            int pagesize = 8;



            return View(unitOfWork.product.Get().ToPagedList(pagenumber, pagesize));
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

             
               



            }
            else
            {
                shoppingCart.Id = OldData.Id;
                unitOfWork.cart.Update(shoppingCart);
                



            }

            unitOfWork.complete();
            HttpContext.Session.SetInt32(SD.SessionCountCart, unitOfWork.cart.Get(x => x.UserID == claim.Value).ToList().Count());

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

		CartViewModel cartViewModel;
		[HttpPost]

        //Show Stripe And Fill Data  Customer Not Pay  
        //Redirect To Function orderconfirm

        public IActionResult CheckOut(OrderHeader orderHeader)
        {
           
            var user = (ClaimsIdentity)User.Identity;
            var claim = user.FindFirst(ClaimTypes.NameIdentifier);

            string userid = claim.Value;

			//CartViewModel cartViewModel = new CartViewModel()
			 cartViewModel = new CartViewModel()

			{
				carts = unitOfWork.cart.Get(x => x.UserID == userid, include: "Product"),
                //Initilize
                orderheader = new()

            };
            cartViewModel.orderheader.City = orderHeader.City;
            cartViewModel.orderheader.Name = orderHeader.Name;

            cartViewModel.orderheader.NameUserID = userid;


            cartViewModel.orderheader.Address= orderHeader.Address;

            cartViewModel.orderheader.PaymentStatus = SD.Pending;
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

                //var domain = "https://localhost:7133/";


                ////var options = new Stripe.Checkout.SessionCreateOptions
                ////{
                ////    LineItems = new List<SessionLineItemOptions>(),
                ////    Mode = "payment",
                ////    SuccessUrl = domainName + $"customer/home/orderconfirm?id={cartViewModel.orderheader.ID}",
                ////    CancelUrl = domainName + $"customer/cart/index",
                ////};

                //var options = new Stripe.Checkout.SessionCreateOptions
                //{
                //    PaymentMethodTypes=new List<string>
                //    {
                //        "card",
                //    },

                //    LineItems = new List<SessionLineItemOptions>(),
                //    Mode = "payment",
                    
                //    SuccessUrl = domain+$"customer/home/orderconfirm?id={cartViewModel.orderheader.ID}",
                //    CancelUrl = domain+$"customer/cart/index",
                //};

                ////Get All Data From 

                //foreach (var itemcart in cartViewModel.carts)
                //{
                //    var sessionlineItem = new SessionLineItemOptions
                //    {

                //        PriceData = new SessionLineItemPriceDataOptions
                //        {

                //            UnitAmount = (long)(itemcart.Product.Price * 100),
                //            Currency = "usd",
                //            ProductData = new SessionLineItemPriceDataProductDataOptions
                //            {
                //                Name = itemcart.Product.Name,
                //            },
                //        },
                //        Quantity = itemcart.Count,



                //    };
                //    options.LineItems.Add(sessionlineItem);
                //}

                //var service = new Stripe.Checkout.SessionService();
                //Stripe.Checkout.Session session = service.Create(options);

                //cartViewModel.orderheader.SessionID = session.Id;
                //unitOfWork.complete();
                //Response.Headers.Add("Location", session.Url);
                //return new StatusCodeResult(303);



            }




            return RedirectToAction("Index");
        }
        //Customer Pay Money and Change Status From Pending To Approve
        public IActionResult orderconfirm(int id)
        {
            OrderHeader orderHeader = unitOfWork.orderHeader.GetFirst(x => x.ID == id);
            var service = new Stripe.Checkout.SessionService();
            Stripe.Checkout.Session session = service.Get(orderHeader.SessionID);
            orderHeader.PaymentIntentId = session.PaymentIntentId;

            if (session.PaymentStatus.ToLower() == "paid")
            {
                unitOfWork.orderHeader.UpdateStatusOrder(id, SD.Approve, SD.Approve);

                //PaymentIntentId Applay After 
                orderHeader.PaymentIntentId = session.PaymentIntentId;

                unitOfWork.complete();

               
               
            }

            //Remove In Cart  Specific User Login 
            List<ShoppingCart> shoppingCarts = unitOfWork.cart.Get(x => x.UserID == orderHeader.NameUserID).ToList();

            unitOfWork.cart.DeleteRange(shoppingCarts);


            //I Want Delete Session
            HttpContext.Session.SetInt32(SD.SessionCountCart, unitOfWork.cart.Get(x => x.UserID ==  orderHeader.NameUserID).ToList().Count());

            unitOfWork.complete();
            return View(id);


        }
        public IActionResult Plus(int Id)
        {
        
            var OldData = unitOfWork.cart.GetFirst(x=>x.Id== Id);
           
            unitOfWork.cart.IncreaseNum(OldData,1);
            unitOfWork.complete();
            //return RedirectToAction("Index");
            HttpContext.Session.SetInt32(SD.SessionCountCart, unitOfWork.cart.Get(x => x.UserID == OldData.UserID).ToList().Count());

            return RedirectToAction(nameof(GetCart));


     


        }
        public IActionResult Minus(int Id)
        {
          
            var OldData = unitOfWork.cart.GetFirst(x=>x.Id == Id);

            if (OldData.Count == 1)
            {
              


                unitOfWork.cart.Delete(OldData);





                unitOfWork.complete();
                HttpContext.Session.SetInt32(SD.SessionCountCart, unitOfWork.cart.Get(x => x.UserID == OldData.UserID).ToList().Count());


                return RedirectToAction("Index");
            }
            else
            {
                unitOfWork.cart.DecreaseNum(OldData, 1);
                unitOfWork.complete();
                //return RedirectToAction("Index");
                HttpContext.Session.SetInt32(SD.SessionCountCart, unitOfWork.cart.Get(x => x.UserID == OldData.UserID).ToList().Count());


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
            HttpContext.Session.SetInt32(SD.SessionCountCart, unitOfWork.cart.Get(x => x.UserID == claim.Value).ToList().Count());


            return RedirectToAction("Index");
        }

        public IActionResult Shipping()
        {
            return RedirectToAction("Index");
        }
    }
}
