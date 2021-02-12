using SevenDev.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SevenDev.Domain.Interfaces
{
    public interface ICommentRepository
    {
        Task<int> InsertAsync(Comments comment);
        Task<List<Comments>> GetByPostageIdAsync(int postageId);
    }
}
