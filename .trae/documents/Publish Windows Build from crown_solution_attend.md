## Goal
- Copy the current project into `/Users/bhaveshvarma/Pictures/CROWN_SOLUTIONS/attendance/crown_solution_attend/` so it can be versioned and built via CI for Windows.

## Copy Strategy (macOS Terminal)
- Create the target directory if it does not exist:
  - `mkdir -p "/Users/bhaveshvarma/Pictures/CROWN_SOLUTIONS/attendance/crown_solution_attend"`
- From the current project root (`/Users/bhaveshvarma/Pictures/CROWN_SOLUTIONS/attendance`), copy all code except build artifacts and the target folder itself:
  - `rsync -a --delete --exclude='crown_solution_attend' --exclude='bin' --exclude='obj' --exclude='.git' --exclude='.DS_Store' ./ "/Users/bhaveshvarma/Pictures/CROWN_SOLUTIONS/attendance/crown_solution_attend/"`
- This preserves structure (`Attendance.Wpf`, `Attendance.Core`, `Attendance.Tests`, `docs`, `.github/workflows/windows-publish.yml`) so references like `..\Attendance.Core` remain valid.

## Initialize Git and Push (Optional)
- Inside the new repo folder:
  - `cd "/Users/bhaveshvarma/Pictures/CROWN_SOLUTIONS/attendance/crown_solution_attend"`
  - `git init`
  - `git add . && git commit -m "Import project"`
  - `git branch -M main`
  - `git remote add origin https://github.com/<your-username>/crown_solution_attend.git`
  - `git push -u origin main`

## Trigger Windows Build via CI
- Tag and push to start the workflow:
  - `git tag v1.0.0 && git push --tags`
- Or manually run the workflow in GitHub Actions.

## Download and Deliver
- Download the artifact `AttendanceApp-win-x64` from the workflow run.
- Share the `.exe` or zip the `publish` folder with your brother.

## Notes
- If your repo structure under `crown_solution_attend` differs later, we will adjust the workflow’s `dotnet publish` path accordingly.
- No changes to code are required for copying; we exclude transient folders to keep the repo clean.