using LiteDB;
using SAR.Libraries.Database.Repos;
using SAR.Modules.Server.Objects;

namespace SAR.Modules.Server.Repos
{
    public class UserSessionRepo : AbstractRepo<UserSession>
    {
        public UserSessionRepo(
            LiteDatabase db)
            : base(db, "UserSessions")
        {

        }
    }
}
