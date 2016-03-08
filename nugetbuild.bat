cd LiveSpec.Extensions.MSpec
nuget.exe pack LiveSpec.Extensions.MSpec.csproj -Verbosity detailed
xcopy *.nupkg "D:\Dev\Nuget" /F /Y
cd ..
