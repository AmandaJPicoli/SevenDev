using System;
using System.Collections.Generic;

namespace SevenDev.Domain.Entities
{
    public class TimeLine
    {
        #region Propriedades
        public List<PostagesTimeLine> Postages { get; set; }
        #endregion
    }


    public class PostagesTimeLine
    {
        #region Construtor
        public PostagesTimeLine(string nameUser, string photoUser, string post, string photoPost, DateTime postCreatedAt, int postId)
        {
            NameUser = nameUser;
            PhotoUser = photoUser;
            Post = post;
            PhotoPost = photoPost;
            PostCreatedAt = postCreatedAt;
            IdPost = postId;
        }
        #endregion

        #region Metodos
        public void AddComments(List<CommentsTimeLine> listComments)
        {
            Comments = listComments;
        }
        public void AddLikes(int likes)
        {
            QuantityLikes = likes;
        }
        #endregion

        #region Propriedades
        public int IdPost { get; set; }
        public string NameUser { get; set; }
        public string PhotoUser { get; set; }
        public string Post { get; set; }
        public string? PhotoPost { get; set; }
        public DateTime PostCreatedAt { get; set; }
        public int QuantityLikes { get; set; }
        public List<CommentsTimeLine> Comments { get; set; } = new List<CommentsTimeLine>();
        #endregion

    }

    public class CommentsTimeLine
    {
        #region Construtor
        public CommentsTimeLine(string nameUser, string photoUser, string comment, DateTime commentCreatedAt)
        {
            NameUser = nameUser;
            PhotoUser = photoUser;
            Comment = comment;
            CommentCreatedAt = commentCreatedAt;
        }

        #endregion

        #region Propriedades
        public string NameUser { get; set; }
        public string PhotoUser { get; set; }
        public string Comment { get; set; }
        public DateTime CommentCreatedAt { get; set; }
        #endregion
    }
}
