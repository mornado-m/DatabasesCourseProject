using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevicesManager.Models
{
    class AddNewTransferModel
    {
        public AddNewTransferModel(int userId, int permissionLevel)
        {
            UserId = userId;
            PermissionLevel = permissionLevel;
        }

        public int UserId { get; private set; }

        public int PermissionLevel { get; private set; }

        public Dictionary<string, int> GetDevices()
        {
            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = $"SELECT * FROM GetAllDevices({UserId})"
                };

                var table = new DataTable();
                var da = new SqlDataAdapter(command);
                da.Fill(table);

                connection.Close();
                da.Dispose();

                var res = new Dictionary<string, int>();
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    res.Add($"{table.Rows[i][1]} {(PermissionLevel > 2 ? table.Rows[i][3] : "")} {table.Rows[i][4]}",
                        (int) table.Rows[i][0]);
                }

                return res;
            }
        }

        public Dictionary<string, int> GetDepartments()
        {
            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = $"SELECT department_id, name FROM Departments"
                };

                var table = new DataTable();
                var da = new SqlDataAdapter(command);
                da.Fill(table);

                connection.Close();
                da.Dispose();

                var res = new Dictionary<string, int>();
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    res.Add($"{table.Rows[i][1]}", (int)table.Rows[i][0]);
                }

                return res;
            }
        }

        public Dictionary<string, TransfersTypes> GetTransfersTypes()
        {
            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = $"SELECT transfers_type_id, name FROM Transfers_Types"
                };

                var table = new DataTable();
                var da = new SqlDataAdapter(command);
                da.Fill(table);

                connection.Close();
                da.Dispose();

                var res = new Dictionary<string, TransfersTypes>();
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    res.Add($"{table.Rows[i][1]}", (TransfersTypes)((int)table.Rows[i][0]));
                }

                return res;
            }
        }

        public Dictionary<string, TransfersTypes> GetTransfersTypes(int deviceId)
        {
            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = $"SELECT transfers_type_id, name FROM Transfers_Types"
                };

                var table = new DataTable();
                var da = new SqlDataAdapter(command);
                da.Fill(table);
                da.Dispose();
                
                var types = new Dictionary<TransfersTypes, string>();
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    types.Add((TransfersTypes)((int)table.Rows[i][0]), $"{table.Rows[i][1]}");
                }
                types.Remove(TransfersTypes.Buy);
                types.Remove(TransfersTypes.ChangeAttribute);
                
                command.CommandText = $"SELECT devices_status_id FROM Devices WHERE device_id={deviceId}";
                table = new DataTable();
                da = new SqlDataAdapter(command);
                da.Fill(table);

                connection.Close();
                da.Dispose();

                var status = (int) table.Rows[0][0];
                switch (status)
                {
                    case 4:
                    case 1:
                        types.Remove(TransfersTypes.MoveToRestore);
                        types.Remove(TransfersTypes.MoveFromRestore);
                        break;
                    case 2:
                        types.Remove(TransfersTypes.MoveFromRestore);
                        
                        break;
                    case 3:
                        types.Remove(TransfersTypes.Move);
                        types.Remove(TransfersTypes.MoveToRestore);
                        types.Remove(TransfersTypes.Sale);
                        types.Remove(TransfersTypes.Remove);
                        break;
                    case 5:
                    case 6:
                        types.Remove(TransfersTypes.Move);
                        types.Remove(TransfersTypes.MoveToRestore);
                        types.Remove(TransfersTypes.MoveFromRestore);
                        types.Remove(TransfersTypes.Sale);
                        types.Remove(TransfersTypes.Remove);
                        break;
                }

                var res = new Dictionary<string, TransfersTypes>();
                foreach (var type in types)
                    res.Add(type.Value, type.Key);

                return res;
            }
        }

        public void AddTransfer(TransfersTypes type, int deviceId, int departmentId, DateTime date, decimal cost, string description)
        {
            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                {
                    Connection = connection
                };

                switch (type)
                {
                    case TransfersTypes.Move:
                        command.CommandText = $"EXEC MoveDevice {deviceId}, {departmentId}, '{date.Year}-{date.Month}-{date.Day}', '{description}', {UserId}";
                        break;
                    case TransfersTypes.MoveToRestore:
                        command.CommandText = $"EXEC MoveToRestoreDevice {deviceId}, {cost}, '{date.Year}-{date.Month}-{date.Day}', '{description}', {UserId}";
                        break;
                    case TransfersTypes.MoveFromRestore:
                        command.CommandText = $"EXEC MoveFromRestoreDevice {deviceId}, {cost}, '{description}', {UserId}";
                        break;
                    case TransfersTypes.Sale:
                        command.CommandText = $"EXEC SaleDevice {deviceId}, {cost}, '{date.Year}-{date.Month}-{date.Day}', '{description}', {UserId}";
                        break;
                    case TransfersTypes.Remove:
                        command.CommandText = $"EXEC RemoveDevice {deviceId}, '{date.Year}-{date.Month}-{date.Day}', '{description}', {UserId}";
                        break;
                }
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public enum TransfersTypes : int
        {
            Buy = 1,
            Move = 2,
            MoveToRestore = 3,
            MoveFromRestore = 4,
            Sale = 5,
            Remove = 6,
            ChangeAttribute = 7
        }
    }
}
