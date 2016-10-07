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
    build -PSakeVersion '4.6.0'
#>


Param
(
)


psake .\build-ConfigurationCache.ps1 -framework 4.6.1