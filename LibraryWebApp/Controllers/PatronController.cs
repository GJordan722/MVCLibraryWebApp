﻿using LibraryWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Controllers
{
    public class PatronController : Controller
    {
        [HttpGet]
        public IActionResult CheckIO(MediaModel mm)
        {
            User _user = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "Profile");
            LibraryBLL LBLL = new LibraryBLL();
            Mapper mapper = new Mapper();
            Media model = mapper.MediaModelFillMedia(mm);
            if (LBLL.CheckIO(model, _user))
            {
                return RedirectToAction("Dashboard", "Home");
            }

            return RedirectToAction("Browse","Search");
        }

        [HttpGet]
        public IActionResult HoldIO(MediaModel mm)
        {
            User _user = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "Profile");
            LibraryBLL LBLL = new LibraryBLL();
            Mapper mapper = new Mapper();
            Media model = mapper.MediaModelFillMedia(mm);
            if (LBLL.HoldIO(model, _user))
            {
                return RedirectToAction("Dashboard", "Home");
            }

            return RedirectToAction("Browse", "Search");
        }

        [HttpGet]
        public IActionResult ShowCheckIO()
        {
            User _user = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "Profile");
            LibraryBLL LBLL = new LibraryBLL();
            Mapper mapper = new Mapper();
            SearchModel model = new SearchModel();
            List<Media> mediaList = LBLL.patronViewCheckIO(_user);
            List<MediaModel> modelList = new List<MediaModel>();
            foreach (Media m in mediaList)
            {
                modelList.Add(mapper.MediaFillMediaModel(m));
            }
            model.MediaList = modelList;
            return View(model);
        }
        [HttpGet]
        public IActionResult ShowHoldIO()
        {
            User _user = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "Profile");
            LibraryBLL LBLL = new LibraryBLL();
            Mapper mapper = new Mapper();
            SearchModel model = new SearchModel();
            List<HoldIO> mediaList = LBLL.patronViewHoldIO(_user);
            List<HoldIOModel> modelList = new List<HoldIOModel>();
            foreach (HoldIO m in mediaList)
            {
                modelList.Add(mapper.HoldIOFillHoldIOMedia(m));
            }
            model.holdiolist = modelList;
            return View(model);
        }
    }
}