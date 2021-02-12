using SevenDev.Domain.Entities;
using SevenDev.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace SevenDev.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IConfiguration _configuration;

        public CommentRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<Comments>> GetByPostageIdAsync(int postageId)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = @$"SELECT Id,
	                                   UsuarioId,
                                       PostagemId,
                                       Texto,
                                       Criacao
                                FROM 
	                                Comentario
                                WHERE 
	                                PostagemId= '{postageId}'";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                    var commentsForPostage = new List<Comments>();

                    while (reader.Read())
                    {
                        var comment = new Comments(int.Parse(reader["Id"].ToString()),
                                                    int.Parse(reader["PostagemId"].ToString()),
                                                    int.Parse(reader["UsuarioId"].ToString()),
                                                    reader["Texto"].ToString(),
                                                    DateTime.Parse(reader["Criacao"].ToString()));

                        commentsForPostage.Add(comment);
                    }

                    return commentsForPostage;
                }
            }
        }

        public async Task<int> InsertAsync(Comments comment)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = @"INSERT INTO
                                Comentario (UsuarioId,
                                             PostagemId,
                                             Texto,
                                             Criacao)
                                VALUES (@usuarioId,
                                        @postagemId,
                                        @texto,
                                        @criacao); SELECT scope_identity();";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("usuarioId", comment.UserId);
                    cmd.Parameters.AddWithValue("postagemId", comment.PostageId);
                    cmd.Parameters.AddWithValue("texto", comment.Text);
                    cmd.Parameters.AddWithValue("criacao", comment.Created);

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

