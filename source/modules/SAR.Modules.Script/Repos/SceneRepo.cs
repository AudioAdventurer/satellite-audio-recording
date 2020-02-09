﻿using System;
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
            return Collection.Query()
                .Where(s => s.ProjectId == projectId)
                .OrderBy(s => s.ScriptSequenceNumber)
                .ToEnumerable();
        }

        public void DeleteByProject(Guid projectId)
        {
            var q = Query.EQ("ProjectId", projectId);
            Collection.DeleteMany(q);
        }
    }
}
