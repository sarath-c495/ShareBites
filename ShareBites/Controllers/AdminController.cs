using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareBites.Models;

namespace ShareBites.Controllers
{
    public class AdminController : Controller
    {
        private readonly ShareBitesContext _dbContext;

        public AdminController(ShareBitesContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Authorize(Roles ="admin")]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateRegion()
        {
            ViewBag.success = false;
            return View();
        }

        [HttpPost]
        public IActionResult AddRegion(RegionMaster region)
        {
            if (!ModelState.IsValid)
            {
                _dbContext.RegionMasters.Add(region);
                _dbContext.SaveChanges();
                ViewBag.success = true;
            }
            return View("CreateRegion");
        }
        
        public IActionResult GetRegions()
        {
            var regions = _dbContext.RegionMasters.ToList();
            return View(regions);
            
        }
        public IActionResult GetSponsor()
        {
            var sponsors = _dbContext.Sponsors.ToList();
            return View(sponsors);

        }
        
    }
}
