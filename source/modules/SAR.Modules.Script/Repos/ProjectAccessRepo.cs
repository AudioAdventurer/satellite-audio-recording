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

        public ProjectAccess Get(Guid projectId, Guid personId)
        {
            Query q = Query.And(
                Query.EQ("ProjectId", projectId), 
                Query.EQ("PersonId", personId));

            return Collection.FindOne(q);
        }

        public IEnumerable<ProjectAccess> GetByProject(Guid projectId)
        {
            Query q = Query.EQ("ProjectId", projectId);
            return Collection.Find(q);
        }

        public IEnumerable<ProjectAccess> GetByPerson(Guid personId)
        {
            Query q = Query.EQ("PersonId", personId);
            return Collection.Find(q);
        }
    }
}
