cd LiveSpec.Extensions.MSpec
nuget.exe pack -Verbosity detailed
xcopy *.nupkg "D:\Dev\Nuget" /F /Y
cd ..
