using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevicesManager.Models
{
    class AttributesModel
    {
        public AttributesModel(int userId, int permissionLevel)
        {
            UserId = userId;
            PermissionLevel = permissionLevel;
        }

        public int UserId { get; private set; }

        public int PermissionLevel { get; private set; }

        public DataTable GetAttributesTable()
        {
            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = $"SELECT * FROM GetAllDevicesAttributes({UserId})"
                };

                var res = new DataTable();
                var da = new SqlDataAdapter(command);
                da.Fill(res);

                connection.Close();
                da.Dispose();
                
                return res;
            }
        }

        public DataTable GetAttributesTable(int deviceId)
        {
            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = $"SELECT * FROM GetDeviceAttribute({deviceId})"
                };

                var res = new DataTable();
                var da = new SqlDataAdapter(command);
                da.Fill(res);

                connection.Close();
                da.Dispose();

                return res;
            }
        }

        public int GetDeviceStatus(int deviceId)
        {
            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = $"SELECT devices_status_id FROM Devices WHERE device_id={deviceId}"
                };

                var res = new DataTable();
                var da = new SqlDataAdapter(command);
                da.Fill(res);

                connection.Close();
                da.Dispose();

                return (int) res.Rows[0][0];
            }
        }

        public void SetDeviceIsBroken(int deviceId)
        {
            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = $"EXEC SetDeviceIsBroken {deviceId}"
                };
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void SetDeviceCannotRestore(int deviceId)
        {
            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = $"EXEC SetDeviceCannotRestore {deviceId}"
                };
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
