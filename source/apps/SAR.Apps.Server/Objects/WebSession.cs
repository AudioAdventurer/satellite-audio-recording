using System;
using SAR.Libraries.Common.Interfaces;

namespace SAR.Apps.Server.Objects
{
    public class WebSession : IWebSession
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid PersonId { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Email { get; set; }
    }
}
