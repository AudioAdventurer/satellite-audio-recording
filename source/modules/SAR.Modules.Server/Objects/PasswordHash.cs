using System;
using SAR.Libraries.Database.Objects;

namespace SAR.Modules.Server.Objects
{
    public class PasswordHash : AbstractDbObject
    {
        public Guid UserId { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
    }
}
