using Autofac;
using SAR.Modules.Script.Repos;
using SAR.Modules.Script.Services;
using SAR.Modules.Server.Repos;

namespace SAR.Modules.Script.Modules
{
    public class ScriptModule : Module
    {
        public ScriptModule()
        {

        }

        protected override void Load(ContainerBuilder builder)
        {
            //Repos
            builder.RegisterType<CharacterRepo>();
            builder.RegisterType<CharacterDialogRepo>();
            builder.RegisterType<ScriptElementRepo>();
            builder.RegisterType<PersonRepo>();
            builder.RegisterType<ProjectAccessRepo>();
            builder.RegisterType<ProjectRepo>();
            builder.RegisterType<RecordingRepo>();
            builder.RegisterType<SceneRepo>();

            //Services
            builder.RegisterType<ScriptService>();
        }
    }
}
