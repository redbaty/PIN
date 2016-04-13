namespace PIN.Core.jModels
{
    public class Metadata
    {
        public string id { get; set; }
        public string version { get; set; }
        public string title { get; set; }
        public string authors { get; set; }
        public string owners { get; set; }
        public string licenseUrl { get; set; }
        public string projectUrl { get; set; }
        public string iconUrl { get; set; }
        public string requireLicenseAcceptance { get; set; }
        public string description { get; set; }
        public string summary { get; set; }
        public string tags { get; set; }
        public string packageSourceUrl { get; set; }
    }

    public class Package
    {
        public string xmlns { get; set; }
        public Metadata metadata { get; set; }
    }

    public class PackageInformation
    {
        public Package package { get; set; }
    }
}
