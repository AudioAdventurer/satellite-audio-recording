using System;
using LiteDB;
using SAR.Libraries.Database.Repos;
using SAR.Modules.Server.Objects;

namespace SAR.Modules.Server.Repos
{
    public class PasswordHashRepo : AbstractRepo<PasswordHash>
    {
        public PasswordHashRepo(
            LiteDatabase db)
            : base(db, "PasswordHashes")
        {
            Collection.EnsureIndex("UserId", true);
        }

        public PasswordHash GetByUser(Guid userId)
        {
            Query q = Query.EQ("UserId", userId);
            return Collection.FindOne(q);
        }
    }
}
