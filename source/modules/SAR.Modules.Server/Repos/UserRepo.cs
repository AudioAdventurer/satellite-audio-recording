using System;
using LiteDB;
using SAR.Libraries.Database.Repos;
using SAR.Modules.Server.Objects;

namespace SAR.Modules.Server.Repos
{
    public class UserRepo : AbstractRepo<User>
    {
        public UserRepo(
            LiteDatabase db)
            : base(db, "Users")
        {
            Collection.EnsureIndex("Email", true);
            Collection.EnsureIndex("PersonId", true);
        }

        public User GetByEmail(string email)
        {
            Query q = Query.EQ("Email", email);
            return Collection.FindOne(q);
        }

        public User GetByPerson(Guid personId)
        {
            Query q = Query.EQ("PersonId", personId);
            return Collection.FindOne(q);
        }

        public int GetUserCount()
        {
            return Collection.Count();
        }
    }
}
