using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevicesManager.Models
{
    class SessionModel
    {
        public SessionModel(int userId, string userLogin)
        {
            UserId = userId;
            UserLogin = userLogin;
            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = $"SELECT r.permission_level FROM User_Roles ur JOIN Roles r ON ur.role_id = r.role_id AND ur.user_id = {userId}"
                };

                var reader = command.ExecuteReader();

                if (reader.Read())
                    PermissionLevel = reader.GetInt32(0);
                reader.Close();

                connection.Close();
            }
        }

        public int UserId { get; private set; }
        public string UserLogin { get; private set; }
        public int PermissionLevel { get; private set; }
    }
}
