@echo off
start cmd /k "cd carwash-backend && dotnet run"
start cmd /k "cd carwash-frontend && npm install && npm run preview"