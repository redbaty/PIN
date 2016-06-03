namespace PIN.Core.Managers
{
    class Language
    {
        public static Language CurrentLanguage { get; set; } = new Language();

        public string InstallListLoaded { get; set; } = "Installation list loaded !.";
        public string InstallationSuccess { get; set; } = "All packages installed sucessfully !";
        public string InstallationPackageInvalid { get; set; } = "Invalid packages: {0}";
        public string InstallationNoPackageFound { get; set; } = "No valid packages found.";
        public string InstallationProgress { get; set; } = "Installing {0}";
        public string GithubInformation { get; set; } = "Help us at github : {0}";
        public string ProgramDone { get; set; } = "Press any key to exit...";
    }
}

