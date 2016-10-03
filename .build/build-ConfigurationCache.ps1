<#
    .SYNOPSIS
    Defines build script for the BagOUtils.ConfigurationCache library.

    .DESCRIPTION
    This defines how to build ConfigurationCache and then package the 
    assembly into a NuGet package. It also determines if this is an updated
    package and if so, updates NuGet.

    This is a psake script, so it will only run via Invoke-psake (or equivelant).

    .NOTES
        Change Log
        ----------------------------------------------------------------
        2016-10-02 (gpm): Initial create.
#>


#This build assumes the following directory structure
#
#  \.build          - This is where the project build code lives
#  \[root]          - This is the root folder containing the solution file and the build folders.
#  \build-artifacts - This folder is created if it is missing and contains output of the build
#  \packages        - The NuGet packages folder.
#
Properties {
    $buildFolder = Split-Path $psake.build_script_file
    $rootFolder = [System.IO.Path]::GetFullPath("$buildFolder\..")
    $buildArtifactsFolder = [System.IO.Path]::GetFullPath("$rootFolder\build-artifacts\")
    $packagesFolder = [System.IO.Path]::GetFullPath("$rootFolder\packages")
    $solution = "BagOUtils.sln"
    $assemblyName = "BagOUtils.ConfigurationCache"
    $LibZVersion = $null
}

# Build LibZ folder
function GetLibZFolder()
{
    [System.IO.Path]::GetFullPath("$packagesFolder\LibZ.Tool.$LibZVersion")
}

FormatTaskName (("-"*25) + "[ {0} ]" + ("-"*25))

task default -depends Validate, Package

task Clean {
    Write-Host "Creating build-artifacts directory" -ForegroundColor Green
    if (Test-Path $buildArtifactsFolder)
    {
        rd $buildArtifactsFolder -rec -force | out-null
    }

    mkdir $buildArtifactsFolder | out-null

    Write-Host "Cleaning $solution" -ForegroundColor Green
    Exec { msbuild "$rootFolder\$solution" /t:Clean /p:Configuration=Release /v:quiet }
}

task Build -depends Clean {
    Write-Host "Building $solution" -ForegroundColor Green
    Exec { msbuild "$rootFolder\$solution" /t:Build /p:Configuration=Release /v:quiet /p:OutDir=$buildArtifactsFolder }
}

task Package -depends Build {
    $libz = "$(GetLibZFolder)\tools\libz.exe"

    & $libz inject-dll --assembly "$buildArtifactsFolder\$assemblyName" --include "$buildArtifactsFolder\*.dll" --move

    Get-ChildItem "$buildArtifactsFolder" -Exclude *.exe | foreach ($_) {remove-item $_.fullname}
}

task Validate {
    Write-Host "Validate that LibZVersion was provided."
    $missingLibZVersionMessage = "`$LibZVersion should not be null. Run with -properties @{'LibZVersion' = 'version'}"
    Assert ($LibZVersion -ne $null) $missingLibZVersionMessage

    Write-Host "Validate LibZ path resolves to existing packages folders."
    $libZFolder = GetLibZFolder
    $babLibZVersionMessage = "The LibZ package folder was not found: '$libZFolder'."
    Assert (Test-Path $libZFolder) $babLibZVersionMessage
}
