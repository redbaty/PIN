# PIN
Package Installer is, as the name suggests, a offline package installer. It uses .iap files to get the correct "silent" arguments for a specific program and execute it.

[To the release page !.](https://github.com/redbaty/PIN/releases)

# Usage
Execute pin, then it'll search through it's directory for .iap files.

# Translations
In 1.1.5.0 translation support were finally added. You can sumbit it on your language using the following form :

```json
{
    "InstallationProgressBarFormat": "{0} de {1} foram instalados. ({2}%)",
    "InstallListLoaded": "Lista de instalação lida.",
    "InstallationSuccess": "Sucesso !",
    "InstallationPackageInvalid": "Pacotes inválidos : {0}",
    "DownloadProgressBarFormat": "Baixando {0:F}MB de {1:F}MB. ({2}%)",
    "GithubInformation": "Nos ajude no github : {0}"
}
```

then save it to yourlanguage.tap, that is .tap is the default language extension.

# Arguments
Current supported args are

```
-ignore=package1;package2... (ignore packages)
-lg=language (type in the language to load without the extension)
-create (launch the windows form creation menu for installation files)
```

# Installation File (.iap)
The installation file goes by the extension .iap and contains the instructions to install the desired program/package. To create your own, you'll have to use the following form :

```json
{
  "Packagename": "7Zip",
  "Version": "0",
  "Url": "test",
  "Arguments": "/S",
  "Executable": "7z.exe",
  "Executablex64": "7zx64.exe",
  "InstallationIndex": 0
}
```

**Being**

- Packagename: The package name.
- Version: The local version.
- Url: The update package location. (Not implemented yet)
- Arguments: The installation silent arguments.
- Executable: The x86 installer name.
- Executablex64: The x64 installer name.
- InstallationIndex: The installation priority.

# Roadmap
- Add a installation file creator. **(Prototyped)**
- Add full chocolatey package support. **(Prototyped)**
- Add upate function.
