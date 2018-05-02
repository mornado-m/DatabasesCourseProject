using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevicesManager.Models
{
    class TransfersModel
    {
        public TransfersModel(int userId, int permissionLevel)
        {
            UserId = userId;
            PermissionLevel = permissionLevel;
        }

        public int UserId { get; private set; }

        public int PermissionLevel { get; private set; }

        public DataTable GetTransfersTable()
        {
            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = $"SELECT * FROM GetAllTransfers({UserId})"
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
