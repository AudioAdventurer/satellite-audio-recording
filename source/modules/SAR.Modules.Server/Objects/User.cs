using System;
using SAR.Libraries.Database.Objects;

namespace SAR.Modules.Server.Objects
{
    public class User : AbstractDbObject
    {
        public Guid PersonId { get; set; }

        public string Email { get; set; }

        public string UserType { get; set; }
    }
}
