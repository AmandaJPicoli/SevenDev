using SevenDev.Application.AppUser.Input;
using SevenDev.Application.AppUser.Output;
using System.Threading.Tasks;

namespace SevenDev.Application.AppUser.Interfaces
{
    public interface IUserAppService
    {
        Task<UserViewModel> InsertAsync(UserInput input);
        Task<UserViewModel> GetByIdAsync(int id);
        Task<UserViewModel> UpdateAsync(int id, UserUpdateInput updateInput);
        Task<ConviteOutPut> InsertInviteAsync(int userIdReceive);
    }
}
