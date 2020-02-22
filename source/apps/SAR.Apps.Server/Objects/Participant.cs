using System;
using System.Collections.Generic;

namespace SAR.Apps.Server.Objects
{
    public class Participant
    {
        public Guid PersonId { get; set; }
        public List<string> AccessTypes { get; set; }
    }
}