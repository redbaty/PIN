# PIN
PIN, or package installer aims to help massive repetitive programs installation, very similar to chocolatey but most importantly with offline support.

[To the release page !](https://github.com/redbaty/PIN/releases)

# Usage
Execute pin, then it'll search through it's directory for .iap files.

# Translations
In 1.1.5.0 translation support were finally added. You can sumbit it on your language using the following form :

```json
{
    "InstallListLoaded": "Installation list loaded !.",
    "InstallationSuccess": "All packages installed sucessfully !",
    "InstallationPackageInvalid": "Invalid packages: {0}",
    "InstallationNoPackageFound": "No valid packages found.",
    "InstallationProgress": "Installing {0}",
    "GithubInformation": "Help us at github : {0}",
    "ProgramDone": "Press any key to exit..."
}
```

then save it to yourlanguage.tap, that is .tap is the default language extension.

# Arguments
All arguments can be displayed by launching PIN with -h.

# Installation File (.iap)
The installation file goes by the extension .iap and contains the instructions to install the desired program/package. To create your own, you'll have to use the following form :

```json
{
  "Version": "0.0.0.0",
  "Packagename": "Example",
  "Arguments": "-s",
  "Executable": "test.exe",
  "Executablex64": "test64.exe",
  "InstallationIndex": 0,
  "ChocolateySupport": false
}
```

You can launch PIN with the argument -e to create a .iap example.

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
