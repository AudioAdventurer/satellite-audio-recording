using System;

namespace SAR.Libraries.Common.Interfaces
{
    public interface IWebSession
    {
        Guid Id { get; set; }
        Guid UserId { get; set; }
        string GivenName { get; set; }
        string FamilyName { get; set; }
        string Email { get; set; }
    }
}
