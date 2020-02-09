using System;
using System.Collections.Generic;
using LiteDB;
using SAR.Libraries.Database.Repos;
using SAR.Modules.Script.Objects;

namespace SAR.Modules.Script.Repos
{
    public class ProjectAccessRepo : AbstractRepo<ProjectAccess>
    {
        public ProjectAccessRepo(LiteDatabase db)
            : base(db, "ProjectAccess")
        {
            Collection.EnsureIndex("ProjectId", false);
            Collection.EnsureIndex("PersonId", false);
        }

        public ProjectAccess Get(Guid personId, Guid projectId)
        {
            var q = Query.And(
                Query.EQ("ProjectId", projectId), 
                Query.EQ("PersonId", personId));

            return Collection.FindOne(q);
        }

        public IEnumerable<ProjectAccess> GetByProject(Guid projectId)
        {
            var q = Query.EQ("ProjectId", projectId);
            return Collection.Find(q);
        }

        public IEnumerable<ProjectAccess> GetByPerson(Guid personId)
        {
            var q = Query.EQ("PersonId", personId);
            return Collection.Find(q);
        }

        public void DeleteByProject(Guid projectId)
        {
            var q = Query.EQ("ProjectId", projectId);
            Collection.DeleteMany(q);
        }
    }
}
