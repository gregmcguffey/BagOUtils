<#
    .SYNOPSIS
    Install build command line build tools.

    .DESCRIPTION
    This installs tools used by the build via Chocolatey.
    
    The following tools are installed:
      * NuGet: .Net package manager.
      * psake: build tool
      * pscx: PowerShell Community Extensions

    This requires that you have Chocolatey installed.

    https://chocolatey.org/install

    .NOTES
        Change Log
        ----------------------------------------------------------------
        2016-10-04 (gpm): Initial create.
        
    .EXAMPLE
    .\install-NuGet.CommandLine.ps1
#>


Import-Module .\ChocoUtils.psm1

# List all the tools that will be installed.
$tools = "psake", "NuGet.CommandLine", "pscx"

$tools | ForEach-Object { Install-ChocoPackage $_ $isInstalled }