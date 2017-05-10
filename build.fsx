// include Fake lib
#r @"packages/FAKE/tools/FakeLib.dll"
open Fake
open Fake.RestorePackageHelper
open Fake.Testing.XUnit2

let buildDir = "./build"

Target "RestorePackages" (fun _ -> 
    trace "Restoring packages"

    "./src/FSharpIntro.sln"
    |> RestoreMSSolutionPackages (fun p -> 
        { p with 
            Retries = 4
            ToolPath = "/Library/Frameworks/Mono.framework/Versions/5.0.0/lib/mono/nuget/NuGet.exe"
            OutputPath = "./src/packages" })
 )

// Default target
Target "Default" (fun _ ->
    trace "Running default target"
)

Target "Build" (fun _ ->
    trace "Building solution"

    !! "src/**/*.fsproj"
        |> MSBuildDebug buildDir "Build"
        |> Log "AppBuild-Output: "
)

Target "Test" (fun _ ->
    trace "Running test"

    !! (buildDir @@ "FSharpIntro.Tests.dll")
        |> xUnit2 (fun p -> { p with HtmlOutputPath = Some (buildDir @@ "xunit.html") })
)

"RestorePackages"
    ==> "Build"
    ==> "Test"
    ==> "Default"

// start build
RunTargetOrDefault "Default"