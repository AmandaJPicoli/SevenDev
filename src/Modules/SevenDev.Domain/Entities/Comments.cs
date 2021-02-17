using System;

namespace SevenDev.Domain.Entities
{
    public class Comments
    {
        #region Construtor
        public Comments(int postageId,
                       int userId,
                       string text)
        {
            PostageId = postageId;
            UserId = userId;
            Text = text;

            Created = DateTime.Now;
        }

        public Comments(int id,
                         int postageId,
                         int userId,
                         string text,
                         DateTime created)
        {
            Id = id;
            PostageId = postageId;
            UserId = userId;
            Text = text;
            Created = created;
        }
        #endregion

        #region Propriedades
        public int Id { get; private set; }
        public int PostageId { get; private set; }
        public int UserId { get; private set; }
        public string Text { get; private set; }
        public DateTime Created { get; private set; }

        #endregion

        #region Metodos
        public void SetId(int id)
        {
            Id = id;
        }
        #endregion

    }
}
