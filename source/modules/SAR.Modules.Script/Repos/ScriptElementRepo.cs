using System;
using System.Collections.Generic;
using System.Linq;
using LiteDB;
using SAR.Libraries.Database.Repos;
using SAR.Modules.Script.Objects;

namespace SAR.Modules.Script.Repos
{
    public class ScriptElementRepo : AbstractRepo<ScriptElement>
    {
        public ScriptElementRepo(LiteDatabase db)
            : base(db, "ScriptElements")
        {
            Collection.EnsureIndex("ProjectId", false);
        }

        public IEnumerable<ScriptElement> GetByProject(Guid projectId)
        {
            Query q = Query.EQ("ProjectId", projectId);
            var items = Collection.Find(q);

            return items.OrderBy(i => i.SequenceNumber);
        }

        public void DeleteByProject(Guid projectId)
        {
            Query q = Query.EQ("ProjectId", projectId);
            Collection.Delete(q);
        }
    }
}
