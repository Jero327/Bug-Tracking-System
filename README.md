# Bug-Tracking-System

"# 7413-Assessment" 

Drop the database;

delete Migrations files;

dotnet build;

dotnet ef migrations add InitialCreate --context BugContext

dotnet ef database update --context BugContext
