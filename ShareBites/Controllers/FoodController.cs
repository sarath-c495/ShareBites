using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShareBites.Models;
using System.Data;
using System.Security.Claims;

namespace ShareBites.Controllers
{
    public class FoodController : Controller
    {
        private readonly ShareBitesContext _dbContext;
        private int _userId;

        public FoodController(ShareBitesContext dbContext)
        {
            _dbContext = dbContext;

        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Restaurant")]
        public IActionResult AddFood()
        {
            string userId = User.FindFirstValue(ClaimTypes.Name); 
            //_userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            int resId = _dbContext.Restaurants.Where(p => p.LoginId.ToString() == userId).Select(p => p.ResId).FirstOrDefault();
            var statusList = new List<SelectListItem>
            {
                new SelectListItem { Text = "Available", Value = "Available" },
               
            };

            ViewBag.StatusList = statusList;
            ViewBag.ResId = resId;

            return View();
        }

        public IActionResult SaveFoodDB(ResFoodHandler food)
        {
            if (ModelState.IsValid)
            {
                _dbContext.ResFoodHandlers.Add(food);
                _dbContext.SaveChanges();
            }
            return RedirectToAction("ViewFood");
        }
        public IActionResult EditFood(int id)
        {
            var food = _dbContext.ResFoodHandlers.FirstOrDefault(f => f.FoodId == id);
            var statusList = new List<SelectListItem>
            {
                new SelectListItem { Text = "Available", Value = "Available" },
                new SelectListItem { Text = "Ordered", Value = "Ordered" },
                new SelectListItem { Text = "Picked Up", Value = "Picked Up" }
            };

            ViewBag.StatusList = statusList;

            return View(food);
        }

        public IActionResult UpdFood(ResFoodHandler food)
        {
            var existingFood = _dbContext.ResFoodHandlers.FirstOrDefault(f => f.FoodId == food.FoodId);
            if (existingFood != null)
            {
                existingFood.DateAndTime = food.DateAndTime;
                existingFood.WaitingTime = food.WaitingTime;
                existingFood.FoodDesc = food.FoodDesc;
                existingFood.FoodStatus = food.FoodStatus;

                _dbContext.SaveChanges();
            }
            //_dbContext.SaveChanges();
            return RedirectToAction("ViewFood");
        }


        public IActionResult OrderConfirm(int id)
        {
            var food = _dbContext.ResFoodHandlers
            .Include(f => f.Res)
            .ThenInclude(r => r.Region).FirstOrDefault(f => f.FoodId == id);

            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }
        [Authorize(Roles = "shelter")]
        public IActionResult OrderFood(int id)
        {
            //_userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            var userid = User.FindFirstValue(ClaimTypes.Name);
            var shelter = _dbContext.Shelters.FirstOrDefault(u => u.LoginId.ToString() == userid);
            ExcessFoodOrder order = new ExcessFoodOrder();
            order.ShelterId = shelter.ShelterId;
            order.FoodId = id;
            order.ModeOfdelivery = shelter.HasCar == true ? "self" : null;
            order.OrderDate = DateTime.Now;

            _dbContext.ExcessFoodOrders.Add(order);
            

            var food = _dbContext.ResFoodHandlers.FirstOrDefault(u => u.FoodId== id);
            food.FoodStatus = "Ordered";
            _dbContext.SaveChanges();

            return RedirectToAction("ViewOrders");
        }

        [Authorize(Roles = "shelter")]
        public IActionResult ViewOrders()
        {
            //_userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            var userid = User.FindFirstValue(ClaimTypes.Name);
            var shelter = _dbContext.Shelters.FirstOrDefault(u => u.LoginId.ToString() == userid);
            var orders = _dbContext.ExcessFoodOrders
                            .Include(h=> h.Helper)
                            .Include(r => r.Food)
                            .ThenInclude(rs => rs.Res)
                            .Where(u => u.ShelterId == shelter.ShelterId).ToList();
            return View(orders);
        }

        public IActionResult OrderDetails(int id)
        {
            var userid = User.FindFirstValue(ClaimTypes.Name);
            var shelter = _dbContext.Shelters.FirstOrDefault(u => u.LoginId.ToString() == userid);
            var orders = _dbContext.ExcessFoodOrders
                            .Include(h => h.Helper)
                            .Include(r => r.Food)
                            .ThenInclude(rs => rs.Res)
                            .FirstOrDefault(u => u.ShelterId == shelter.ShelterId && u.OrderId== id);

            return View(orders);
        }
        [Authorize(Roles ="admin,shelter")]
        public IActionResult DeleteOrder(int id)
        {
            var order = _dbContext.ExcessFoodOrders
                .Include(h => h.Food)
                .ThenInclude(r=> r.Res)
                .FirstOrDefault(f => f.OrderId == id);           
            
            return View(order);
        }
        public IActionResult DeleteOrderConfirm(int OrderId)
        {
            var order = _dbContext.ExcessFoodOrders.FirstOrDefault(f => f.OrderId == OrderId);

            if(order != null)
            {
                _dbContext.ExcessFoodOrders.Remove(order);
                _dbContext.SaveChanges();
                ViewBag.StatusMsg = "Deleted Successfully";
            }            

            return RedirectToAction("ViewOrders");
        }
        public IActionResult ViewFood(string resname, string? region, DateTime? fooddate, string sortBy, string sortOrder, int page = 1, int pageSize = 4)
        {

            var food = _dbContext.ResFoodHandlers
            .Include(f => f.Res)
            .ThenInclude(r => r.Region)
            .ToList();

             var role = User.FindFirstValue(ClaimTypes.Role);
             var userid = User.FindFirstValue(ClaimTypes.Name);
            if (!string.IsNullOrEmpty(role) && role == "Restaurant")
            {
                food = food.Where(r => r.Res.LoginId.ToString() == userid).ToList();
            }

            if (!string.IsNullOrEmpty(resname))
            {
                food = food.Where(p => p.Res.Name.ToLower().Contains(resname.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(region))
            {
                food = food.Where(r => r.Res.Region.RegionName.ToLower().Contains(region.ToLower())).ToList();
            }

            if (fooddate.HasValue)
            {
                food = food.Where(p => p.DateAndTime >= fooddate.Value).ToList();
            }

            // Sort by selected column and order
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "resname":
                        food = sortOrder.ToLower() == "asc" ? food.OrderBy(p => p.Res.Name).ToList() : food.OrderByDescending(p => p.Res.Name).ToList();
                        break;
                    case "region":
                        food = sortOrder.ToLower() == "asc" ? food.OrderBy(p => p.Res.Region.RegionName).ToList() : food.OrderByDescending(p => p.Res.Region.RegionName).ToList();
                        break;
                    case "fooddate":
                        food = sortOrder.ToLower() == "asc" ? food.OrderBy(p => p.DateAndTime).ToList() : food.OrderByDescending(p => p.DateAndTime).ToList();
                        break;
                }
            }

            // Paginate the result set
            int totalItems = food.Count();
            int totalPages = (int)Math.Ceiling((decimal)totalItems / pageSize);
            int skip = (page - 1) * pageSize;
            food = food.Skip(skip).Take(pageSize).ToList();

            ViewBag.resname = resname;
            ViewBag.region = region;
            ViewBag.fooddate = fooddate;
            ViewBag.SortBy = sortBy;
            ViewBag.SortOrder = sortOrder;
            ViewBag.Page = page;
            ViewBag.TotalPages = totalPages;

            return View(food);
        }

    }
}
