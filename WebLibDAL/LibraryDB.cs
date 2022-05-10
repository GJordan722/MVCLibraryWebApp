using System.Data.SqlClient;
using System.Data;

namespace LibraryWebApp
{
    public class LibraryDB
    {
        const string connection = "Server=DESKTOP-CIQ2956\\SQLEXPRESS; Database = LibraryDB; Trusted_Connection=true";

        //Username,<Password,Role>
        private Dictionary<string, string[]> _Profiles = new Dictionary<string, string[]>()
        {
            {"Admin123", new string[] {"password123","Admin"}},
            {"LibKate", new string[] { "katelibrarian", "Librarian" }}
        };
        //                      {username,password,role,account_id}


        public LibraryDB()
        {


        }

        public Dictionary<string, string[]> Profiles
        {
            get { return _Profiles; }
        }

        public string[]? Profile(string Username)
        {
            if (!_Profiles.ContainsKey(Username))
            {
                Console.WriteLine("Username is invalid");
                return null;
            }
            return _Profiles[Username];
        }

        public int SetActive(int account_id)
        {
            
            using(SqlConnection conn = new SqlConnection(connection))
            {
                int result = -1;
                conn.Open();
                try
                {
                    using(SqlCommand cmd = new SqlCommand($"EXEC [dbo].[setActive] {account_id}", conn))
                    {
                        SqlDataReader dr = cmd.ExecuteReader();
                        dr.Read();
                        result = dr.GetInt32(0);
                        dr.Close();
                    }
                }
                catch(SqlException ex)
                {
                    int errorID = ex.ErrorCode;
                    string? stack = ex.StackTrace;
                    string message = ex.Message;
                    string source = ex.Source;
                    DateTime date = DateTime.Now;
                    ExceptionLog(errorID, stack, message, source, date);
                }
                finally
                {
                    conn.Close();
                }
                return result;
            }
            
        }

        public DataTable CheckLogin(string UserName, string Password)
        {
            DataTable results = new DataTable();
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                try
                {
                    using (SqlCommand cmd = new SqlCommand($"EXEC [dbo].[checkUserValid] {UserName},{Password}", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(results);
                        da.Dispose();

                    }
                }
                catch (SqlException ex)
                {
                    int errorID = ex.ErrorCode;
                    string? stack = ex.StackTrace;
                    string message = ex.Message;
                    string source = ex.Source;
                    DateTime date = DateTime.Now;
                    ExceptionLog(errorID, stack, message, source, date);
                }
                con.Close();
            }
            return results;
        }

