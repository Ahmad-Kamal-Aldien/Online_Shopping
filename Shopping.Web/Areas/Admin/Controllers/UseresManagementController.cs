using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using Shopping.DataAccessLayer.Data;
using Shopping.DataAccessLayer.Repositorys.IRepository;
using Shopping.DataAccessLayer.Repositorys.Repository;
using Shopping.Utilities;

using System.Security.Claims;

namespace Shopping.Web.Areas.Admin.Controllers
{
    [Area("Admin")]

    //[Authorize(Roles = SD.Admin)]


    
    public class UseresManagementController : Controller
    {
        
        public ApplicationDBContext _context;
        public UseresManagementController(ApplicationDBContext context)
        {
            _context = context;

        }
        //Admin/UseresManagement/Index
        //Return All User Except Login
        public IActionResult Index()
        {

            //var user = (ClaimsIdentity)User.Identity;
            //var claim = user.FindFirst(ClaimTypes.NameIdentifier);

            //    string userid = claim.Value;
            //    //return View(_context.ExstraUserDatas.Where(x => x.Id != userid).ToList());
            //    return View(_context.ExstraUserDatas.ToList());


            var user = (ClaimsIdentity)User.Identity;
            var claim = user.FindFirst(ClaimTypes.NameIdentifier);
            string userid = claim.Value;

            return View(_context.ExstraUserDatas.Where(x=>x.Id!=userid)) ;


            ////All User
            //return View(_context.ExstraUserDatas.ToList());

        }

     
        public IActionResult StatusLock(string id)
        {
            var userid = _context.ExstraUserDatas.FirstOrDefault(x=>x.Id==id);
            if (userid == null)
            {
                return NotFound();
            }
            if(userid.LockoutEnd==null || userid.LockoutEnd < DateTime.Now)
            {
                userid.LockoutEnd = DateTime.Now.AddHours(1);
            }
            else
            {
                userid.LockoutEnd= DateTime.Now;
            }
            return View();
        }
    }
}
