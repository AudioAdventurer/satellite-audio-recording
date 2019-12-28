using System;
using System.Collections.Generic;
using LiteDB;
using SAR.Libraries.Database.Repos;
using SAR.Modules.Script.Objects;

namespace SAR.Modules.Script.Repos
{
    public class ElementRepo : AbstractRepo<Element>
    {
        public ElementRepo(LiteDatabase db)
            : base(db, "Elements")
        {
            Collection.EnsureIndex("SceneId", false);
        }

        public IEnumerable<Element> GetByScene(Guid sceneId)
        {
            Query q = Query.EQ("SceneId", sceneId);
            return Collection.Find(q);
        }
    }
}
