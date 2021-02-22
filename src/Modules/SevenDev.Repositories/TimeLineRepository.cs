using Microsoft.Extensions.Configuration;
using SevenDev.Domain.Entities;
using SevenDev.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace SevenDev.Repositories
{
    public class TimeLineRepository : ITimeLineRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILikesRepository _likesRepository;

        public TimeLineRepository(IConfiguration configuration,
            ILikesRepository likesRepository)
        {
            _configuration = configuration;
            _likesRepository = likesRepository;
        }

        public async Task<List<PostagesTimeLine>> GetTimeLine(int userIdLogado)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = @$"select  u0.nome, u0.foto as Perfil, 
                                      p.Id, p.Texto, p.Foto, p.Criacao as dataPost
                                from Convite co
                                inner join Usuario u0 on (co.UsuarioIdRecebeu = u0.Id and AceitouConvite = 1)
                                or (co.UsuarioIdConvidou = u0.Id and AceitouConvite = 1)
                                inner join Usuario u1 on (co.UsuarioIdConvidou = u1.Id and AceitouConvite = 1)
                                or (co.UsuarioIdRecebeu = u1.Id and AceitouConvite = 1)
                                inner join Postagem p on p.UsuarioId = u0.Id 
                                where u1.Id = {userIdLogado}
                                order by p.Criacao desc";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                    var postageTimeLine = new List<PostagesTimeLine>();

                    while (reader.Read())
                    {
                        var postage = new PostagesTimeLine(reader["Nome"].ToString(),
                                                    reader["Perfil"].ToString(),
                                                    reader["Texto"].ToString(),
                                                    reader["Foto"].ToString(),
                                                    DateTime.Parse(reader["dataPost"].ToString()),
                                                    int.Parse(reader["Id"].ToString()));

                        postageTimeLine.Add(postage);
                    }
                 return postageTimeLine;
                }
            }
        }

        public async Task<List<CommentsTimeLine>> GetCommentsAsync(int postId)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = @$"select u2.Nome , u2.Foto ,
                                                cm.Texto , cm.Criacao as Data
                                            from Comentario cm
                                            inner join Usuario u2 on u2.Id = cm.UsuarioId
                                            where cm.PostagemId = {postId}";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                    var commentsPost = new List<CommentsTimeLine>();

                    while (reader.Read())
                    {
                        var comments = new CommentsTimeLine(reader["Nome"].ToString(),
                                                    reader["Foto"].ToString(),
                                                    reader["Texto"].ToString(),
                                                    DateTime.Parse(reader["Data"].ToString()));

                        commentsPost.Add(comments);
                    }
                    return commentsPost;
                }
            }
        }
    }
}
