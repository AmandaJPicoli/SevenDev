using SevenDev.Domain.Entities;
using SevenDev.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SevenDev.Repositories
{
    public class GenderRepository : IGenderRepository
    {
        private readonly IConfiguration _configuration;

        public GenderRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Gender> GetByIdAsync(int id)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = @$"SELECT 
                                     Id,
	                                 Descricao
                                FROM 
	                                Genero
                                WHERE 
	                                Id={id}";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                    while (reader.Read())
                    {
                        var gender = new Gender(reader["Descricao"].ToString());
                        gender.SetId(int.Parse(reader["Id"].ToString()));

                        return gender;
                    }

                    return default;
                }
            }
        }
    }
}
