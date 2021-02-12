using SevenDev.Domain.Entities;
using System.Threading.Tasks;

namespace SevenDev.Application.AppPostage.Interfaces
{
    public interface ILikesAppService
    {
        Task InsertOrDeleteAsync(int postageId);
        Task<int> GetQuantityOfLikesByPostageIdAsync(int postageId);
    }
}
