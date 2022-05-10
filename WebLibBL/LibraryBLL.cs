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
        public void CheckIO(int media_id, string type)
        {
            // int accountID = int.Parse(DB.Active[3]);
            int accountID = 1;
            if (DB.updateMedia(media_id, type, accountID) == 1)
            {
                Console.WriteLine($"Checked out.");
            }
            else
            {
                Console.WriteLine($"Checked in.");
            }
        }
        public void ChangeRole(string username, string password, int roleid)
        {
            DataTable? dt = DB.CheckLogin(username, password);
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
                Console.WriteLine($"{username} has been deleted!");
            }
            else
            {
                Console.WriteLine($"{username} not found");
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

        //FIX ACTIVE
        public bool CheckLoggedin()
        {
            if (false)
            {
                return true;
            }
            else
            {
                Console.WriteLine($"Already logged in as ");
                return false;
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
            if (UserName == "" && Password == "")
            {
                Console.WriteLine("Logged in as guest");
                return null;
            }
            DataTable dt = DB.CheckLogin(UserName, Password);
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
                    Console.WriteLine("Username or password invalid.");
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public int Register(User user)
        {
            int role_id = 3;
            Random rand = new Random();
            int id = rand.Next();
            DB.userCreate(id, user.Username, user.Password, role_id, user.Email, user.FirstName, user.LastName);
            return id;
        }
        
        //FIX ACTIVE
    /*    public string[]? Logout()
        {
            if (DB.Active == null)
            {
                Console.WriteLine("No one currently logged in.");
            }
            else
            {
                Console.WriteLine($"Goodbye {DB.Active[0]}");
                DB.Active = null;
            }
            return DB.Active;
        }*/

        public void TestCheck(string username, string password)
{

}
public string[] testRegister(string user, string password, string role)
{
    if (DB.Profiles.ContainsKey(user))
    {
        return new string[] { "", "", "" };
    }
    if (role.ToUpper() == "P" || role.ToUpper() == "L" || role.ToUpper() == "A")
    {
        switch (role.ToUpper())
        {
            case "P":
                role = "Patron";
                break;
            case "L":
                role = "Librarian";
                break;
            case "A":
                role = "Admin";
                break;
        }
        return new string[] { user, password, role };
    }
    else
    {
        return new string[] { user, password, "" };
    }
}
    }
}