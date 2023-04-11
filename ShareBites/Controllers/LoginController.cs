using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShareBites.Models;
using System.Security.Claims;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ShareBites.Controllers
{
    public class LoginController : Controller
    {
        private readonly ShareBitesContext _dbContext;

        public LoginController(ShareBitesContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            //int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            return View();
        }
        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {

            if (string.IsNullOrEmpty(ReturnUrl))
            {
                ReturnUrl = Request.Headers["Referer"];
            }

            ViewData["returnUrl"] = ReturnUrl;
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser(string username, string password, string returnUrl)
        {
            bool result = false;
            var existingUser = _dbContext.UserLogins.FirstOrDefault(u => u.Username == username && u.Password == password);

            UserTypeMaster usertype = null; ;

            if (existingUser != null)
            {
                usertype = _dbContext.UserTypeMasters.SingleOrDefault(ut => ut.UserTypeId == existingUser.UserTypeId);
                result = true;
            }

            if (result)
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, username));

                claims.Add(new Claim(ClaimTypes.Role, usertype.UserType));

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties
                {
                    IsPersistent = true 
                    
                });

                HttpContext.Session.SetInt32("UserId", existingUser.LoginId);

                if (!string.IsNullOrEmpty(returnUrl))
                    return Redirect(returnUrl);
                else
                    return Redirect("/");
            }
            TempData["ErrorMessage"] = "Invalid username or password";
            return View("login");

            //if ((username == "abcd1234") && (password == "abcd1234"))
            //{
            //    result = true;
            //}
            //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
            //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30) // set to the desired expiration time
        }
        public IActionResult Register()
        {
            ViewBag.UserType = new SelectList(_dbContext.UserTypeMasters.Where(ut=> ut.UserType != "admin").ToList(), "UserTypeId", "UserType");
            return View();
        }
        [HttpPost]
        public IActionResult RegisterUser(UserLogin user)
        {
            if (ModelState.ContainsKey("UserType"))
            {
                ModelState["UserType"].Errors.Clear();
                ModelState["UserType"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
            }
            if (ModelState.IsValid)
            {
                //var existingProduct = _dbContext.UserLogins.FirstOrDefault(p => p.Username == user.Username);

                //if (existingProduct != null)
                //{
                //    //ModelState.AddModelError(nameof(user.Username), $"Product ID {user.Username} already exists.");
                //    return View("Register",user);
                //}
               _dbContext.UserLogins.Add(user);
               _dbContext.SaveChanges();
                return RedirectToAction("RegisterUserTypes");
            }
            else
                return RedirectToAction("Register");
        }


        [Authorize]
        public IActionResult RegisterUserTypes()
        {

            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            //var role = User.Identity.GetClaimValue(ClaimTypes.Role);
            var userIdentity = (ClaimsIdentity)User.Identity;
            var roleClaim = userIdentity.FindFirst(ClaimTypes.Role);

            if (userId != 0)
            {
                switch (roleClaim.Value)
                {
                    case "shelter":
                        return RedirectToAction("ShelterReg","UserType");
                    case "Sponsor":
                        return RedirectToAction("SponsorReg", "UserType");

                    case "helper":

                        return RedirectToAction("HelperReg", "UserType");
                    case "Restaurant":

                        return RedirectToAction("RestReg", "UserType");
                    default:
                        break;
                }

            }
            return View();
        }
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
        public JsonResult CheckId(string Username)
        {
            string msg = CheckID.IDExists(_dbContext, Username);
            if (string.IsNullOrEmpty(msg))
            {
                return Json(true);
            }
            else
            {
                return Json(msg);
            }
        }
    }
}
