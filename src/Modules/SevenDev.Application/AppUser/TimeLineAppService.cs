using SevenDev.Application.AppPostage.Interfaces;
using SevenDev.Application.AppUser.Interfaces;
using SevenDev.Domain.Core.Interfaces;
using SevenDev.Domain.Entities;
using SevenDev.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SevenDev.Application.AppUser
{
    public class TimeLineAppService : ITimeLineAppService
    {
        private readonly ITimeLineRepository _timeLineRepository;
        private readonly ILikesRepository _likesRepository;
        private readonly ILogged _logged;

        public TimeLineAppService(ITimeLineRepository timeLineRepository,
            ILogged logged,
            ILikesRepository likesRepository)
        {
            _timeLineRepository = timeLineRepository;
            _logged = logged;
            _likesRepository = likesRepository;
        }

        public async Task<List<PostagesTimeLine>> GetTimeLineByUserId()
        {
            var userIdLogado = _logged.GetUserLoggedId();

            var postagesList = await _timeLineRepository
                                     .GetTimeLine(userIdLogado)
                                     .ConfigureAwait(false);
            
            foreach (var post in postagesList)
            {
                var likes =  await _likesRepository
                                     .GetQuantityOfLikesByPostageIdAsync(post.IdPost)
                                     .ConfigureAwait(false);

                var comments = await _timeLineRepository
                                     .GetCommentsAsync(post.IdPost)
                                     .ConfigureAwait(false);

                if (comments != null)
                {
                    post.AddComments(comments);
                }
                if (likes > 0)
                {
                    post.AddLikes(likes);
                }
            }
            return postagesList;
        }
    }
}
