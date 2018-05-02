using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevicesManager.Models
{
    class LoginModel
    {
        public static int VerifyLogin(string login, string pass)
        {
            if (String.IsNullOrWhiteSpace(login) || String.IsNullOrWhiteSpace(pass))
                return -1;
            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = $"SELECT user_id FROM Users WHERE login='{login}' AND pass='{pass}'"
                };

                var reader = command.ExecuteReader();

                int res = -1;
                if (reader.Read())
                    res = reader.GetInt32(0);
                reader.Close();

                connection.Close();

                return res;
            }
        }
    }
}
