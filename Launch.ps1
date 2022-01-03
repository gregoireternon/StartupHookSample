$scriptpath = $PSScriptRoot
Write-Host $scriptpath
cd $scriptpath
dotnet build StartupHook.sln

$Env:DOTNET_STARTUP_HOOKS = $scriptpath+"\StartupHook\bin\Debug\netstandard2.0\StartupHookPJ.dll"

dotnet run --project .\ListVegetables\ListVegetables.csproj