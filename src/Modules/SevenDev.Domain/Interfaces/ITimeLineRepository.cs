using SevenDev.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SevenDev.Domain.Interfaces
{
    public interface ITimeLineRepository
    {
        Task<List<PostagesTimeLine>> GetTimeLine(int userIdLogado);
        Task<List<CommentsTimeLine>> GetCommentsAsync(int postId);
    }
}
