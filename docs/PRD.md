Title: Office Attendance Management – Product Requirements Document

Goal

Provide a Windows desktop application to manage employee attendance with secure login/logout tracking, user management, reporting, and Excel export aligned with the current manual format.

Core Features

- Authentication with secure password hashing
- Login/Logout with automatic timestamps
- User management: add/edit/remove, roles
- Reports: daily and monthly summaries, per-user filters
- Excel export matching manual format, date range and user filters
- Automatic backups, configurable retention

User Personas

- Employee: performs login/logout
- Administrator: manages users, reviews reports, runs exports, restores backups

User Flows

- Employee opens app → logs in → clicks Login to start day → clicks Logout to end day
- Admin opens app → navigates Users to add/edit/remove → reviews Attendance and Reports → exports to Excel

Non-Functional Requirements

- Windows desktop UI responsive, MVVM
- Local storage via SQLite for portability
- Security: PBKDF2 password hashing, AES encryption for sensitive fields, least-privilege access
- Backup: daily automatic DB snapshot and manual export, retention policy
- Performance: 10k+ records with sub-second report generation

Constraints

- Offline-first
- No external services required

Deliverables

- WPF app, core library, test suite
- Installation package and documentation

