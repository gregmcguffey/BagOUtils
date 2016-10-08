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
    [switch]
    # Publish the package(s) to NuGet after building?
    $PublishToNuGet,

    [switch]
    # The package is an alpha build.
    $Alpha,

    [switch]
    # The package is a beta build.
    $Beta,

    [switch]
    # The package is a release candidate
    $ReleaseCandidate
)

trap
{
    Write-Output $_
    exit 1
}

# Process pre-build suffixes. One or zero can be defined.
$prereleaseFlagCount = [int]$Alpha.IsPresent + [int]$Beta.IsPresent + [int]$ReleaseCandidate.IsPresent
if($prereleaseFlagCount -gt 1) {
    throw [System.ArgumentException] "Zero or one pre-release flags can be specified. You specified $prereleaseFlagCount."
}
$prereleaseSuffix = ""
if($Alpha.IsPresent) { $prereleaseSuffix = "alpha"}
if($Beta.IsPresent) { $prereleaseSuffix = "beta"}
if($ReleaseCandidate.IsPresent) { $prereleaseSuffix = "rc"}

psake .\build-ConfigurationCache.ps1 -framework 4.6.1 -properties "@{ 'publishIt' = `$$PublishToNuGet; 'prereleaseSuffix' = '$prereleaseSuffix' }"

exit 0