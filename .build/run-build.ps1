<#
    .SYNOPSIS
    Run the build of the Bag O'Utils and publish updates to NuGet.

    .DESCRIPTION
    This builds the all the utilities under the Bag O'Utils. It then creates
    NuGet packages for each. If the version of the utility has been updated,
    the package is uploaded to NuGet.

    .NOTES
        Change Log
        ----------------------------------------------------------------
        2016-10-02 (gpm): Initial create.

    .EXAMPLE
    build -PSakeVersion '4.6.0' -LibZVersion '1.2.0.0'
#>


Param
(
    [Parameter(Mandatory=$true)]
    [string]
    # Version of psake to use.
    $PSakeVersion,

    [Parameter(Mandatory=$true)]
    [string]
    # Version of LibZ to use.
    $LibZVersion
)


# NuGet isn't doing so well with solution level installation,
# so we have to dig into the packages folder.
Import-Module "..\packages\psake.$PSakeVersion\tools\psake.psm1"

Invoke-psake .\build.ps1 -framework 4.6.1 -properties @{"LibZVersion" = "$LibZVersion"}