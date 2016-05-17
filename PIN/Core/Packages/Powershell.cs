using System;

namespace PIN.Core.Packages
{
    class PowerShell
    {
        public string URL86 { get; set; }
        public string URL64 { get; set; }
        public string FileType { get; set; }
        public string Argument { get; set; }
        private string Source { get; }

        public PowerShell(string source, Version version)
        {
            Source = source;

            if (!source.Contains("$fileType") && source.Contains("msi"))
                FileType = "msi";
            else if (source.Contains("$fileType"))
                FileType = Utils.GetPowershellValue("$fileType", Source).Replace("\r", "");

            if (Source.Contains("$silentArgs"))
                Argument = Utils.GetPowershellValue("$silentArgs", Source).Replace("\r", "");
            if (Source.Contains("$url"))
                URL86 = GetUrl86(Source, version).Replace("https", "http");
            if (Source.Contains("$url64"))
                URL64 = GetUrl64(Source, version).Replace("https", "http");

        }

        private string GetUrl86(string source, Version newestversion)
        {
            return Utils.GetPowershellValue("$url", source).Replace("${version}", newestversion.ToString()).Replace("${locale}", "pt-BR");
        }

        private string GetUrl64(string source, Version newestversion)
        {
            return Utils.GetPowershellValue("$url64", source).Replace("${version}", newestversion.ToString()).Replace("${locale}", "pt-BR");
        }
    }
}
