using Autofac;
using SAR.Modules.Script.Importer.Importers;

namespace SAR.Modules.Script.Importer.Modules
{
    public class ImporterModule : Module
    {
        public ImporterModule()
        {

        }

        protected override void Load(ContainerBuilder builder)
        {
            //Importers
            builder.RegisterType<FountainImporter>();
        }
    }
}
