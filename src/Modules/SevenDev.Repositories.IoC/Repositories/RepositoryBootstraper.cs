using SevenDev.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace SevenDev.Repositories.IoC.Repositories
{
    internal class RepositoryBootstraper
    {
        internal void ChildServiceRegister(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IGenderRepository, GenderRepository>();
            services.AddScoped<IPostageRepository, PostageRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ILikesRepository, LikesRepository>();
            services.AddScoped<ITimeLineRepository, TimeLineRepository>();
        }
    }
}
