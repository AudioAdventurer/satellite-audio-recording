using System.IO;
using Autofac;
using LiteDB;
using SAR.Apps.Server.Services;
using SAR.Libraries.Common.Helpers;
using SAR.Libraries.Common.Interfaces;
using SAR.Libraries.Common.Logging;
using SAR.Libraries.Common.Storage;

namespace SAR.Apps.Server.Modules
{
    public class SarWebServerModule : Module
    {
        private readonly Config _config;

        public SarWebServerModule(Config config)
        {
            _config = config;
        }

        protected override void Load(ContainerBuilder builder)
        {
            SetupFolders();

            builder.RegisterType<ConsoleLogger>().As<ISarLogger>();

            builder.Register(c =>
                {
                    string filename = Path.Combine(_config.DbFolder, "sar.db");
                    string connect = $"Filename={filename};Connection=direct;Upgrade=true";
                    
                    return new LiteDatabase(connect);

                }).As<LiteDatabase>()
                .SingleInstance();

            builder.Register(c => new JwtService(_config.JwtSecret))
                .As<JwtService>();

            //Local Services
            builder.RegisterType<AuthService>();
            builder.RegisterType<ProjectService>();

            builder.Register(c => new LocalStorage(_config.FilesFolder))
                .As<IFileStorage>();
        }

        private void SetupFolders()
        {
            DirectoryHelper.EnsureDirectory(_config.DbFolder);
            DirectoryHelper.EnsureDirectory(_config.FilesFolder);
        }
    }
}