        public int updateUser(int account_id, string username, int role_id)
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                int newRole_id = -1;
                con.Open();
                try
                {
                    using (SqlCommand cmd = new SqlCommand($"EXEC [dbo].[updateUser] {account_id},{username},{role_id}", con))
                    {
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            newRole_id = dr.GetInt32(0);
                        }
                        dr.Close();
                     
                    }
                }
                catch (SqlException ex)
                {
                    int errorID = ex.ErrorCode;
                    string? stack = ex.StackTrace;
                    string message = ex.Message;
                    string source = ex.Source;
                    DateTime date = DateTime.Now;
                    ExceptionLog(errorID, stack, message, source, date);
                }
                con.Close();
                return newRole_id;
            }

        }

        public bool deleteUser(int account_id, string username, string password)
        {
            bool ToF = false;
            using (SqlConnection con = new SqlConnection(connection))
            {
                int result = 0;
                con.Open();
                try
                {
                    using (SqlCommand cmd = new SqlCommand($"EXEC [dbo].[deleteUser] {account_id},{username},{password}", con))
                    {
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            result = dr.GetInt32(0);
                        }
                        dr.Close();
                        if (result == 1)
                        {
                            ToF = true;
                        }
                        else
                        {
                            ToF = false;
                        }
                    }
                }
                catch (SqlException ex)
                {
                    int errorID = ex.ErrorCode;
                    string? stack = ex.StackTrace;
                    string message = ex.Message;
                    string source = ex.Source;
                    DateTime date = DateTime.Now;
                    ExceptionLog(errorID, stack, message, source, date);
                }
                con.Close();
            }
            return ToF;
        }

        public int updateMedia(int media_id, string media_type, int account_id)
        {
            int result = 0;
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                try
                {
                    using (SqlCommand cmd = new SqlCommand($"EXEC [dbo].[updateMedia] {media_id},{media_type},{account_id}", con))
                    {
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            result = dr.GetInt32(0);
                        }
                        dr.Close();
                    }
                }
                catch (SqlException ex)
                {
                    int errorID = ex.ErrorCode;
                    string? stack = ex.StackTrace;
                    string message = ex.Message;
                    string source = ex.Source;
                    DateTime date = DateTime.Now;
                    ExceptionLog(errorID, stack, message, source, date);
                }
                con.Close();
            }
            return result;
        }

        public DataTable viewMedia()
        {
            DataTable results = new DataTable();
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                try
                {
                    using (SqlCommand cmd = new SqlCommand($"SELECT * FROM Media", con))
                    {

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(results);
                        da.Dispose();

                    }
                }
                catch (SqlException ex)
                {
                    int errorID = ex.ErrorCode;
                    string? stack = ex.StackTrace;
                    string message = ex.Message;
                    string source = ex.Source;
                    DateTime date = DateTime.Now;
                    ExceptionLog(errorID, stack, message, source, date);
                }
                con.Close();
            }
            return results;
        }

        public void userCreate(int account_id, string username, string password, int role_id,string email,string first_name,string last_name)
        {
            bool finish = false;
            using (SqlConnection con = new SqlConnection(connection))
            {
                int result = 0;
                con.Open();
                try
                {
                    using (SqlCommand cmd = new SqlCommand($"EXEC [dbo].[userCreate] {account_id},{username},{password},{role_id},{email},{first_name},{last_name}", con))
                    {
                        SqlDataReader dr = cmd.ExecuteReader();
                        dr.Read();
                        int wow = dr.GetInt32(0);
                    }
                }
                catch (SqlException ex)
                {
                    int errorID = ex.ErrorCode;
                    string? stack = ex.StackTrace;
                    string message = ex.Message;
                    string source = ex.Source;
                    DateTime date = DateTime.Now;
                    ExceptionLog(errorID, stack, message, source, date);
                }
                finally
                {
                    con.Close();
                }
            }
            
        }

        public void ExceptionLog(int id, string stackT, string message, string source, DateTime date)
        {
            int result = -1;
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand($"EXEC [dbo].[addExceptionLog] {id},{stackT},{message},{source},{date}", con))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        result = dr.GetInt32(0);
                    }
                    dr.Close();
                }
            }
        }

        public DataTable viewUsers()
        {
            DataTable results = new DataTable();
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                try
                {
                    using (SqlCommand cmd = new SqlCommand($"EXEC [dbo].[viewUsers]", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(results);
                        da.Dispose();
                        return results;
                    }
                }
                catch (SqlException ex)
                {
                    int errorID = ex.ErrorCode;
                    string? stack = ex.StackTrace;
                    string message = ex.Message;
                    string source = ex.Source;
                    DateTime date = DateTime.Now;
                    ExceptionLog(errorID, stack, message, source, date);
                }
                con.Close();
            }
            return results;
        }

        public DataTable viewRoles()
        {
            DataTable results = new DataTable();
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                try
                {
                    using (SqlCommand cmd = new SqlCommand($"EXEC [dbo].[viewRoles]", con))
                    {
                        
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(results);
                        da.Dispose();
                        
                    }
                }
                catch (SqlException ex)
                {
                    int errorID = ex.ErrorCode;
                    string? stack = ex.StackTrace;
                    string message = ex.Message;
                    string source = ex.Source;
                    DateTime date = DateTime.Now;
                    ExceptionLog(errorID, stack, message, source, date);
                }
                con.Close();
            }
            return results;
        }
    }
}