using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevicesManager.Models
{
    class AddNewDeviceModel
    {
        public AddNewDeviceModel(int userId, int permissionLevel)
        {
            UserId = userId;
            PermissionLevel = permissionLevel;
            UserDepartmentId = GetUserDepartmentId();
        }

        private int GetUserDepartmentId()
        {
            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = $"SELECT department_id FROM Employees WHERE user_id={UserId}"
                };

                var table = new DataTable();
                var da = new SqlDataAdapter(command);
                da.Fill(table);

                connection.Close();
                da.Dispose();
                
                return (int) table.Rows[0][0];
            }
        }

        public int UserId { get; private set; }

        public int PermissionLevel { get; private set; }

        public int UserDepartmentId { get; private set; }
        
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

        public Dictionary<string, int> GetDevicesTypes()
        {
            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = $"SELECT device_type_id, name FROM Device_Types"
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

        public Tuple<Dictionary<string, int>, Dictionary<string, string>> GetAttributesTypes()
        {
            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = $"SELECT attributes_type_id, name, units_of_measurement FROM Attributes_Types"
                };

                var table = new DataTable();
                var da = new SqlDataAdapter(command);
                da.Fill(table);

                connection.Close();
                da.Dispose();

                var types = new Dictionary<string, int>();
                var measurements = new Dictionary<string, string>();
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    types.Add($"{table.Rows[i][1]}", (int)table.Rows[i][0]);
                    measurements.Add($"{table.Rows[i][1]}", (string)table.Rows[i][2]);
                }

                return new Tuple<Dictionary<string, int>, Dictionary<string, string>>(types, measurements);
            }
        }

        public Tuple<Dictionary<string, int>, Dictionary<string, string>> GetAttributesTypes(int deviceTypeId)
        {
            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = $"SELECT attributes_type_id, name, units_of_measurement FROM Attributes_Types" +
                                  $" WHERE attributes_type_id IN " +
                                  $"(SELECT attributes_type_id FROM Device_Type_Attributes WHERE device_type_id={deviceTypeId})"
                };

                var table = new DataTable();
                var da = new SqlDataAdapter(command);
                da.Fill(table);

                connection.Close();
                da.Dispose();

                var types = new Dictionary<string, int>();
                var measurements = new Dictionary<string, string>();
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    types.Add($"{table.Rows[i][1]}", (int)table.Rows[i][0]);
                    measurements.Add($"{table.Rows[i][1]}", (string)table.Rows[i][2]);
                }

                return new Tuple<Dictionary<string, int>, Dictionary<string, string>>(types, measurements);
            }
        }

        public void AddDevice(int deviceTypeId, int departmentId, decimal devCost, int serialNum, DateTime prodDate,
            string description, decimal tranCost, DateTime tranDate, Dictionary<int, string> attrs)
        {
            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = $"DECLARE @attrs DeviceAttributesList;{Environment.NewLine}" +
                                  $"INSERT INTO @attrs (attribute_type_id, val) VALUES "
                };

                foreach (var attr in attrs)
                    command.CommandText += $"({attr.Key}, '{attr.Value}'),";
                command.CommandText = command.CommandText.Remove(command.CommandText.Length - 1) +
                                      $";{Environment.NewLine}" +
                                      $"EXEC AddNewDevice {deviceTypeId}, {departmentId}, {devCost}, {serialNum}, " +
                                      $"'{prodDate.Year}-{prodDate.Month}-{prodDate.Day}', '{description}', {tranCost}, " +
                                      $"'{tranDate.Year}-{tranDate.Month}-{tranDate.Day}', {UserId}, @attrs";
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
