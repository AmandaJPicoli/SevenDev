using SevenDev.Application.AppPostage;
using SevenDev.Application.AppPostage.Interfaces;
using SevenDev.Application.AppUser;
using SevenDev.Application.AppUser.Interfaces;
using SevenDev.Domain.Core;
using SevenDev.Domain.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SevenDev.Application.AppGender.Interfaces;
using SevenDev.Application.AppGender;

namespace SevenDev.Repositories.IoC.Application
{
    internal class ApplicationBootstraper
    {
        internal void ChildServiceRegister(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ILogged, Logged>();
            services.AddScoped<IStorageHelper, StorageHelper>();

            services.AddScoped<IUserAppService, UserAppService>();
            services.AddScoped<ILoginAppService, LoginAppService>();
            services.AddScoped<IPostageAppService, PostageAppService>();
            services.AddScoped<ICommentAppService, CommentAppService>();
            services.AddScoped<ILikesAppService, LikesAppService>();
            services.AddScoped<IGenderAppService, GenderAppService>();

        }
    }
}
