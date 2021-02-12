namespace SevenDev.Domain.Entities
{
    public class Likes
    {
        public Likes(int postageId,
                        int userId)
        {
            PostageId = postageId;
            UserId = userId;
        }

        public Likes(int id,
                        int postageId,
                        int userId)
        {
            Id = id;
            PostageId = postageId;
            UserId = userId;
        }

        public int Id { get; private set; }
        public int PostageId { get; private set; }
        public int UserId { get; private set; }
    }
}
