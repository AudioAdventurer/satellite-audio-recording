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
            return Collection.Query()
                .Where(r => r.ProjectId == projectId)
                .ToEnumerable();
        }

        public IEnumerable<Recording> GetByCharacterDialog(Guid characterDialogId)
        {
            return Collection.Query()
                .Where(r => r.CharacterDialogId == characterDialogId)
                .OrderBy(r => r.SequenceNumber)
                .ToEnumerable();
        }

        public IEnumerable<Recording> GetByPerformer(Guid performerPersonId)
        {
            return Collection.Query()
                .Where(r => r.PerformerPersonId == performerPersonId)
                .OrderBy(r => r.RecordedOn)
                .ToEnumerable();
        }
    }
}
