using System;
using SAR.Libraries.Database.Objects;

namespace SAR.Modules.Server.Objects
{
    public class UserSession : AbstractDbObject
    {
        public Guid UserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ExpiresOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
