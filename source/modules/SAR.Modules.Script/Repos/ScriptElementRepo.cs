using System;
using System.Collections.Generic;
using System.Linq;
using LiteDB;
using SAR.Libraries.Database.Repos;
using SAR.Modules.Script.Objects;

namespace SAR.Modules.Script.Repos
{
    public class ScriptElementRepo : AbstractRepo<ScriptElement>
    {
        public ScriptElementRepo(LiteDatabase db)
            : base(db, "ScriptElements")
        {
            Collection.EnsureIndex("ProjectId", false);
            Collection.EnsureIndex("SequenceNumber", false);
        }

        public IEnumerable<ScriptElement> GetByProject(Guid projectId)
        {
            return Collection.Query()
                .Where(s => s.ProjectId == projectId)
                .OrderBy(s => s.SequenceNumber)
                .ToEnumerable();
        }

        public void DeleteByProject(Guid projectId)
        {
            var q = Query.EQ("ProjectId", projectId);
            Collection.DeleteMany(q);
        }

        public IEnumerable<ScriptElement> GetByProject(Guid projectId, int? startPosition, int? endPosition)
        {
            int start = 0;
            if (startPosition != null)
            {
                start = startPosition.Value;
            }

            int end = int.MaxValue;
            if (endPosition != null)
            {
                end = endPosition.Value;
            }

            return Collection.Query()
                .Where(s => s.ProjectId == projectId
                            && s.SequenceNumber >= start
                            && s.SequenceNumber <= end)
                .OrderBy(s => s.SequenceNumber)
                .ToEnumerable();
        }
    }
}
