using SevenDev.Repositories.IoC.Application;
using SevenDev.Repositories.IoC.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace SevenDev.Repositories.IoC
{
    public class RootBootstrapper
    {
        public void RootRegisterServices(IServiceCollection services)
        {
            new ApplicationBootstraper().ChildServiceRegister(services);
            new RepositoryBootstraper().ChildServiceRegister(services);
        }
    }
}
