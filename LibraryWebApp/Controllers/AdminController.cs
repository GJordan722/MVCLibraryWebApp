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
            return RedirectToAction("Browse", "Search");
        }

        [HttpGet]
        public IActionResult Edit(MediaModel model)
        {
            SessionHelper.SetObjectAsJson(HttpContext.Session, "OldModel", model);
            return RedirectToAction("EditMedia", "Admin");
        }

        [HttpGet]
        public IActionResult EditMedia()
        {
            MediaModel oldmodel = SessionHelper.GetObjectFromJson<MediaModel>(HttpContext.Session, "OldModel");
            return View(oldmodel);
        }

        [HttpPost]
        public IActionResult EditMedia(MediaModel media)
        {
            LibraryBLL LBLL = new LibraryBLL();
            Mapper mapper = new Mapper();
            Media model = mapper.MediaModelFillMedia(media);
            MediaModel oldmodel = SessionHelper.GetObjectFromJson<MediaModel>(HttpContext.Session, "OldModel");
            Media old = mapper.MediaModelFillMedia(oldmodel);
            if (LBLL.UpdateMedia(old, model))
            {
                return RedirectToAction("Browse", "Search");
            }
            else
            {
                return View(media);
            }

        }
        [HttpGet]
        public IActionResult AddMedia() => View();

        [HttpPost]
        public IActionResult AddMedia(MediaModel media)
        {
            LibraryBLL LBLL = new LibraryBLL();
            Mapper mapper = new Mapper();
            Media model = mapper.MediaModelFillMedia(media);
            if (LBLL.addMedia(model))
            {
                return RedirectToAction("Browse", "Search");
            }
            else
            {
                return View(media);
            }
        }
        [HttpGet]
        public IActionResult ViewCheckIO()
        {
            LibraryBLL LBLL = new LibraryBLL();
            Mapper mapper = new Mapper();
            SearchModel model = new SearchModel();
            List<CheckIO> mediaList = LBLL.ViewCheckIO();
            List<CheckIOModel> modelList = new List<CheckIOModel>();
            foreach(CheckIO m in mediaList)
            {
                modelList.Add(mapper.CheckIOFillCheckIOMedia(m));
            }
            model.checkiolist = modelList;
            return View(model);
        }
    }
}
