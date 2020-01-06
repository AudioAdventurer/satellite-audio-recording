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
            Query q = Query.EQ("ProjectId", projectId);
            Collection.Delete(q);
        }

        public void DeleteByCharacterDialog(Guid characterDialogId)
        {
            Query q = Query.EQ("CharacterDialogId", characterDialogId);
            Collection.Delete(q);
        }

        public void DeleteByPerformer(Guid performerPersonId)
        {
            Query q = Query.EQ("PerformerPersonId", performerPersonId);
            Collection.Delete(q);
        }

        public IEnumerable<Recording> GetByProject(Guid projectId)
        {
            Query q = Query.EQ("ProjectId", projectId);
            return Collection.Find(q);
        }

        public IEnumerable<Recording> GetByCharacterDialog(Guid characterDialogId)
        {
            Query q = Query.EQ("CharacterDialogId", characterDialogId);
            return Collection.Find(q);
        }

        public IEnumerable<Recording> GetByPerformer(Guid performerPersonId)
        {
            Query q = Query.EQ("PerformerPersonId", performerPersonId);
            return Collection.Find(q);
        }
    }
}
