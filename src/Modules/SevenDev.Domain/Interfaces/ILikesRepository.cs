using SevenDev.Domain.Entities;
using System.Threading.Tasks;

namespace SevenDev.Domain.Interfaces
{
    public interface ILikesRepository
    {
        Task<int> InsertAsync(Likes likes);
        Task DeleteAsync(int id);
        Task<int> GetQuantityOfLikesByPostageIdAsync(int postageId);
        Task<Likes> GetByUserIdAndPostageIdAsync(int userId, int postageId);
    }
}
