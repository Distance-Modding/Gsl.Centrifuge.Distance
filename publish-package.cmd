@echo off
rem this script doesn't have the api key/nuget token for obvious security reasons
pushd %~dp0
for /f %%A in ('dir /b /s "Build\nuget\*.nupkg"') do (
    nuget push %%A -Source https://api.nuget.org/v3/index.json
)
popd