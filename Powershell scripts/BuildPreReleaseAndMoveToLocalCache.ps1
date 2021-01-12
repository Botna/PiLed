cd C:\code\PiLed\PiLed
dotnet pack -c Release -o nupkgs --version-suffix "-beta"
Get-ChildItem -Path ./nupkgs | Move-Item -destination C:\code\LocalNugetCache