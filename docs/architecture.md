System Architecture

Overview

Layered architecture separating UI (WPF), application services, domain models, and infrastructure (SQLite, Excel, backup). Core library is cross-platform; UI is Windows-only.

Modules

- Attendance.Core
  - Models: User, AttendanceRecord
  - Services: AuthService, AttendanceService, ExportService, BackupService
  - Infrastructure: SqliteDb, repositories
- Attendance.Wpf
  - Views: Login, Users, Attendance, Reports, Settings
  - ViewModels: LoginViewModel, MainViewModel, UsersViewModel, AttendanceViewModel, ReportsViewModel, SettingsViewModel
- Attendance.Tests
  - Unit tests for core services and repositories

Database Schema (SQLite)

- users
  - id INTEGER PRIMARY KEY
  - username TEXT UNIQUE NOT NULL
  - display_name TEXT NOT NULL
  - password_hash TEXT NOT NULL
  - password_salt TEXT NOT NULL
  - role TEXT NOT NULL
  - created_at TEXT NOT NULL
  - is_active INTEGER NOT NULL
- attendance_records
  - id INTEGER PRIMARY KEY
  - user_id INTEGER NOT NULL
  - login_at TEXT NOT NULL
  - logout_at TEXT
  - notes TEXT
  - FOREIGN KEY(user_id) REFERENCES users(id)

Security Design

- Passwords: PBKDF2 with per-user random salt, high iteration count
- Sensitive fields: AES encryption at rest via application-layer encryption
- Keys: ProtectedData (DPAPI) on Windows for key material; fallback to local secrets
- Access control: Admin-only for user management and exports

Backup Strategy

- Daily automated backup of SQLite DB and config to a local folder, zipped with retention
- Manual backup/restore from Settings

Reporting and Export

- Query attendance by date range and user
- Generate daily/monthly aggregates
- Excel export via ClosedXML with manual format column mapping

Update Mechanism

- Versioning in app settings
- In-app check for updates via configurable URL, manual download and install

Performance Considerations

- Indexed queries on users(id, username) and attendance(user_id, login_at)
- Batched writes for high-volume imports

UI Mockups (Textual)

- Login: Username, Password, Login button
- Main: Tabs [Dashboard | Users | Attendance | Reports | Settings]
- Users: Grid list with Add/Edit/Delete
- Attendance: Per-user records, Login/Logout actions
- Reports: Filters (date range, user), Generate, Export to Excel
- Settings: Backup schedule, Export format, Database location

