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
            Collection.EnsureIndex("ScriptSequenceNumber");
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

        public IEnumerable<CharacterDialog> GetNextByCharacter(
            Guid characterId, 
            int currentScriptSequenceNumber,
            int limit = 1)
        {
            Query q = Query.And(
                Query.All("ScriptSequenceNumber", Query.Ascending),
                Query.EQ("CharacterId", characterId),
                Query.GT("ScriptSequenceNumber", currentScriptSequenceNumber));

            return Collection.Find(q, limit: limit);
        }

        public IEnumerable<CharacterDialog> GetPreviousByCharacter(
            Guid characterId, 
            int currentScriptSequenceNumber,
            int limit = 1)
        {
            Query q = Query.And(
                Query.All("ScriptSequenceNumber", Query.Descending),
                Query.EQ("CharacterId", characterId),
                Query.LT("ScriptSequenceNumber", currentScriptSequenceNumber));

            return Collection.Find(q, limit: limit);
        }

        public void DeleteByProject(Guid projectId)
        {
            Query q = Query.EQ("ProjectId", projectId);
            Collection.Delete(q);
        }
    }
}
