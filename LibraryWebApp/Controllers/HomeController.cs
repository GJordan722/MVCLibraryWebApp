using LibraryWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LibraryWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
                return View();
        }

        [HttpGet]
        public IActionResult Dashboard()
        github
            {
            User user = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "Profile");
            if(user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Mapper mapper = new Mapper();
            UserModel model = mapper.UserFillUserModel(user);
            if (ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult LogOut()
        {
            SessionHelper.SetObjectAsJson(HttpContext.Session, "Profile", null);
            return RedirectToAction("Index","Home");
        }
        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Login(UserModel user)
        {
            LibraryBLL LBLL = new LibraryBLL();
            Mapper mapper = new Mapper();
            User transfer = LBLL.Login(user.Username, user.Password);
            if(transfer != null)
            {
                SessionHelper.SetObjectAsJson(HttpContext.Session, "Profile", transfer);
                return RedirectToAction("Dashboard", "Home");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Register(UserModel user)
        {
            LibraryBLL LBLL = new LibraryBLL();
            Random rand = new Random();
            Mapper mapper = new Mapper();
            user.Account_ID = rand.Next();
            user.Role_ID = 3;
            User transfer = mapper.UserModelFillUser(user);
            if (LBLL.Register(transfer))
            {
                SessionHelper.SetObjectAsJson(HttpContext.Session, "Profile", transfer);
                return RedirectToAction("Dashboard", "Home");
            }
            else
            {
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}