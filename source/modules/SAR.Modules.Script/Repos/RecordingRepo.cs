using System;
using System.Collections.Generic;
using LiteDB;
using SAR.Libraries.Database.Repos;
using SAR.Modules.Script.Objects;

namespace SAR.Modules.Script.Repos
{
    public class RecordingRepo : AbstractRepo<Recording>
    {
        public RecordingRepo(LiteDatabase db)
            : base(db, "Recordings")
        {
            Collection.EnsureIndex("ProjectId");
            Collection.EnsureIndex("PerformerPersonId");
            Collection.EnsureIndex("CharacterDialogId");
        }

        public void DeleteByProject(Guid projectId)
        {
            var q = Query.EQ("ProjectId", projectId);
            Collection.DeleteMany(q);
        }

        public void DeleteByCharacterDialog(Guid characterDialogId)
        {
            var q = Query.EQ("CharacterDialogId", characterDialogId);
            Collection.DeleteMany(q);
        }

        public void DeleteByPerformer(Guid performerPersonId)
        {
            var q = Query.EQ("PerformerPersonId", performerPersonId);
            Collection.DeleteMany(q);
        }

        public IEnumerable<Recording> GetByProject(Guid projectId)
        {
            var q = Query.EQ("ProjectId", projectId);
            return Collection.Find(q);
        }

        public IEnumerable<Recording> GetByCharacterDialog(Guid characterDialogId)
        {
            var q = Query.EQ("CharacterDialogId", characterDialogId);
            return Collection.Find(q);
        }

        public IEnumerable<Recording> GetByPerformer(Guid performerPersonId)
        {
            var q = Query.EQ("PerformerPersonId", performerPersonId);
            return Collection.Find(q);
        }
    }
}
