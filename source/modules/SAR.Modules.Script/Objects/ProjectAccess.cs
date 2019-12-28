using System;
using System.Collections.Generic;
using System.Text;
using SAR.Libraries.Database.Objects;

namespace SAR.Modules.Script.Objects
{
    public class ProjectAccess : AbstractDbObject
    {
        public ProjectAccess()
        {
            AccessTypes = new List<string>(); 
        }

        public Guid ProjectId { get; set; }
        public Guid PersonId { get; set; }
        public List<string> AccessTypes { get; set; }
    }
}
