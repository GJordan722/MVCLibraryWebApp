using Microsoft.AspNetCore.Mvc;
using LibraryWebApp.Models;

namespace LibraryWebApp.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public IActionResult MediaControl(MediaModel model)
        {
            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(MediaModel model)
        {

            return View(model);
        }

        [HttpGet]
        public IActionResult DeleteMedia(MediaModel model)
        {
            LibraryBLL LBLL = new LibraryBLL();
            Mapper mapper = new Mapper();
            Media media = mapper.MediaModelFillMedia(model);
            LBLL.DeleteMedia(media);
            return RedirectToAction("Browse", "Search", media);
        }

        [HttpPost]
        public IActionResult Edit(MediaModel model)
        {
            SessionHelper.SetObjectAsJson(HttpContext.Session, "OldModel", model);
            return RedirectToAction("EditMedia", "Admin");
        }

        [HttpGet]
        public IActionResult EditMedia()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EditMedia(MediaModel media)
        {
            LibraryBLL LBLL = new LibraryBLL();
            Mapper mapper = new Mapper();
            Media model = mapper.MediaModelFillMedia(media);
            Media old = SessionHelper.GetObjectFromJson<Media>(HttpContext.Session, "OldModel");
            if (LBLL.UpdateMedia(old, model))
            {
                return RedirectToAction("Browse", "Search");
            }
            else
            {
                return View(media);
            }

        }
    }
}
