using LibraryWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LibraryWebApp.Controllers
{
    public class SearchController : Controller
    {
        [HttpGet]
        public IActionResult Browse(Media search)
        {
            Mapper mapper = new Mapper();
            LibraryBLL LBLL = new LibraryBLL();
            Search searchList = LBLL.Search(search);
            SearchModel Browse = mapper.FillOutMediaList(searchList);
            User user = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "Profile");
            if(user == null)
            {
                return View(Browse);
            }
            switch (user.Role_ID)
            {
                case 1:
                    Browse.access = 1;
                    break;
                case 2:
                    Browse.access = 2;
                    break;
                case 3:
                    Browse.access = 3;
                    break;
                default:
                    break;
            }
            return View(Browse);
        }


        [HttpPost]
        public IActionResult Search(IFormCollection search)
        {
            string damn = search["Search"];
            Mapper mapper = new Mapper();
            MediaModel searchModel = mapper.SetSearchMediaModel(damn);
            Media searchQuery = mapper.MediaModelFillMedia(searchModel);
            return RedirectToAction("Browse", "Search", searchQuery);
        }
    }
}
