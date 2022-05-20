using LibraryWebApp.Models;

namespace LibraryWebApp
{
    public class Mapper
    {
        public UserModel UserFillUserModel(User transfer)
        {
            UserModel user = new UserModel();
            user.Email = transfer.Email;
            user.Username = transfer.Username;
            user.Password = transfer.Password;
            user.Account_ID = transfer.Account_ID;
            user.Role_ID = transfer.Role_ID;
            user.FirstName = transfer.FirstName;
            user.LastName = transfer.LastName;
            return user;
        }

        public User UserModelFillUser(UserModel transfer)
        {
            User user = new User();
            user.Email = transfer.Email;
            user.Username = transfer.Username;
            user.Password = transfer.Password;
            user.Account_ID = transfer.Account_ID;
            user.Role_ID = transfer.Role_ID;
            user.FirstName = transfer.FirstName;
            user.LastName = transfer.LastName;
            return user;
        }

        public MediaModel MediaFillMediaModel(Media search)
        {
            MediaModel model = new MediaModel();
            model.Media_ID = search.Media_ID;
            model.Media_Name = search.Media_Name;
            model.Media_Type = search.Media_Type;
            model.Account_ID = search.Account_ID;
            model.Author = search.Author;
            model.Publisher = search.Publisher;
            return model;
        }

        public Media MediaModelFillMedia(MediaModel search)
        {
            Media model = new Media();
            model.Media_ID = search.Media_ID;
            model.Media_Name = search.Media_Name;
            model.Media_Type = search.Media_Type;
            model.Account_ID = search.Account_ID;
            model.Author = search.Author;
            model.Publisher = search.Publisher;
            return model;
        }

        public SearchModel FillOutMediaList(Search search)
        {
            SearchModel model = new SearchModel();
            model.MediaList = new List<MediaModel>();
            foreach (Media media in search.MediaList)
            {
                model.MediaList.Add(new MediaModel() { Media_ID = media.Media_ID,
                                                       Media_Name = media.Media_Name,
                                                       Media_Type = media.Media_Type,
                                                       Account_ID = media.Account_ID,
                                                       Author = media.Author,
                                                       Publisher = media.Publisher});
            }

            return model;
        }

        public MediaModel SetSearchMediaModel(string keyword, string category = "Keyword")
        {
            MediaModel search = new MediaModel();
            search.Media_Name = keyword;
            search.Author = keyword;
            search.Publisher = keyword;
            return search;
        }

        public CheckIOModel CheckIOFillCheckIOMedia(CheckIO checkio)
        {
            CheckIOModel CheckIOM = new CheckIOModel();
            CheckIOM.MediaID = checkio.MediaID;
            CheckIOM.MediaName = checkio.MediaName;
            CheckIOM.AccountID = checkio.AccountID;
            CheckIOM.Username = checkio.Username;
            CheckIOM.FirstName = checkio.FirstName;
            CheckIOM.LastName = checkio.LastName;
            return CheckIOM;
        }
        public HoldIOModel HoldIOFillHoldIOMedia(HoldIO hold)
        {
            HoldIOModel HoldIOM = new HoldIOModel();
            HoldIOM.MediaID = hold.MediaID;
            HoldIOM.MediaName = hold.MediaName;
            HoldIOM.AccountID = hold.AccountID;
            HoldIOM.Username = hold.Username;
            HoldIOM.Email = hold.Email;
            return HoldIOM;
        }
    }
}
