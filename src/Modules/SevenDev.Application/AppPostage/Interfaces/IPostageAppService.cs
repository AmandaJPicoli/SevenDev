using SevenDev.Application.AppPostage.Input;
using SevenDev.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SevenDev.Application.AppPostage.Interfaces
{
    public interface IPostageAppService
    {
        Task<Postage> InsertAsync(PostageInput input);
        Task<List<Postage>> GetPostageByUserIdAsync();
        Task<List<Postage>> GetAlbum();
    }
}
