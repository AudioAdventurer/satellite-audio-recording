using System;
using System.Collections.Generic;
using LiteDB;
using SAR.Libraries.Database.Repos;
using SAR.Modules.Script.Objects;

namespace SAR.Modules.Script.Repos
{
    public class CharacterRepo : AbstractRepo<Character>
    {
        public CharacterRepo(LiteDatabase db)
            : base(db, "Characters")
        {
            Collection.EnsureIndex("ProjectId", false);
        }

        public IEnumerable<Character> GetByProject(Guid projectId)
        {
            var q = Query.EQ("ProjectId", projectId);
            return Collection.Find(q);
        }

        public void DeleteByProject(Guid projectId)
        {
            var q = Query.EQ("ProjectId", projectId);
            Collection.DeleteMany(q);
        }
    }
}
