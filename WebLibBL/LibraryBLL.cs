using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace LibraryWebApp
{
    public class LibraryBLL
    {
        LibraryDB DB = new LibraryDB();

        public void SelectMedia(string mediaName = "", string mediaType = "")
        {
            DataTable dt = DB.viewMedia();

            if (mediaName != "" && mediaType != "")
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["media_name"] == mediaName && dr["media_type"] == mediaType)
                    {
                        Console.WriteLine($"Media Name: {dr["media_name"]} Media Type: {dr["media_type"]} Media ID: {dr["media_id"]} Author: {dr["author"]} Publisher: {dr["Publisher"]}");
                    }
                }
            }
            else if (mediaType == "")
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["media_name"] == mediaName)
                    {
                        Console.WriteLine($"Media Name: {dr["media_name"]} Media Type: {dr["media_type"]} Media ID: {dr["media_id"]} Author: {dr["author"]} Publisher: {dr["Publisher"]}");
                    }
                }
            }
            else if (mediaName == "")
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["media_type"] == mediaType)
                    {
                        Console.WriteLine($"Media Name: {dr["media_name"]} Media Type: {dr["media_type"]} Media ID: {dr["media_id"]} Author: {dr["author"]} Publisher: {dr["Publisher"]}");
                    }
                }
            }
        }
        public void PrintMedia()
        {
            DataTable data = DB.viewMedia();
            Console.WriteLine();
            Dictionary<string, int> colWidths = new Dictionary<string, int>();

            foreach (DataColumn col in data.Columns)
            {
                Console.Write(col.ColumnName);
                var maxLabelSize = data.Rows.OfType<DataRow>()
                        .Select(m => (m.Field<object>(col.ColumnName)?.ToString() ?? "").Length)
                        .OrderByDescending(m => m).FirstOrDefault();

                colWidths.Add(col.ColumnName, maxLabelSize);
                for (int i = 0; i < maxLabelSize - col.ColumnName.Length + 10; i++) Console.Write(" ");
            }

            Console.WriteLine();

            foreach (DataRow dataRow in data.Rows)
            {
                for (int j = 0; j < dataRow.ItemArray.Length; j++)
                {
                    Console.Write(dataRow.ItemArray[j]);
                    for (int i = 0; i < colWidths[data.Columns[j].ColumnName] - dataRow.ItemArray[j].ToString().Length + 10; i++) Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
      /*  public void PrintUsers()
        {
            DataTable data = DB.viewUsers();
            Console.WriteLine();
            Dictionary<string, int> colWidths = new Dictionary<string, int>();

            foreach (DataColumn col in data.Columns)
            {
                Console.Write(col.ColumnName);
                var maxLabelSize = data.Rows.OfType<DataRow>()
                        .Select(m => (m.Field<object>(col.ColumnName)?.ToString() ?? "").Length)
                        .OrderByDescending(m => m).FirstOrDefault();

                colWidths.Add(col.ColumnName, maxLabelSize);
                for (int i = 0; i < maxLabelSize - col.ColumnName.Length + 10; i++) Console.Write(" ");
            }

            Console.WriteLine();

            foreach (DataRow dataRow in data.Rows)
            {
                for (int j = 0; j < dataRow.ItemArray.Length; j++)
                {
                    Console.Write(dataRow.ItemArray[j]);
                    for (int i = 0; i < colWidths[data.Columns[j].ColumnName] - dataRow.ItemArray[j].ToString().Length + 10; i++) Console.Write(" ");
                }
                Console.WriteLine();
            }
        }*/

        public List<User> GetUsers()
        {
            List<User>? userList = DB.viewUsers();
            return userList;
        }
        public void PrintRoles()
        {
            DataTable data = DB.viewRoles();
            Console.WriteLine();
            Dictionary<string, int> colWidths = new Dictionary<string, int>();

            foreach (DataColumn col in data.Columns)
            {
                Console.Write(col.ColumnName);
                var maxLabelSize = data.Rows.OfType<DataRow>()
                        .Select(m => (m.Field<object>(col.ColumnName)?.ToString() ?? "").Length)
                        .OrderByDescending(m => m).FirstOrDefault();

                colWidths.Add(col.ColumnName, maxLabelSize);
                for (int i = 0; i < maxLabelSize - col.ColumnName.Length + 10; i++) Console.Write(" ");
            }

            Console.WriteLine();

            foreach (DataRow dataRow in data.Rows)
            {
                for (int j = 0; j < dataRow.ItemArray.Length; j++)
                {
                    Console.Write(dataRow.ItemArray[j]);
                    for (int i = 0; i < colWidths[data.Columns[j].ColumnName] - dataRow.ItemArray[j].ToString().Length + 10; i++) Console.Write(" ");
                }
                Console.WriteLine();
            }

        }

/*        public bool SearchProfile(string UserName)
        {
            DataTable dt = DB.viewUsers();
            bool found = false;
            string username = "";
            int id = -1;
            string role = "";
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < dr.ItemArray.Length; i += 3)
                {
                    string UN = dr[i].ToString();
                    string ROLE = dr[i + 1].ToString();
                    int ID = int.Parse(dr[i + 2].ToString());
                    if (UN == UserName)
                    {
                        username = UN;
                        role = ROLE;
                        id = ID;
                        found = true;
                        break;
                    }
                }

            }
            return found;
        }*/
        public bool CheckIO(Media media, User user)
        {
            int result = DB.checkIO(media.Media_ID, media.Media_Type, user.Account_ID);
            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool HoldIO(Media media, User user)
        {
            int result = DB.HoldIO(media.Media_ID, user.Account_ID);
            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool updateUser(User user, User olduser)
        {
            int result = 0;
            if(user.Password != null)
            {
                byte[] salty = Salt(user.Password);
                string salt = Convert.ToBase64String(salty);
                string hash = Hash(user.Password, salty);
                result = DB.updateUser((int)olduser.Account_ID, user.Username, hash, user.Role_ID, user.Email, user.FirstName, user.LastName, salt);
            }
            else
            {
                result = DB.updateUser((int)olduser.Account_ID, user.Username, user.Password, user.Role_ID, user.Email, user.FirstName, user.LastName, null);
            }
            if(result == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public List<Media>? patronViewCheckIO(User user)
        {
            DataTable? dt = DB.patronViewCheckIO(user.Account_ID);
            List<Media> mediaList = new List<Media>();
            if (dt != null)
            {

                foreach (DataRow row in dt.Rows)
                {
                    mediaList.Add(new Media
                    {
                        Media_ID = int.Parse(row[0].ToString()),
                        Media_Name = row[1].ToString(),
                        Media_Type = row[2].ToString(),
                        Account_ID = checkNull(row[3]),
                        Author = row[4].ToString(),
                        Publisher = row[5].ToString()
                    });
                  }
                return mediaList;
            }
            return null;
        }

        public List<HoldIO>? patronViewHoldIO(User user)
        {
            DataTable? dt = DB.patronViewHoldIO(user.Account_ID);
            List<HoldIO> mediaList = new List<HoldIO>();
            if (dt != null)
            {

                foreach (DataRow row in dt.Rows)
                {
                    mediaList.Add(new HoldIO
                    {
                        MediaID = int.Parse(row[0].ToString()),
                        MediaName = row[1].ToString(),
                        AccountID = int.Parse(row[2].ToString()),
                        Username = row[3].ToString(),
                        Email = row[4].ToString(),
                    });
                }
                return mediaList;
            }
            return null;
        }

        public List<CheckIO>? ViewCheckIO()
        {
            DataTable? dt = DB.adminViewCheckIO();
            List<CheckIO> mediaList = new List<CheckIO>();
            if (dt != null)
            {

                foreach (DataRow row in dt.Rows)
                {
                    mediaList.Add(new CheckIO
                    {
                        MediaID = int.Parse(row[0].ToString()),
                        MediaName = row[1].ToString(),
                        AccountID = int.Parse(row[2].ToString()),
                        Username = row[3].ToString(),
                        FirstName = row[4].ToString(),
                        LastName = row[5].ToString()
                    });
                }
                return mediaList;
            }
            return null;
        }

        public List<HoldIO>? ViewHoldIO()
        {
            DataTable? dt = DB.adminViewHoldIO();
            List<HoldIO> mediaList = new List<HoldIO>();
            if (dt != null)
            {

                foreach (DataRow row in dt.Rows)
                {
                    mediaList.Add(new HoldIO
                    {
                        MediaID = int.Parse(row[0].ToString()),
                        MediaName = row[1].ToString(),
                        AccountID = int.Parse(row[2].ToString()),
                        Username = row[3].ToString(),
                        Email = row[4].ToString(),
                    });
                }
                return mediaList;
            }
            return null;
        }

        public bool UpdateMedia(Media media, Media update)
        {
            int? result = DB.UpdateMedia(media, update.Media_ID, update.Media_Name, update.Media_Type, update.Author, update.Publisher);

            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        public void UserDelete(int id, string username, string password)
        {
            if (DB.deleteUser(id, username, password))
            {
            }
            else
            {
            }
        }
/*
        public void PrintProfile(string UserName)
        {
            DataTable dt = DB.viewUsers();
            bool found = false;
            string username = "";
            int id = -1;
            string role = "";
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < dr.ItemArray.Length; i += 3)
                {
                    string UN = dr[i].ToString();
                    string ROLE = dr[i + 1].ToString();
                    int ID = int.Parse(dr[i + 2].ToString());
                    if (UN == UserName)
                    {
                        username = UN;
                        role = ROLE;
                        id = ID;
                        found = true;
                        break;
                    }
                }

            }
            if (found)
            {
                Console.WriteLine($"{username}#{id} is a(n) {role}");
            }
            else
            {
                Console.WriteLine($"{UserName} was not found");
            }
        }
*/
        public bool PasswordValidation(string password)
        {
            bool valid = false;
            string[] req = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            string[] reqSpeacial = {"!", "@", "#", "$", "%", "^", "&", "*", "?" };
            
            foreach(string x in req)
            {
                if (password.Contains(x))
                {
                    valid = true;
                }
            }
            if (!valid)
            {
                return valid;
            }
            valid = false;
            foreach(string x in reqSpeacial)
            {
                if (password.Contains(x))
                {
                    valid = true;
                }
            }
            if (!valid)
            {
                return valid;
            }
            if (!password.Any(char.IsUpper))
            {
                return false;
            }

            return valid;
        }

        public User? Login(string? UserName = null, string? Password = null)
        {
            DataTable? dt = null;
            bool email = false;
            User user = new User();
            if(UserName == null || Password == null){
                return null;
            }
            Password = Hash(Password, Convert.FromBase64String(DB.passSalt(UserName)));
            if (UserName.Contains("@"))
            {
                dt = DB.retrieveUser(null, Password, UserName);
                email = true;
            }
            else
            {
                dt = DB.retrieveUser(UserName, Password, null);
            }
           
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    user.Account_ID = int.Parse(row[0].ToString());
                    user.Username = row[1].ToString();
                    user.Password = row[2].ToString();
                    user.Role_ID = int.Parse(row[4].ToString());
                    user.Email = row[5].ToString();
                    user.FirstName = row[6].ToString();
                    user.LastName = row[7].ToString();
                }
                if (email)
                {
                    DB.SetActive(user.Account_ID);
                    user.Active = true;
                    return user;
                }
                if (user.Username == UserName && user.Password == Password)
                {
                    DB.SetActive(user.Account_ID);
                    user.Active = true;
                    return user;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        internal int? checkNull(object x)
        {
            if(x.ToString() == "")
            {
                return null;
            }
            return int.Parse(x.ToString());
        }
        internal byte[] Salt(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetNonZeroBytes(salt);
            }
            return salt;
        }
        internal string Hash(string password, byte[] salt)
        {
            string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256, 100000, 256 / 8));
            return hash;
        }

        public bool addMedia(Media media)
        {
            int result = DB.addMedia(media_id: (int)media.Media_ID,media.Media_Name,media.Media_Type,media.Author,media.Publisher);

            if(result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }



        public Search? Search(Media Media)
        {
            LibraryDB db = new LibraryDB();
            DataTable dt = db.searchMedia(Media.Media_ID, Media.Media_Name, Media.Media_Type, Media.Account_ID, Media.Author, Media.Publisher);
            Search search = new Search();
            search.MediaList = new List<Media>();
            if(dt != null)
            {
             
                foreach (DataRow row in dt.Rows)
                {
                    search.MediaList.Add(new Media {Media_ID = int.Parse(row[0].ToString()),
                                                    Media_Name = row[1].ToString(),
                                                    Media_Type = row[2].ToString(),
                                                    Account_ID = checkNull(row[3]),
                                                    Author = row[4].ToString(),
                                                    Publisher = row[5].ToString()});
                }
                return search;
            }
            return null;
        }

        public bool Register(User user)
        {
            byte[] passwordSalt = Salt(user.Password);
            string passwordHash = Hash(user.Password, passwordSalt);
            if(DB.userCreate(user.Account_ID, user.Username, passwordHash, user.Role_ID, user.Email, user.FirstName, user.LastName,passwordSalt) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public bool DeleteMedia(Media model)
        {
            int wasDeleted = DB.deleteMedia(model);
            if(wasDeleted == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}