using System;

namespace SevenDev.Domain.Entities
{
    public class Postage
    {
        #region Construtor
        public Postage(string text,
                      int userId)
        {
            Text = text;
            UserId = userId;

            Created = DateTime.Now;
        }

        public Postage(int id,
                        string text,
                        string foto,
                        int userId,
                        DateTime created)
        {
            Id = id;
            Text = text;
            UserId = userId;
            Created = created;
            Foto = foto;
        }
        #endregion

        #region MyRegion
        public int Id { get; private set; }
        public int UserId { get; private set; }
        public string Text { get; private set; }
        public DateTime Created { get; private set; }
        public string Foto { get; private set; }
        #endregion

        #region Metodo
        public void SetId(int id)
        {
            Id = id;
        }
        #endregion

    }
}
