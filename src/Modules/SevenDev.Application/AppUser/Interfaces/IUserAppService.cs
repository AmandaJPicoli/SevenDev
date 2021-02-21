using SevenDev.Application.AppUser.Input;
using SevenDev.Application.AppUser.Output;
using SevenDev.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SevenDev.Application.AppUser.Interfaces
{
    public interface IUserAppService
    {
        Task<UserViewModel> InsertAsync(UserInput input);
        Task<UserViewModel> GetByIdAsync(int id);
        Task<UserViewModel> UpdateAsync(UserUpdateInput updateInput);
        Task<ConviteOutPut> InsertInviteAsync(int userIdReceive);
        Task<InviteFriends> AcceptDeniedInvite(InviteFriends invite);
        Task<List<InviteFriends>> GetAllInvitesReceive();

    }
}
