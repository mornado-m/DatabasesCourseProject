using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevicesManager.Models
{
    class DevicesModel
    {
        public DevicesModel(int userId, int permissionLevel)
        {
            UserId = userId;
            PermissionLevel = permissionLevel;
        }

        public int UserId { get; private set; }

        public int PermissionLevel { get; private set; }

        public DataTable GetDevicesTable()
        {
            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = $"SELECT * FROM GetAllDevices({UserId})"
                };
                
                var res = new DataTable();
                var da = new SqlDataAdapter(command);
                da.Fill(res);

                connection.Close();
                da.Dispose();
                
                return res;
            }
        }
    }
}
