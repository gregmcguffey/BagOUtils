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
    [ValidateSet("none", "nuget", "local")]
    [string]
    # Publish action to take. Three choices: none, nuget, local.
    # default is none. The nuget choice publishs to configured 
    # nuget repository. The local choice publishs to a local
    # folder for testing.
    $PublishAction = "none",

    [string]
    # Define the path to the local NuGet source folder. This
    # is the folder this machine has configured in VS in NuGet
    # and can be used to test pre-releaes packages. This is required
    # with a PublishAction of 'local'.
    $PreReleaseFolder
)

trap
{
    Write-Output $_
    exit 1
}

# Validate that if the publish action is 'none', a pre-release was provided.
$folderOK = (($PublishAction -eq "local" -and -not [string]::IsNullOrWhiteSpace($PreReleaseFolder)) -or ($PublishAction -ne "local"))
if( -not $folderOK )
{
    throw [System.ArgumentNullException] "If the PublishAction is 'local', then you must supply a PreReleaseFolder."
}
psake .\build-ConfigurationCache.ps1 -framework 4.6.1 -properties "@{ 'publishAction' = '$PublishAction'; 'prereleaseFolder' = '$PreReleaseFolder' }"

exit 0