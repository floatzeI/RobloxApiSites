[![Maintainability](https://api.codeclimate.com/v1/badges/c1e6ff9283e40b3db80d/maintainability)](https://codeclimate.com/github/floatzeI/RobloxApiSites/maintainability)
![Dotnet Workflow](https://github.com/floatzeI/RobloxApiSites/actions/workflows/dotnet.yml/badge.svg)
![Code Quality](https://github.com/floatzeI/RobloxApiSites/actions/workflows/codeql-analysis.yml/badge.svg)

# RobloxWeb

An attempt to (sort of) re-create the Roblox ApiSites based off swagger documentation, leaked stack traces/paths, comments in JS source code, comments by Roblox staff, etc. This project is not sponsored, endorsed, or in any way affiliated with Roblox corporation.

### Inconsistencies

- Uses .NET Core instead of .NET Framework

- Does not use postsharp (which Roblox seems to use) 

- Does not add the various microservices Roblox uses (e.g. *.apis.roblox.com), as there is next to no documentation on them, and I don't want to just guess the names of various methods or services they use.

- It's unlikely Roblox uses the shared model format used here (i.e. DB and JSON responses use same models), but for simplicity I just decided to combine them.

- Will use Postgres instead of MSSQL