using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace LibraryWebApp
{
    public class LibraryDB
    {
        const string connection = "Server=LAPTOP-325; Database = LibraryDB; Trusted_Connection=true";

        public LibraryDB()
        {


        }

        public int? SetActive(int? account_id)
        {

            using (SqlConnection conn = new SqlConnection(connection))
            {
                int result = -1;
                conn.Open();
                try
                {
                    using (SqlCommand cmd = new SqlCommand($"EXEC [dbo].[setActive] {account_id}", conn))
                    {
                        SqlDataReader dr = cmd.ExecuteReader();
                        dr.Read();
                        result = dr.GetInt32(0);
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
                finally
                {
                    conn.Close();
                }
                return result;
            }

        }

        public DataTable retrieveUser(string UserName, string Password)
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

        public int deleteMedia(Media media)
        {
            int result = -1;
            using (SqlConnection con = new SqlConnection(connection))
            {
                string command = $"DELETE FROM Media WHERE media_id = {media.Media_ID} AND media_name = N'{media.Media_Name}' AND media_type = N'{media.Media_Type}' AND author = N'{media.Author}' AND Publisher = N'{media.Publisher}';";
                string output = $"SELECT N'#Records Affected' = @@ROWCOUNT;";
                con.Open();
                try
                {
                    using (SqlCommand cmd = new SqlCommand($"{command}\n{output}", con))
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
                finally
                {
                    con.Close();

                }
                return result;
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

        public int checkIO(int? media_id, string media_type, int? account_id)
        {
            int result = 0;
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                try
                {
                    using (SqlCommand cmd = new SqlCommand($"EXEC [dbo].[checkIO] {media_id},'{media_type}',{account_id}", con))
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

        public int? UpdateMedia(Media media, int? media_id, string? media_name, string? media_type, string? author, string? publisher)
        {
            int result = -1;
            string command = $"UPDATE Media SET";
            string where = $"WHERE media_id = {media.Media_ID} AND media_name = N'{media.Media_Name}' AND media_type = N'{media.Media_Type}' AND author = N'{media.Author}' AND Publisher = N'{media.Publisher}'";
            string finish = "SELECT N'#Records Affected' = @@ROWCOUNT";
            DataTable dt = new DataTable();
            StringBuilder conditions = new StringBuilder();
            if (media_id != null)
            {
                conditions.Append($"media_id = {media_id}, ");
            }
            if (media_name != null)
            {
                conditions.Append($"media_name = N'{media_name}', ");
            }
            if (media_type != null)
            {
                conditions.Append($"media_type = N'{media_type}', ");
            }
            if (author != null)
            {
                conditions.Append($"author = N'{author}', ");
            }
            if (publisher != null)
            {
                conditions.Append($"Publisher = N'{publisher}', ");
            }
            if (conditions.Length > 0)
            {
                conditions.Remove(conditions.Length - 2, 2);
            }
            if (conditions.Length == 0)
            {
                return null;
            }
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                try
                {
                    using (SqlCommand cmd = new SqlCommand($"{command} {conditions.ToString()} {where} {finish}", con))
                    {
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            result = dr.GetInt32(0);
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
                finally
                {
                    con.Close();
                }
            }
            return result;
        }

        public int addMedia(int media_id, string media_name, string media_type, string author, string publisher,int? account_id = null)
        {
            string command = $"INSERT INTO Media (media_id,media_name,media_type,author,publisher)";
            string values = $"VALUES ({media_id},N'{media_name}',N'{media_type}',N'{author}',N'{publisher}')";
            DataTable dt = new DataTable();
            int result = -1;
            using(SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                try
                {
                    using(SqlCommand cmd = new SqlCommand($"{command}\n{values} SELECT N'#RecordsAffected' = @@ROWCOUNT", con))
                    {
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            result = dr.GetInt32(0);
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
                finally
                {
                    con.Close();
                }
            }
            return result;
        }

        public DataTable? patronViewCheckIO(int? accountID)
        {
            DataTable dt = new DataTable();
            using(SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                try
                {
                    using(SqlCommand cmd = new SqlCommand($"SELECT * FROM Media WHERE account_id = {accountID}",con))
                    {
                        SqlDataAdapter data = new SqlDataAdapter(cmd);
                        data.Fill(dt);
                        data.Dispose();
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
            return dt;
        }

        public DataTable? adminViewCheckIO()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                try
                {
                    using(SqlCommand cmd = new SqlCommand($"SELECT * FROM [AdminCheckOutView]",con))
                    {
                        SqlDataAdapter data = new SqlDataAdapter(cmd);
                        data.Fill(dt);
                        data.Dispose();
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
            return dt;
        }



        public DataTable? searchMedia(int? media_id = null, string? media_name = null, string? media_type = null, int? account_id = null, string? author = null, string? publisher = null)
        {
            string command = $"SELECT * FROM Media WHERE";
            DataTable results = new DataTable();
            StringBuilder conditions = new StringBuilder();
            if (media_id != null)
            {
                conditions.Append($"media_id LIKE %{media_id}% OR ");
            }
            if (media_name != null)
            {
                conditions.Append($"media_name LIKE N'%{media_name}%' OR ");
            }
            if (media_type != null)
            {
                conditions.Append($"media_type LIKE N'%{media_type}%' OR ");
            }
            if (account_id != null)
            {
                conditions.Append($"account_id LIKE %{account_id}% OR ");
            }
            if (author != null)
            {
                conditions.Append($"author LIKE N'%{author}%' OR ");
            }
            if (publisher != null)
            {
                conditions.Append($"Publisher LIKE N'%{publisher}%' OR ");
            }
            if (conditions.Length > 0)
            {
                conditions.Remove(conditions.Length - 4, 4);
            }
            if (conditions.Length == 0)
            {
                command = $"SELECT * FROM Media";
            }
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                try
                {
                    using (SqlCommand cmd = new SqlCommand($"{command} {conditions.ToString()}", con))
                    {
                        SqlDataAdapter data = new SqlDataAdapter(cmd);
                        data.Fill(results);
                        data.Dispose();
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
            return results;
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

        public int? userCreate(int? account_id, string username, string password, int? role_id, string email, string first_name, string last_name)
        {
            if (username == null || password == null || first_name == null || last_name == null || email == null)
            {
                return null;
            }
            int wow = 0;
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                try
                {
                    using (SqlCommand cmd = new SqlCommand($"EXEC [dbo].[userCreate] {account_id},'{username}','{password}',{role_id},'{email}','{first_name}','{last_name}'", con))
                    {
                        SqlDataReader dr = cmd.ExecuteReader();
                        dr.Read();
                        wow = dr.GetInt32(0);
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
            return wow;
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