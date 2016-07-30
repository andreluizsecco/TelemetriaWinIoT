using System;
using System.Data;
using System.Data.SqlClient;

namespace SensorRealTime.DAL
{
    public class CustomContext
    {
        static string _connString = @"SUA_CONNECTION_STRING";
        static string _selectQuery = @"SELECT TOP(1) Temperatura, Umidade FROM DHT11 ORDER BY Data DESC";

        public static decimal Temperatura { get; set; }
        public static decimal Umidade { get; set; }

        public static void UpdateSensorValues()
        {
            using (var connection = new SqlConnection(_connString))
            {
                using (SqlCommand command = new SqlCommand(_selectQuery, connection))
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Temperatura = Convert.ToDecimal(reader.GetValue(0));
                            Umidade = Convert.ToDecimal(reader.GetValue(1));
                        }
                    }
                }
            }
        }
    }
}