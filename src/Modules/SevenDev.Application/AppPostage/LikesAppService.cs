using SevenDev.Application.AppPostage.Interfaces;
using SevenDev.Domain.Core.Interfaces;
using SevenDev.Domain.Entities;
using SevenDev.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SevenDev.Application.AppPostage
{
    public class LikesAppService : ILikesAppService
    {
        private readonly ILikesRepository _likesRepository;
        private readonly ILogged _logged;

        public LikesAppService(ILikesRepository likesRepository,
                                ILogged logged)
        {
            _likesRepository = likesRepository;
            _logged = logged;
        }

        public async Task<int> GetQuantityOfLikesByPostageIdAsync(int postageId)
        {
            return await _likesRepository
                            .GetQuantityOfLikesByPostageIdAsync(postageId)
                            .ConfigureAwait(false);
        }

        public async Task InsertOrDeleteAsync(int postageId)
        {
            var userId = _logged.GetUserLoggedId();

            var likesExistForPostage = await _likesRepository
                                                .GetByUserIdAndPostageIdAsync(userId, postageId)
                                                .ConfigureAwait(false);
            if (likesExistForPostage != null)
            {
                await _likesRepository
                         .DeleteAsync(likesExistForPostage.Id)
                         .ConfigureAwait(false);
            }
            else
            {
                var like = new Likes(postageId, userId);

                await _likesRepository
                        .InsertAsync(like)
                        .ConfigureAwait(false);
            }
        }
    }
}
