using System;
using System.Collections.Generic;
using System.Linq;
using LiteDB;
using SAR.Libraries.Database.Repos;
using SAR.Modules.Script.Objects;

namespace SAR.Modules.Script.Repos
{
    public class CharacterDialogRepo : AbstractRepo<CharacterDialog>
    {
        public CharacterDialogRepo(LiteDatabase db)
            : base(db, "CharacterDialogs")
        {
            Collection.EnsureIndex("ProjectId");
            Collection.EnsureIndex("ScriptElementId");
            Collection.EnsureIndex("CharacterId");
        }

        public IEnumerable<CharacterDialog> GetByProject(Guid projectId)
        {
            Query q = Query.EQ("ProjectId", projectId);

            return Collection.Find(q).OrderBy(cd => cd.ScriptSequenceNumber);
        }

        public IEnumerable<CharacterDialog> GetByCharacter(Guid characterId)
        {
            Query q = Query.EQ("CharacterId", characterId);

            return Collection.Find(q).OrderBy(cd => cd.ScriptSequenceNumber);
        }

        public void DeleteByProject(Guid projectId)
        {
            Query q = Query.EQ("ProjectId", projectId);
            Collection.Delete(q);
        }
    }
}
