using SevenDev.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SevenDev.Domain.Interfaces
{
    public interface IPostageRepository
    {
        Task<int> InsertAsync(Postage postage);
        Task<List<Postage>> GetPostageByUserIdAsync(int userId);
        Task<List<Postage>> GetAlbum(int userId);
    }

}
