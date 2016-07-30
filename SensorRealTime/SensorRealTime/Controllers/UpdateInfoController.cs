using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;

namespace SensorRealTime.Controllers
{
    public class UpdateInfoController : ApiController
    {
        [HttpGet]
        public void Get(Guid id, decimal temperatura, decimal umidade)
        {
            if (id == Guid.Parse("SEU_TOKEN_AQUI"))
            {
                string connString = @"SUA_CONNECTION_STRING";
                string command = @"INSERT INTO DHT11(Temperatura, Umidade) VALUES (@Temperatura, @Umidade)";

                using (var connection = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand(command, connection))
                    {
                        if (connection.State == ConnectionState.Closed)
                            connection.Open();

                        cmd.Parameters.Add(new SqlParameter("@Temperatura", temperatura));
                        cmd.Parameters.Add(new SqlParameter("@Umidade", umidade));
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}