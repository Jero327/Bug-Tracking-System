# Bug-Tracking-System

"# 7413-Assessment" 

dotnet tool install --global dotnet-ef

dotnet ef database drop --force --context BugContext

//delete Migrations files;

dotnet build

dotnet ef migrations add InitialCreate --context BugContext

dotnet ef database update --context BugContext

------------------------------------------------------------------

Add new Model and Context, configure Startup.cs and appsettings.json

OR

Delete OLD controller and views, then:

dotnet aspnet-codegenerator controller -name MoviesController -m Movie -dc MvcMovieContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries