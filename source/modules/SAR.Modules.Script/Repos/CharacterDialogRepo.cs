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
            return Collection.Query()
                .Where(cd=> cd.ProjectId == projectId)
                .OrderBy(cd => cd.ScriptSequenceNumber)
                .ToEnumerable();
        }

        public IEnumerable<CharacterDialog> GetByCharacter(Guid characterId)
        {
            return Collection.Query()
                .Where(cd => cd.CharacterId == characterId)
                .OrderBy(cd => cd.ScriptSequenceNumber)
                .ToEnumerable();
        }

        public IEnumerable<CharacterDialog> GetNextByCharacter(
            Guid characterId, 
            int currentScriptSequenceNumber,
            int limit = 1)
        {
            return Collection.Query()
                .Where(x => x.CharacterId == characterId
                            && x.ScriptSequenceNumber > currentScriptSequenceNumber)
                .OrderBy(x => x.ScriptSequenceNumber)
                .Limit(limit)
                .ToEnumerable();
        }

        public IEnumerable<CharacterDialog> GetPreviousByCharacter(
            Guid characterId, 
            int currentScriptSequenceNumber,
            int limit = 1)
        {
            return Collection.Query()
                .Where(x => x.CharacterId == characterId
                            && x.ScriptSequenceNumber < currentScriptSequenceNumber)
                .OrderBy(x => x.ScriptSequenceNumber)
                .Limit(limit)
                .ToEnumerable();
        }

        public void DeleteByProject(Guid projectId)
        {
            var q = Query.EQ("ProjectId", projectId);
            Collection.DeleteMany(q);
        }
    }
}
