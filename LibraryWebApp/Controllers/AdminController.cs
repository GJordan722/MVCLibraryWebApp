using Microsoft.AspNetCore.Mvc;
using LibraryWebApp.Models;

namespace LibraryWebApp.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public IActionResult MediaControl(MediaModel model)
        {
            User? user = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "Profile");
            if (user == null || user.Role_ID == 3)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(model);
            }
        }
        [HttpGet]
        public IActionResult Delete(MediaModel model)
        {
            User? user = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "Profile");
            if (user == null || user.Role_ID == 3)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult DeleteMedia(MediaModel model)
        {
            User? user = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "Profile");
            if (user == null || user.Role_ID == 3)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                LibraryBLL LBLL = new LibraryBLL();
                Mapper mapper = new Mapper();
                Media media = mapper.MediaModelFillMedia(model);
                LBLL.DeleteMedia(media);
                return RedirectToAction("Browse", "Search");
            }

        }

        [HttpGet]
        public IActionResult Edit(MediaModel model)
        {
            User? user = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "Profile");
            if (user == null || user.Role_ID == 3)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                SessionHelper.SetObjectAsJson(HttpContext.Session, "OldModel", model);
                return RedirectToAction("EditMedia", "Admin");
            }
           
        }

        [HttpGet]
        public IActionResult EditMedia()
        {
            User? user = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "Profile");
            if (user == null || user.Role_ID == 3)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                MediaModel oldmodel = SessionHelper.GetObjectFromJson<MediaModel>(HttpContext.Session, "OldModel");
                return View(oldmodel);
            }
            
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
        public IActionResult AddMedia()
        {
            User? user = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "Profile");
            if(user == null || user.Role_ID == 3)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

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
            User? user = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "Profile");
            if (user == null || user.Role_ID == 3)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                LibraryBLL LBLL = new LibraryBLL();
                Mapper mapper = new Mapper();
                SearchModel model = new SearchModel();
                List<CheckIO> mediaList = LBLL.ViewCheckIO();
                List<CheckIOModel> modelList = new List<CheckIOModel>();
                foreach (CheckIO m in mediaList)
                {
                    modelList.Add(mapper.CheckIOFillCheckIOMedia(m));
                }
                model.checkiolist = modelList;
                return View(model);
            }
           
        }
        [HttpGet]
        public IActionResult ViewHoldIO()
        {
            User? user = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "Profile");
            if (user == null || user.Role_ID == 3)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                LibraryBLL LBLL = new LibraryBLL();
                Mapper mapper = new Mapper();
                SearchModel model = new SearchModel();
                List<HoldIO> mediaList = LBLL.ViewHoldIO();
                List<HoldIOModel> modelList = new List<HoldIOModel>();
                foreach (HoldIO m in mediaList)
                {
                    modelList.Add(mapper.HoldIOFillHoldIOMedia(m));
                }
                model.holdiolist = modelList;
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult EditUser()
        {
            User? user = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "Profile");
            if (user == null || user.Role_ID == 3)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                LibraryBLL LBLL = new LibraryBLL();
                Mapper mapper = new Mapper();
                List<User> userlist = LBLL.GetUsers();
                List<UserModel> umList = new List<UserModel>();
                foreach (User u in userlist)
                {
                    umList.Add(mapper.UserFillUserModel(u));
                }
                ListUserModel model = new ListUserModel();
                model.ListUsers = umList;
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult EditU(UserModel user)
        {
            User? _user = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "Profile");
            if (_user == null || _user.Role_ID == 3)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                SessionHelper.SetObjectAsJson(HttpContext.Session, "ProfileEdit", user);
                return View(user);
            }
        }

        [HttpPost]
        public IActionResult EditUser(UserModel user)
        {
            User? _user = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "Profile");
            if (_user == null || _user.Role_ID == 3)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                User oldUserInfo = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "ProfileEdit");
                LibraryBLL LBLL = new LibraryBLL();
                Mapper mapper = new Mapper();
                if(user.Username == oldUserInfo.Username)
                {
                    user.Username = null;
                }
                if (user.Password == oldUserInfo.Password)
                {
                    user.Password = null;
                }
                if (user.Role_ID == oldUserInfo.Role_ID)
                {
                    user.Role_ID = null;
                }
                if (user.Email == oldUserInfo.Email)
                {
                    user.Email = null;
                }
                if (user.FirstName == oldUserInfo.FirstName)
                {
                    user.FirstName = null;
                }
                if (user.LastName == oldUserInfo.LastName)
                {
                    user.LastName = null;
                }
                User Useruser = mapper.UserModelFillUser(user);
                bool update = LBLL.updateUser(Useruser, oldUserInfo);
                if (update)
                {
                    return RedirectToAction("EditUser", "Admin");
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session,"ProfileEdit",null);
                return View();
            }
            
        }
    }
}
