## Why Windows Build Is Needed

* `Attendance.Wpf` targets `net6.0-windows` and uses WPF, which only builds on Windows. Building on macOS directly is not supported.

* Confirmed project setup:

  * `Attendance.Wpf/Attendance.Wpf.csproj` → `TargetFramework: net6.0-windows`, `UseWPF: true`.

  * `Attendance.Core/Attendance.Core.csproj` → `TargetFrameworks: net6.0;net9.0`, packages: `Microsoft.Data.Sqlite`, `ClosedXML`.

## Option A: Build via GitHub Actions (Recommended)

* Runs on a Windows runner to produce a ready-to-share build automatically.

* Steps:

  * Push your repo to GitHub.

  * Add a workflow (e.g., `.github/workflows/windows-publish.yml`) to publish a self-contained single-file exe and upload as an artifact.

```YAML
name: Build Windows WPF
on:
  workflow_dispatch:
  push:
    tags: [ 'v*' ]
jobs:
  build:
    runs-on: windows-latest
    steps:
      - uses: actions/checkout@v4
      - uses: actions/setup-dotnet@v4
        with:
          dotnet-version: '6.0.x'
      - name: Restore
        run: dotnet restore
      - name: Publish (self-contained single exe)
        run: >
          dotnet publish Attendance.Wpf/Attendance.Wpf.csproj -c Release -r win-x64
          --self-contained true /p:PublishSingleFile=true
          /p:IncludeNativeLibrariesForSelfExtract=true /p:EnableCompressionInSingleFile=true
      - name: Upload artifact
        uses: actions/upload-artifact@v4
        with:
          name: AttendanceApp-win-x64
          path: Attendance.Wpf/bin/Release/net6.0-windows/win-x64/publish/
```

* Download the artifact from the workflow run and send the folder or the single `.exe` to your brother.

## Option B: Build in a Windows VM on Your Mac

* Use Parallels Desktop or VMware to install Windows 11.

* In the Windows VM:

  * Install `.NET 6 SDK (Windows)` and Visual Studio Build Tools with “Desktop development with C#”.

  * Clone the repo and run:

    * `dotnet restore`

    * `dotnet publish Attendance.Wpf/Attendance.Wpf.csproj -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true /p:EnableCompressionInSingleFile=true`

  * The build output is under `Attendance.Wpf/bin/Release/net6.0-windows/win-x64/publish/`.

* Zip the `publish` folder or share the single `.exe`.

## Distribution Choices

* Self-contained single-file `.exe` (no runtime needed) → simplest handoff.

* Zip of the `publish` folder (contains all files) → robust fallback.

* MSIX/MSI installer (optional) → requires Windows packaging tooling; best for long-term distribution.

## Brother’s PC Prerequisites

* Self-contained publish: none; just run the `.exe`.

* Framework-dependent publish: install `Microsoft .NET 6 Desktop Runtime (Windows)`.

* App writes data under `%AppData%/AttendanceApp/`; ensure standard user permissions.

## Verification Before Sharing

* On a Windows machine/VM, run the exported `.exe` and verify:

  * Login/logout works and records are saved.

  * Excel export produces a file and opens successfully.

  * Backup folder is created under `%AppData%/AttendanceApp/backups`.

## Next Actions

* Confirm preferred route (CI build vs. VM build).

* If CI is preferred, I will add the GitHub workflow and guide you to download the artifact.

* If VM build is preferred, follow the commands above; I can refine packaging to MSIX/MSI later if desired.

