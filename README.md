# PIN
PIN or package installer, aims to help massive repetitive programs installation, very similar to chocolatey but most importantly with offline support.

[To the release page !](https://github.com/redbaty/PIN/releases)

# Usage
When PIN is executed it will
* Search through its launch directory for .iap files.
* Sort packages by "InstallationIndex"
* Execute them using the desired arguments and the compatible plataform. (x86 / x64)

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

save it to yourlanguage.tap and launch PIN with the `-l yourlanguage` or `--language yourlanguage` argument.

# Updates
PIN can update packages that are supported by chocolatey, all you have to do is set **"ChocolateySupport"** to **true** in the desired package and execute PIN with the `-u` or `--update` argument. This will update **all packages that have "ChocolateySupport" set to true.**

# Arguments
All arguments can be displayed by launching PIN with `-h` or `--help` argument.

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

You can launch PIN with the argument `-e` or `--example` to create a .iap example.

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
