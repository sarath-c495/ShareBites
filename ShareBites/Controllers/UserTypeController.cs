using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShareBites.Models;
using System.Drawing;
using System.Security.Claims;

namespace ShareBites.Controllers
{
    public class UserTypeController : Controller
    {
        private readonly ShareBitesContext _dbContext;
        private  int _userId;

        public UserTypeController(ShareBitesContext dbContext)
        {
            _dbContext = dbContext;
            
        }
        public IActionResult Index() 
        {
            return View();
        }
        //[Authorize]
        //public IActionResult Register()
        //{
            
        //    int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
        //    //var role = User.Identity.GetClaimValue(ClaimTypes.Role);
        //    var userIdentity = (ClaimsIdentity)User.Identity;
        //    var roleClaim = userIdentity.FindFirst(ClaimTypes.Role);

        //    if (userId != 0)
        //    {
        //        switch (roleClaim.Value)
        //        {
        //            case "shelter":                        
        //                return RedirectToAction("ShelterReg");                                
        //            case "Sponsor":
        //                return RedirectToAction("SponsorReg");

        //            case "helper":

        //                return RedirectToAction("HelperReg");
        //            case "Restaurant":

        //                return RedirectToAction("RestReg");
        //            default: 
        //                break;
        //        }
                
        //    }
        //    return View();
        //}
        [Authorize]
        public IActionResult SponsorReg()
        {
            ViewBag.RegionId = new SelectList(_dbContext.RegionMasters.ToList(), "RegionId", "RegionName");
            ViewBag.ModeOfHelp = new SelectList(_dbContext.ModeOfhelpMasters.ToList(), "HelpId", "ModeOfHelp");

            return View();
        }
        [Authorize]
        public IActionResult ShelterReg()
        {
            ViewBag.RegionId = new SelectList(_dbContext.RegionMasters.ToList(), "RegionId", "RegionName");
            //ViewBag.ModeOfHelp = new SelectList(_dbContext.ModeOfhelpMasters.ToList(), "HelpId", "ModeOfHelp");

            return View();
        }
        [Authorize]
        public IActionResult HelperReg()
        {
            ViewBag.RegionId = new SelectList(_dbContext.RegionMasters.ToList(), "RegionId", "RegionName");
            ViewBag.ModeOfHelp = "del1";

            return View();
        }
        [Authorize]
        public IActionResult RestReg()
        {
            ViewBag.RegionId = new SelectList(_dbContext.RegionMasters.ToList(), "RegionId", "RegionName");
            ViewBag.ModeOfHelp = new SelectList(_dbContext.ModeOfhelpMasters.ToList(), "HelpId", "ModeOfHelp");

            return View();
        }
        public IActionResult AddSponsor(Sponsor sponsor)
        {
            if (ModelState.IsValid)
            {
                _userId = HttpContext.Session.GetInt32("UserId") ?? 0;
                
                sponsor.LoginId = _userId;
                _dbContext.Sponsors.Add(sponsor);
                _dbContext.SaveChanges();
                ViewBag.success = true;

            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult AddRest(Restaurant rest)
        {
            if (ModelState.IsValid)
            {
                _userId = HttpContext.Session.GetInt32("UserId") ?? 0;
                rest.LoginId = _userId;
                _dbContext.Restaurants.Add(rest);
                _dbContext.SaveChanges();
                ViewBag.success = true;

            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult AddShelter(Shelter shelter)
        {
            if (ModelState.IsValid)
            {
                _userId = HttpContext.Session.GetInt32("UserId") ?? 0;
                shelter.LoginId = _userId;
                _dbContext.Shelters.Add(shelter);
                _dbContext.SaveChanges();
                ViewBag.success = true;

            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult AddHelper(Helper helper)
        {
            
            if (ModelState.IsValid)
            {
                _userId = HttpContext.Session.GetInt32("UserId") ?? 0;
                helper.LoginId = _userId;
                helper.ModeOfHelp = "del1";
                _dbContext.Helpers.Add(helper);
                _dbContext.SaveChanges();
                ViewBag.success = true;

            }
            return RedirectToAction("Index", "Home");
        }

    }
}
