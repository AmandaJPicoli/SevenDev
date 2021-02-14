using SevenDev.Application.AppUser.Input;
using SevenDev.Application.AppUser.Interfaces;
using SevenDev.Application.AppUser.Output;
using SevenDev.Domain.Core.Interfaces;
using SevenDev.Domain.Entities;
using SevenDev.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace SevenDev.Application.AppUser
{
    public class UserAppService : IUserAppService
    {
        private readonly IGenderRepository _genderRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogged _logged;

        public UserAppService(IGenderRepository genderRepository,
                                IUserRepository userRepository,
                                ILogged logged)
        {
            _genderRepository = genderRepository;
            _userRepository = userRepository;
            _logged = logged;
        }
        public async Task<UserViewModel> GetByIdAsync(int id)
        {
            var user = await _userRepository
                                .GetByIdAsync(id)
                                .ConfigureAwait(false);

            if (user is null)
                return default;

            return new UserViewModel()
            {
                Id = user.Id,
                Name = user.Name,
                Birthday = user.Birthday,
                Email = user.Email,
                Gender = user.Gender,
                Photo = user.Photo
            };
        }

        public async Task<UserViewModel> InsertAsync(UserInput input)
        {
            var gender = await _genderRepository
                                   .GetByIdAsync(input.GenderId)
                                   .ConfigureAwait(false);

            if (gender is null)
            {
                throw new ArgumentException("O genero que está tentando associar ao usuário não existe!");
            }

            var user = new User(input.Email,
                                 input.Password,
                                 input.Name,
                                 input.Birthday,
                                 new Gender(gender.Id, gender.Description),
                                 input.Photo);

            if (!user.IsValid())
            {
                throw new ArgumentException("Existem dados que são obrigatórios e não foram preenchidos");
            }

            var id = await _userRepository
                                .InsertAsync(user)
                                .ConfigureAwait(false);

            return new UserViewModel()
            {
                Id = id,
                Name = user.Name,
                Birthday = user.Birthday,
                Email = user.Email,
                Gender = user.Gender,
                Photo = user.Photo
            };
        }

       
        public async Task<ConviteOutPut> InsertInviteAsync(int userIdReceive)
        {
            var userIdInvited = _logged.GetUserLoggedId();

            var invited = await _userRepository
                                    .InsertInviteAsync(userIdInvited, userIdReceive)
                                    .ConfigureAwait(false);

            var resposta = new ConviteOutPut();

            if (invited >= 0)
            {
                resposta = new ConviteOutPut()
                {
                    Message = "Convite enviado com sucesso",
                    Status = 1
                };
            }
            else
            {
                resposta = new ConviteOutPut()
                {
                    Message = "Convite não enviado",
                    Status = 0
                };
            }

            return resposta; 
        }

        public async Task<UserViewModel> UpdateAsync(int id, UserUpdateInput updateInput)
        {
            var user = await _userRepository
                                     .GetByIdAsync(id)
                                     .ConfigureAwait(false);

            if (user is null)
            {
                throw new ArgumentException("Usuário não encontrado");
            }

            var gender = await _genderRepository
                                   .GetByIdAsync(updateInput.GenderId)
                                   .ConfigureAwait(false);

            if (gender is null)
            {
                throw new ArgumentException("O gênero que está tentando associar ao usuário não existe!");
            }

            user.UpdateInfo(updateInput.Email, updateInput.Password, updateInput.Name, updateInput.Photo, updateInput.GenderId);

            await _userRepository
                    .UpdateAsync(user)
                    .ConfigureAwait(false);

            return new UserViewModel()
            {
                Id = id,
                Name = user.Name,
                Birthday = user.Birthday,
                Email = user.Email,
                Gender = user.Gender,
                Photo = user.Photo
            };
        }

        
    }
}
