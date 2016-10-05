<#
    .SYNOPSIS
    Determine if a Chocolatey package is installed.

    .DESCRIPTION
    Given a Chocolatey package name, determine if it is installed locally.

    .NOTES
        Change Log
        ----------------------------------------------------------------
        2016-10-04 (gpm): Initial create.

    .EXAMPLE
    Test-ChocoPackageInstalled
#>
function Test-ChocoPackageInstalled
{
    Param
    (
        [String]
        # Name of Chocolatey package to install.
        $PackageName
    )

    $packageNameLength = $PackageName.Length;
    $chocoPackages = choco list -localonly
    $foundPackage = $chocoPackages | Where-Object { $_.Length -ge $packageNameLength -and $_.Substring(0,$packageNameLength) }

    return ($foundPackage -ne $null)
}


<#
    .SYNOPSIS
    Install a Chocolatey package.

    .DESCRIPTION
    Given the name of a Chocolatey package, install the latest stable version.

    .NOTES
        Change Log
        ----------------------------------------------------------------
        2016-10-05 (gpm): Initial create.

    .EXAMPLE
    Install-ChocoPackage
#>
function Install-ChocoPackage
{
    Param
    (
        [String]
        # Name of Chocolatey package to install.
        $PackageName
    )

    $alreadyInstalled = Test-ChocoPackageInstalled $PackageName

    $chocoCommand = "install"
    if($alreadyInstalled)
    {
        $chocoCommand = "upgrade"
    }

    choco $chocoCommand $PackageName
}


Export-ModuleMember -Function * -Alias *