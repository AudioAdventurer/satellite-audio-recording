using System;
using System.Collections.Generic;
using System.Linq;
using LiteDB;
using SAR.Libraries.Database.Repos;
using SAR.Modules.Script.Objects;

namespace SAR.Modules.Script.Repos
{
    public class SceneRepo : AbstractRepo<Scene>
    {
        public SceneRepo(LiteDatabase db)
            : base(db, "Scenes")
        {
            Collection.EnsureIndex("ProjectId");
        }

        public IEnumerable<Scene> GetByProject(Guid projectId)
        {
            Query q = Query.EQ("ProjectId", projectId);
            return Collection.Find(q).OrderBy(s => s.ScriptSequenceNumber);
        }

        public void DeleteByProject(Guid projectId)
        {
            Query q = Query.EQ("ProjectId", projectId);
            Collection.Delete(q);
        }
    }
}
