Office Attendance Management (Windows WPF)

Overview

This repository contains a Windows desktop application for office attendance management built with WPF and a modular .NET architecture. It includes secure authentication, automatic login/logout timestamping, user management, reporting with Excel export, and automated backups.

Key Modules

- Attendance.Core: Cross-platform core logic (SQLite, auth, export, backup)
- Attendance.Wpf: Windows-only WPF UI using MVVM
- Attendance.Tests: Unit tests for core modules

Build Requirements

- Windows 10+ with .NET 6 SDK and Visual Studio 2022
- SQLite database file is created automatically on first run

Quick Start (Windows)

1. Open the solution in Visual Studio on Windows
2. Set Attendance.Wpf as startup project
3. Build and run

Export Format

Excel exports match the manual format with configurable column order, date range, and user filters.

