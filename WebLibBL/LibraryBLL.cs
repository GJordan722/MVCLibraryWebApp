using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public void PrintUsers()
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

        public bool SearchProfile(string UserName)
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
        }

        //FIX ACTIVE
        public void CheckIO(int media_id, string type,int accountID)
        {
            if (DB.checkIO(media_id, type, accountID) == 1)
            {
                Console.WriteLine($"Checked out.");
            }
            else
            {
                Console.WriteLine($"Checked in.");
            }
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

        public void ChangeRole(string username, string password, int roleid)
        {
            DataTable? dt = DB.retrieveUser(username, password);
            int id = -1;
            foreach (DataRow dr in dt.Rows)
            {
                id = int.Parse(dr[0].ToString());
            }
            if (dt != null)
            {
                Console.WriteLine("Role Changed");
                int role_id = DB.updateUser(id, username, roleid);
            }
            else
            {
                Console.WriteLine($"{username} not found");
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

  
        public string GetHiddenConsoleInput()
        {
            StringBuilder input = new StringBuilder();
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter) break;
                if (key.Key == ConsoleKey.Backspace && input.Length > 0) input.Remove(input.Length - 1, 1);
                else if (key.Key != ConsoleKey.Backspace) input.Append(key.KeyChar);
            }
            return input.ToString();
        }


        public User? Login(string UserName = "", string Password = "")
        {
            User user = new User();
            if(UserName == null || Password == null){
                return null;
            }
            DataTable? dt = DB.retrieveUser(UserName, Password);
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

        public Search Search(Media Media)
        {
            LibraryDB db = new LibraryDB();
            DataTable dt = db.searchMedia(Media.Media_ID, Media.Media_Name, Media.Media_Type, Media.Account_ID, Media.Author, Media.Publisher);
            Search search = new Search();
            search.MediaList = new List<Media>();
            Media mediaTemp = new Media();
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
            if(DB.userCreate(user.Account_ID, user.Username, user.Password, user.Role_ID, user.Email, user.FirstName, user.LastName) != null)
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