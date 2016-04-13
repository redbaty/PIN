# PIN
Package Installer is, as the name suggests, a offline package installer. It uses .iap files to get the correct "silent" arguments for a specific program and execute it.

[To the release page !.](https://github.com/redbaty/PIN/releases)

# Usage
Execute pin, then it'll search through it's directory for .iap files.

# Arguments
Current supported args are
```
-noinstall (run the program but ignore installations)
-noargs (install ignoring any arguments)
-fix (try to update any .iap files)
-ignore=package1;package2... (ignore packages)
```

# Installation File (.iap)
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
- Add a installation file creator.
- Add full chocolatey package support.
- Add upate function.
- Add translation support. (Currently all debug text are in PT-BR)
