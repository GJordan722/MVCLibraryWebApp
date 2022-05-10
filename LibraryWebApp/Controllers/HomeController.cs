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
        public IActionResult Index(UserModel user)
        {
            if (ModelState.IsValid)
            {
                return View(user);
            }
            else
            {
                return View(new UserModel() { FirstName =""});
            }
            
        }
        public IActionResult Privacy()
        {
            return View();
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
            User transfer = LBLL.Login(user.Username, user.Password);
            int result = transfer.Role_ID;
            if(result == 1 || result == 2 || result == 3)
            {
                user.Email = transfer.Email;
                user.Username = transfer.Username;
                user.Password = transfer.Password;
                user.Account_ID = transfer.Account_ID;
                user.Role_ID = transfer.Role_ID;
                user.FirstName = transfer.FirstName;
                user.LastName = transfer.LastName;
                return RedirectToAction("Index", "Home",user);
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
            User transfer = new User();
            transfer.Email      =user.Email;     
            transfer.Username   =user.Username;
            transfer.Password   =user.Password;
            transfer.Account_ID = LBLL.Register(transfer);
            transfer.Role_ID    =user.Role_ID;
            transfer.FirstName  =user.FirstName;
            transfer.LastName   =user.LastName;
           
            if(transfer.Account_ID != 0)
            {
                return RedirectToAction("Index", "Home",user);
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