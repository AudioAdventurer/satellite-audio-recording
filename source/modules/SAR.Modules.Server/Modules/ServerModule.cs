using Autofac;
using SAR.Modules.Server.Repos;
using SAR.Modules.Server.Services;

namespace SAR.Modules.Server.Modules
{
    public class ServerModule : Module
    {
        public ServerModule()
        {

        }

        protected override void Load(ContainerBuilder builder)
        {
            //Repos
            builder.RegisterType<UserRepo>();
            builder.RegisterType<PasswordHashRepo>();
            builder.RegisterType<UserSessionRepo>();

            //Services
            builder.RegisterType<ServerService>();
        }
    }
}
