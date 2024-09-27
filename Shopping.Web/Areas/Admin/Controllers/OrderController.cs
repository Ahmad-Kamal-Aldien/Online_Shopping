using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping.DataAccessLayer.Repositorys.IRepository;
using Shopping.Entites.Model;
using Shopping.Entites.Model.ViewModels;
using Shopping.Utilities;
using Stripe;
using System.Collections.Generic;

namespace Shopping.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.Admin)]
    [Area("Admin")]
    public class OrderController : Controller
    {
        IUnitOfWork UnitOfWork;
        public OrderController(IUnitOfWork _UnitOfWork)
        {
            UnitOfWork = _UnitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable < OrderHeader> OrderHeader = UnitOfWork.orderHeader.Get(include: "UserData");


          //  IEnumerable<OrderHeader> OrderHeader = UnitOfWork.orderHeader.Get();








            return Json(new { data = OrderHeader });
        }

        public IActionResult Details(int id)
        {
            OrderViewModel orderViewModel = new OrderViewModel()
            {
                OrderHeader = UnitOfWork.orderHeader.GetFirst(x => x.ID == id, include: "UserData"),
                OrderDetails = UnitOfWork.orderDetails.Get(x => x.OrderID == id, include: "Product")
            };
            return View(orderViewModel);
        }


        [BindProperty]

        public OrderViewModel orderViewModel { get; set; }
        public IActionResult Update()
        {
            var order = UnitOfWork.orderHeader.GetFirst(x=>x.ID== orderViewModel.OrderHeader.ID);
            order.Name=orderViewModel.OrderHeader.Name;
            order.Carrier=orderViewModel.OrderHeader.Carrier;
            order.PhoneNumber=orderViewModel.OrderHeader.PhoneNumber;
            order.City=orderViewModel.OrderHeader.City;
            order.Address=orderViewModel.OrderHeader.Address;





           UnitOfWork.orderHeader.Update(order);



        

       
            UnitOfWork.complete();

            TempData["Edit"] = "Data Updated Success";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult UpdateToProcess()
        {
            UnitOfWork.orderHeader.UpdateStatusOrder(orderViewModel.OrderHeader.ID, SD.Proccessing,SD.Approve);
            UnitOfWork.complete();
            return RedirectToAction(nameof(Index));
        }

        //Add Exstra Data As Carier And Tracking Num 

        public IActionResult UpdateToShiping()
        {
            var order = UnitOfWork.orderHeader.GetFirst(x => x.ID == orderViewModel.OrderHeader.ID);
            order.TrackingNumber = orderViewModel.OrderHeader.TrackingNumber;
            order.Carrier = orderViewModel.OrderHeader.Carrier;
            order.orderStatus = SD.Shipped;
            order.ShippingDate = DateTime.Now;
            UnitOfWork.orderHeader.Update(order);


            UnitOfWork.complete();
            return RedirectToAction(nameof(Index));
        }

        //Two Status For Payment (Approve Or Appending)
        public IActionResult Cancel()
        {
            var order = UnitOfWork.orderHeader.GetFirst(x => x.ID == orderViewModel.OrderHeader.ID);

            if (order.PaymentStatus == SD.Approve)
            {
                var option = new RefundCreateOptions
                {
                    Reason = RefundReasons.RequestedByCustomer,
                    PaymentIntent = order.PaymentIntentId
                };
                var services = new RefundService();
                Refund refund = services.Create(option);
                UnitOfWork.orderHeader.UpdateStatusOrder(order.ID,SD.Cancel,SD.Refund);
            }
            if(order.PaymentStatus == SD.Pending)
            {
                UnitOfWork.orderHeader.UpdateStatusOrder(order.ID, SD.Cancel, SD.Cancel);

            }
            UnitOfWork.complete();
            TempData["Edit"] = "Data Updated Success";
            return RedirectToAction(nameof(Index));
        }
    }
}
