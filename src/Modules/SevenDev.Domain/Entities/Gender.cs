namespace SevenDev.Domain.Entities
{
    public class Gender
    {
        #region Construtor
        public Gender(string description)
        {
            Description = description;
        }

        public Gender(int id,
                        string description)
        {
            Id = id;
            Description = description;
        }

        #endregion

        #region propriedades
         public int Id { get; private set; }
        public string Description { get; private set; }
        #endregion

        #region metodos
        public bool IsValid()
        {
            bool valid = true;

            if (string.IsNullOrEmpty(Description))
            {
                valid = false;
            }

            return valid;
        }

        public void SetId(int id)
        {
            Id = id;
        }
        #endregion
    }
}
