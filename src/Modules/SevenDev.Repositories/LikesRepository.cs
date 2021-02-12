using SevenDev.Domain.Entities;
using SevenDev.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SevenDev.Repositories
{
    public class LikesRepository : ILikesRepository
    {
        private readonly IConfiguration _configuration;

        public LikesRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task DeleteAsync(int id)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = $@"DELETE 
                                FROM
                                Curtidas
                               WHERE 
                                Id={id}";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                     await cmd
                            .ExecuteScalarAsync()
                            .ConfigureAwait(false);
                }
            }
        }

        public async Task<Likes> GetByUserIdAndPostageIdAsync(int userId, int postageId)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = @$"SELECT Id,
	                                   UsuarioId,
                                       PostagemId
                                FROM 
	                                Curtidas
                                WHERE 
	                                UsuarioId= '{userId}'
                                AND 
                                    PostagemId= '{postageId}'";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                    while (reader.Read())
                    {
                        var like = new Likes(int.Parse(reader["Id"].ToString()),
                                                int.Parse(reader["PostagemId"].ToString()),
                                                int.Parse(reader["UsuarioId"].ToString()));

                        return like;
                    }

                    return default;
                }
            }
        }

        public async Task<int> GetQuantityOfLikesByPostageIdAsync(int postageId)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = @$"SELECT
                                    COUNT(*) AS Quantidade
                                FROM 
	                                Curtidas
                                WHERE 
	                                PostagemId={postageId}";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                    while (reader.Read())
                    {
                        return int.Parse(reader["Quantidade"].ToString());
                    }

                    return default;
                }
            }
        }

        public async Task<int> InsertAsync(Likes likes)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = @"INSERT INTO
                                Curtidas (UsuarioId,
                                           PostagemId)
                                VALUES (@usuarioId,
                                        @postagemId); SELECT scope_identity();";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("usuarioId", likes.UserId);
                    cmd.Parameters.AddWithValue("postagemId", likes.PostageId);

                    con.Open();
                    var id = await cmd
                                    .ExecuteScalarAsync()
                                    .ConfigureAwait(false);

                    return int.Parse(id.ToString());
                }
            }
        }
    }
}
