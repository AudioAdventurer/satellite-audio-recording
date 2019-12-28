namespace SAR.Apps.Server.Objects
{
    public class SetupRequest
    {
        public string OwnerEmail { get; set; }

        public string OwnerPassword { get; set; }

        public string OwnerGivenName { get; set; }

        public string OwnerFamilyName { get; set; }

        public string InitialProjectName { get; set; }
    }
}
