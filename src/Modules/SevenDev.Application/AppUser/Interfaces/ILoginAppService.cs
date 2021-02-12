using SevenDev.Application.AppUser.Output;
using System.Threading.Tasks;

namespace SevenDev.Application.AppUser.Interfaces
{
    public interface ILoginAppService
    {
        Task<UserViewModel> LoginAsync(string login, string password);
    }
}
