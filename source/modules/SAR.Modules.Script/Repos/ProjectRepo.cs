using LiteDB;
using SAR.Libraries.Database.Repos;
using SAR.Modules.Script.Objects;

namespace SAR.Modules.Server.Repos
{
    public class ProjectRepo : AbstractRepo<Project>
    {
        public ProjectRepo(
            LiteDatabase db)
            : base(db, "Projects")
        {

        }
    }
}
