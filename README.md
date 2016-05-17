# PIN
Package Installer is, as the name suggests, a offline package installer. It uses .iap files to get the correct "silent" arguments for a specific program and execute it.

[To the release page !.](https://github.com/redbaty/PIN/releases)

# Usage
Execute pin, then it'll search through it's directory for .iap files.

# Translations
In 1.1.5.0 translation support were finally added. You can sumbit it on your language using the following form :

```json
{
    "InstallationProgressBarFormat": "{0} of {1} were installed. ({2}%)",
    "InstallListLoaded": "Installation list loaded.",
    "InstallationSuccess": "Success !",
    "InstallationPackageInvalid": "Invalid packages: {0}",
    "DownloadProgressBarFormat": "Downloaded {0:F}MB of {1:F}MB. ({2}%)",
    "GithubInformation": "Help us at github : {0}"
}
```

then save it to yourlanguage.tap, that is .tap is the default language extension.

# Arguments
Current supported args are

```
-ignore=package1;package2... (ignore packages)
-install=package1;package2... (download packages)
-update[=package1;package2...] (if not specified update all packages)
-noinstall (do not start any installation)
-lg=language (type in the language to load without the extension)
```

# Installation File (.iap)
The installation file goes by the extension .iap and contains the instructions to install the desired program/package. To create your own, you'll have to use the following form :

```json
{
  "Version": "50.0.2661.102",
  "Packagename": "googlechrome",
  "Arguments": "/quiet",
  "Executable": "googlechrome.msi",
  "Executablex64": "googlechrome64.msi",
  "InstallationIndex": 0,
  "ChocolateySupport": true,
  "FileName": "googlechrome.iap"
}
```

**Being**

- Packagename: The package name.
- Version: The local version.
- ChocolateySupport: Wheather or not this package can be updated through chocolatey repositories.
- Arguments: The installation silent arguments.
- Executable: The x86 installer name.
- Executablex64: The x64 installer name.
- InstallationIndex: The installation priority.

# Roadmap
- Add a installation file creator.
