var target = Argument("target", "Default");
var configuration = Argument("buildconfiguration", "Debug");
var nugetsource = Argument("nugetsource", "http://192.168.1.100:81/nuget/Default/");
var packages = "./packages";
var artifacts = "./artifacts/";
var solution = "./Monolith.sln";
var project = "./Monolith/Monolith.csproj";

var nugetRestoreSettings = new NuGetRestoreSettings
	{
		PackagesDirectory = packages
	};

var nugetUpdateSettings = new NuGetUpdateSettings
	{
	};

var nugetPackSettings = new NuGetPackSettings
	{
		OutputDirectory = artifacts,
		Symbols = true,
		Properties = new Dictionary<string, string>{{"Configuration", configuration}}
	};

var nugetPushSettings = new NuGetPushSettings
	{
		Source = nugetsource,
		ApiKey = "Development:Development"
	};

var msbuildSettings = new MSBuildSettings 
	{
		Verbosity = Verbosity.Minimal,
		ToolVersion = MSBuildToolVersion.VS2015,
		Configuration = configuration,
		PlatformTarget = PlatformTarget.MSIL
	};

Task("Clean")
	.Does(() =>
{
	CleanDirectory(artifacts);
});

Task("Restore")
	.IsDependentOn("Clean")
	.Does(() => 
{
	NuGetRestore(project, nugetRestoreSettings);
});

Task("Update")
	.IsDependentOn("Restore")
	.Does(() =>
{
	NuGetUpdate(project, nugetUpdateSettings);
});

Task("Build")
	.IsDependentOn("Clean")
	.IsDependentOn("Restore")
	.IsDependentOn("Update")
	.Does(() =>
{
	MSBuild(project, msbuildSettings);
});

Task("Package")
	.IsDependentOn("Build")
	.Does(() =>
{
	NuGetPack(project, nugetPackSettings);
});

Task("Push")
	.IsDependentOn("Package")
	.Does(() =>
{
	var symbolpkg = GetFiles(System.IO.Path.Combine(artifacts, "*.symbols.nupkg"));
	NuGetPush(symbolpkg, nugetPushSettings);
});

Task("Default").IsDependentOn("Build");

RunTarget(target);
